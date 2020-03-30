using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Infrastucture.Data;
using AIDA.Master.Service.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Radyalabs.Core.Helper;
using Radyalabs.Core.LogHelper;
using Radyalabs.Core.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AIDA.Master.Service.Businesses
{
    public class BaseBusiness
    {
        protected IUnitOfWork _unitOfWork;
        protected UserAuthenticated _userAuth;
        protected CultureInfo _cultureInfo;
        protected ILogHelper _logger;

        public BaseBusiness()
        {
            _unitOfWork = new UnitOfWork<_AIDAEntities>();
            _cultureInfo = CultureInfo.InvariantCulture;
            _logger = new Log4NetHelper();
        }

        public object GetListBranch()
        {
            IRepository<Plant> repo = _unitOfWork.GetRepository<Plant>();

            List<Plant> list = repo.Find();

            if (list == null) return null;

            List<PlantViewModel> result = new List<PlantViewModel>();

            foreach (var item in list)
            {
                result.Add(new PlantViewModel()
                {
                    Code = item.Plant1,
                    Name = item.Description
                });
            }

            return result;
        }

        public List<RayonTypeViewModel> GetListRayonType()
        {
            //DateTime defaultValidTo = DateTime.ParseExact(AppConstant.DefaultValidTo, AppConstant.DefaultFormatDate, _cultureInfo);

            //DateTime today = DateTime.UtcNow.ToUtcID().Date;

            //IRepository<RHeader> repo = _unitOfWork.GetRepository<RHeader>();
            //repo.Condition = PredicateBuilder.True<RHeader>().And(x=> x.ValidTo >= today);

            //List<RHeader> listItem = repo.Find();

            //if (listItem == null) return null;

            //List<RayonTypeViewModel> listModel = new List<RayonTypeViewModel>();
            //RayonTypeViewModel model = null;

            //foreach (var item in listItem)
            //{
            //    model = new RayonTypeViewModel()
            //    {
            //        RayonType = item.RayonType
            //    };

            //    listModel.Add(model);
            //}

            List<RayonTypeViewModel> listModel = new List<RayonTypeViewModel>()
            {
                new RayonTypeViewModel("DCS"),
                new RayonTypeViewModel("GSK"),
                new RayonTypeViewModel("GSV"),
                new RayonTypeViewModel("MDD"),
                new RayonTypeViewModel("MTL"),
                new RayonTypeViewModel("PFV"),
                new RayonTypeViewModel("PGV"),
                new RayonTypeViewModel("RDI"),
                new RayonTypeViewModel("SAV"),
                new RayonTypeViewModel("SD1"),
                new RayonTypeViewModel("SD2"),
                new RayonTypeViewModel("SEM"),
            };

            return listModel;
        }

        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[AppConstant.ConnStringName].ConnectionString;
        }

        protected Dictionary<string, ICellStyle> GetDcCellStyle(IWorkbook workbook)
        {
            string dateFormat = "yyyy/MM/dd";

            ICreationHelper creationHelper = workbook.GetCreationHelper();

            ICellStyle cellStyleDate = workbook.CreateCellStyle();
            cellStyleDate.DataFormat = creationHelper.CreateDataFormat().GetFormat(dateFormat);

            ICellStyle cellStyleText = workbook.CreateCellStyle();
            cellStyleText.DataFormat = creationHelper.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyleInteger = workbook.CreateCellStyle();
            cellStyleInteger.DataFormat = creationHelper.CreateDataFormat().GetFormat("0");

            ICellStyle cellStyleDecimal = workbook.CreateCellStyle();
            cellStyleDecimal.DataFormat = workbook.CreateDataFormat().GetFormat("0.00");

            var fontBold = workbook.CreateFont();
            fontBold.FontHeightInPoints = 11;
            fontBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            ICellStyle cellStyleHeader = workbook.CreateCellStyle();
            cellStyleHeader.SetFont(fontBold);
            cellStyleHeader.BorderBottom = BorderStyle.Thin;
            cellStyleHeader.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyleHeader.FillPattern = FillPattern.SolidForeground;

            Dictionary<string, ICellStyle> dcCellStyle = new Dictionary<string, ICellStyle>()
            {
                { "header", cellStyleHeader },
                { "text", cellStyleText },
                { "number", cellStyleInteger },
                { "decimal", cellStyleDecimal },
                { "date", cellStyleDate }
            };

            return dcCellStyle;
        }

        private List<string> GetListModule(string roleCode)
        {
            List<string> listModule = new List<string>();

            foreach (var dc in ModuleCode.DcRole)
            {
                if (listModule.Exists(x => x.Equals(dc.Key))) continue;

                foreach (var str in dc.Value)
                {
                    if (str.Equals(roleCode))
                    {
                        listModule.Add(dc.Key);
                        break;
                    }
                }
            }

            return listModule;
        }

        protected NSM GetNsmByUserAuth()
        {
            NSM nsm = null;

            string query = "SELECT DISTINCT nsm.NIK, nsm.FullName, nsm.Email" + System.Environment.NewLine +
"FROM RHHeader rhh" + System.Environment.NewLine +
"LEFT JOIN NSM nsm ON nsm.NIK = rhh.NSM" + System.Environment.NewLine +
"    AND nsm.ValidTo >= GETDATE()" + System.Environment.NewLine +
"WHERE 1=1" + System.Environment.NewLine +
"    AND rhh.ValidTo >= GETDATE()" + System.Environment.NewLine +
"[:CONDITION]";

            string condition = "";
            Dictionary<string, object> dcParams = new Dictionary<string, object>();

            if (RoleCode.ASM.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rhh.ASM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.FSS.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rhh.FSS = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }

            query = query.Replace("[:CONDITION]", condition);

            string connString = GetConnectionString();

            DataTable dataTable = SqlHelper.ExecuteQuery(connString, query, dcParams);

            if (dataTable == null || dataTable.Rows.Count == 0) return null;

            nsm = new NSM();

            nsm.NIK = dataTable.Rows[0].IsNull("NIK") ? 0 : Convert.ToInt32(dataTable.Rows[0]["NIK"]);
            nsm.FullName = dataTable.Rows[0].IsNull("FullName") ? null : dataTable.Rows[0]["FullName"].ToString();
            nsm.Email = dataTable.Rows[0].IsNull("Email") ? null : dataTable.Rows[0]["Email"].ToString();

            return nsm;
        }

        protected DataTable GetNSMByCondition(int year, int month, string field = null, string var = null, string value = null)
        {
            string connString = GetConnectionString();

            string validTo = new DateTime(year, month, 1).AddDays(-1).ToString("yyyy-MM-dd");

            string query = "SELECT DISTINCT NSM" + System.Environment.NewLine +
"FROM RHHeader" + System.Environment.NewLine +
"WHERE 1=1" + System.Environment.NewLine +
"    AND ValidTo >= @validTo";

            query = query.Replace("[:ROLE]", field);

            Dictionary<string, object> dcParam = new Dictionary<string, object>()
            {
                { "@validTo", validTo }
            };

            if (field != null && var != null && value != null)
            {
                query += System.Environment.NewLine + string.Format(" AND {0} = {1}", field, var);

                dcParam[var] = value;
            }

            return SqlHelper.ExecuteQuery(connString, query, dcParam);
        }

        protected string GetStrListNSM(int year, int month, string field = null, string var = null, string value = null)
        {
            DataTable dtListNSM = GetNSMByCondition(year, month, field, var, value);

            if (dtListNSM == null || dtListNSM.Rows.Count == 0) return null;

            string str = "";

            foreach (DataRow dr in dtListNSM.Rows)
            {
                str += dr["NSM"].ToString() + ",";
            }

            str = str.Trim(',');

            return str;
        }

        protected object GetObjFromCell(ICell cell)
        {
            if (cell == null) return null;

            object obj = null;

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    obj = cell.NumericCellValue;
                    break;
                case CellType.String:
                    obj = cell.StringCellValue;
                    break;
            }

            return obj;
        }

        protected string GetPostedFileType(HttpPostedFileBase postedFile)
        {
            foreach (var item in PostedFileType.ContentTypes)
            {
                if (item.Value.Exists(x => x.Equals(postedFile.ContentType)))
                {
                    return item.Key;
                }
            }

            return null;
        }

        protected string GetSalesGroupRayonCode()
        {
            if (RoleCode.KaCab.Equals(_userAuth.RoleCode)
                || RoleCode.RM.Equals(_userAuth.RoleCode)
                /*|| RoleCode.NSM.Equals(_userAuth.RoleCode)*/)
            {
                return SalesGroupRayonCode.All;
            }
            //else if (RoleCode.NSM.Equals(_userAuth.RoleCode))
            //{
            //    return SalesGroupRayonCode.AllByPlant;
            //}

            DateTime today = DateTime.UtcNow.ToUtcID().Date;

            string query = "select distinct rhh.RayonType" + System.Environment.NewLine +
"    , rh.SalesGroup" + System.Environment.NewLine +
"from RHHeader rhh" + System.Environment.NewLine +
"left join RHeader rh on rh.RayonType = rhh.RayonType" + System.Environment.NewLine +
"where 1= 1" + System.Environment.NewLine +
"    and rhh.ValidTo >= GETDATE()" + System.Environment.NewLine +
"[:CONDITON]";

            Dictionary<string, object> dcParams = new Dictionary<string, object>();

            if (RoleCode.BUM.Equals(_userAuth.RoleCode))
            {
                query = query.Replace("[:CONDITON]", "    and rhh.BUM = @nikBUM");

                dcParams["@nikBUM"] = _userAuth.NIK;
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode))
            {
                query = query.Replace("[:CONDITON]", "    and rhh.NSM = @nikNSM");

                dcParams["@nikNSM"] = _userAuth.NIK;
            }
            else
            {
                return null;
            }

            DataTable dt = SqlHelper.ExecuteQuery(GetConnectionString(), query, dcParams);

            if (dt == null || dt.Rows.Count == 0) return null;

            List<string> listSalesGroup = new List<string>()
            {
                "MDD", "SD1", "SD2"
            };

            List<string> listRayonCode = new List<string>()
            {
                "GSV", "PFV"
            };

            string code = null;

            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (listSalesGroup.Exists(x => x.Equals(dr["SalesGroup"].ToString())))
                    {
                        code = listSalesGroup.FirstOrDefault(x => x.Equals(dr["SalesGroup"].ToString()));
                        break;
                    }
                    else if (listRayonCode.Exists(x => x.Equals(dr["RayonType"].ToString())))
                    {
                        code = listRayonCode.FirstOrDefault(x => x.Equals(dr["RayonType"].ToString()));
                        break;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Write("error", DateTime.Now, ex.Message, _userAuth.Fullname, ex);
                    continue;
                }
            }

            return code;
        }

        public UserAuthenticated GetUserAuth()
        {
            string strUserId = AppCookieHelper.Get<string>();

            if (string.IsNullOrEmpty(strUserId)) return null;

            Guid userId = Guid.Parse(strUserId);

            TUser user = GetUserById(userId);

            return GetUserAuth(user);
        }

        public UserAuthenticated GetUserAuth(TUser user)
        {
            if (user == null) return null;
            if (user.TUserRole == null || user.TUserRole.Count == 0) return null;
            if (user.EmpID == (int?)null) return null;

            UserAuthenticated userAuth = new UserAuthenticated();

            if (user.IsActive != (int?)null && user.IsActive.Value == 1)
            {
                userAuth.IsActive = true;
            }
            else
            {
                userAuth.IsActive = false;

                return userAuth;
            }

            string currentRoleCode = user.TUserRole.FirstOrDefault().TRole.status;

            userAuth.RoleCode = currentRoleCode;

            if (RoleCode.Admin.Equals(currentRoleCode))
            {
                SetUserAdmin(ref userAuth, user.EmpID.Value);
            }
            else if (RoleCode.ASM.Equals(currentRoleCode))
            {
                SetUserASM(ref userAuth, user.EmpID.Value);
            }
            else if (RoleCode.BUM.Equals(currentRoleCode))
            {
                SetUserBUM(ref userAuth, user.EmpID.Value);
            }
            else if (RoleCode.FSS.Equals(currentRoleCode))
            {
                SetUserFSS(ref userAuth, user.EmpID.Value);
            }
            else if (RoleCode.NSM.Equals(currentRoleCode))
            {
                SetUserNSM(ref userAuth, user.EmpID.Value);
            }
            else if (RoleCode.SLM.Equals(currentRoleCode))
            {
                SetUserSLM(ref userAuth, user.EmpID.Value);
            }
            //else if (RoleCode.KaCab.Equals(currentRoleCode)) -- table Kacab is currently not exist
            //{
            //    SetUserKaCab(ref userAuth, user.EmpID.Value);
            //}
            else if (RoleCode.AdminOperation.Equals(currentRoleCode))
            {
                SetUserAdminOperation(ref userAuth, user.EmpID.Value);
            }

            else if (RoleCode.AdminExclusive.Equals(currentRoleCode))
            {
                SetUserAdminExclusive(ref userAuth, user.EmpID.Value);
            }

            userAuth.ListModule = GetListModule(userAuth.RoleCode);

            return userAuth;
        }

        protected TUser GetUserById(Guid id)
        {
            IRepository<TUser> repoUser = _unitOfWork.GetRepository<TUser>();
            repoUser.Includes = new string[] { "TUserRole.TRole" };
            repoUser.Condition = PredicateBuilder.True<TUser>().And(x => x.UserID == id);

            return repoUser.Find().FirstOrDefault();
        }

        protected TUser GetUserByNIK(string NIK)
        {
            IRepository<TUser> repoUser = _unitOfWork.GetRepository<TUser>();
            repoUser.Includes = new string[] { "TUserRole.TRole" };
            repoUser.Condition = PredicateBuilder.True<TUser>().And(x => x.UserName == NIK);

            return repoUser.Find().FirstOrDefault();
        }

        protected IWorkbook GetWorkbook(HttpPostedFileBase postedFile)
        {

            string postedFileType = Path.GetExtension(postedFile.FileName);
            IWorkbook workbook = null;

            try
            {
                using (Stream stream = postedFile.InputStream)
                {
                    if (PostedFileType.XLS.Equals(postedFileType)) // excel 97-2003 workbook
                    {
                        workbook = new HSSFWorkbook(stream);
                    }
                    else if (PostedFileType.XLSX.Equals(postedFileType)) // excel workbook
                    {
                        workbook = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Write("error", DateTime.Now, ex.Message, _userAuth.Fullname, ex);
                return null;
            }

            return workbook;
        }

        public bool IsAccessible(string moduleCode)
        {
            if (_userAuth == null) return false;

            if (!_userAuth.IsRoleValid) return false;

            if (!ModuleCode.DcRole.ContainsKey(moduleCode)) return false;

            List<string> listRole = ModuleCode.DcRole[moduleCode];

            if (listRole == null) return false;

            if (listRole.Exists(x => x.Equals(_userAuth.RoleCode)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void SetDatatableRepository<T>(JDatatableViewModel model, string[] arrOrderColumn, ref IRepository<T> repo, ref JDatatableResponse result)
        {
            long? totalRecords = null;

            if (model.Length > -1) // get total records for pagging info only, model.length -1 = get all records
            {
                totalRecords = repo.Count();

                if (totalRecords == null) return;

                result.TotalRecords = totalRecords.Value;
                result.TotalDisplayRecords = totalRecords.Value;
            }

            SqlOrderBy orderBy = new SqlOrderBy()
            {
                Type = SqlOrderType.Descending,
                Column = "CreatedDate"
            };

            if (model.IndexOrderCol <= arrOrderColumn.Length)
            {
                orderBy.Column = arrOrderColumn[model.IndexOrderCol];
                orderBy.Type = model.OrderType.Equals(SqlOrderType.Ascending) ? SqlOrderType.Ascending : SqlOrderType.Descending;
            }

            repo.OrderBy = orderBy;

            if (model.Length > -1)
            {
                repo.Limit = model.Length;
                repo.Offset = model.Start;
            }
        }

        public void SetUserAuth(UserAuthenticated userAuth)
        {
            _userAuth = userAuth;
        }

        private void SetUserAdminOperation(ref UserAuthenticated userAuth, int empID)
        {
            DateTime today = DateTime.UtcNow.ToUtcID();

            IRepository<AdminOperation> repo = _unitOfWork.GetRepository<AdminOperation>();
            repo.Condition = PredicateBuilder.True<AdminOperation>().And(x=>x.NIK == empID && x.ValidTo >= today);

            AdminOperation adm = repo.Find().FirstOrDefault();

            if (adm == null) return;

            userAuth.IsRoleValid = true;
            userAuth.NIK = adm.NIK;
            userAuth.Fullname = adm.FullName;
        }

        private void SetUserAdminExclusive(ref UserAuthenticated userAuth, int empID)
        {
            DateTime today = DateTime.UtcNow.ToUtcID();


            IRepository<AdminExclusive> repo = _unitOfWork.GetRepository<AdminExclusive>();
            repo.Condition = PredicateBuilder.True<AdminExclusive>().And(x => x.NIK == empID && x.ValidTo >= today);

            AdminExclusive adm = repo.Find().FirstOrDefault();

            if (adm == null) return;
            userAuth.IsRoleValid = true;
            userAuth.NIK = adm.NIK;
            userAuth.Fullname = adm.FullName;
        }

        private void SetUserASM(ref UserAuthenticated userAuth, int empID)
        {
            DateTime today = DateTime.UtcNow.ToUtcID();

            IRepository<ASM> repoASM = _unitOfWork.GetRepository<ASM>();
            repoASM.Condition = PredicateBuilder.True<ASM>().And(x => x.NIK == empID && x.ValidTo >= today);

            ASM asm = repoASM.Find().FirstOrDefault();

            if (asm == null) return;

            userAuth.IsRoleValid = true;
            userAuth.NIK = asm.NIK;
            userAuth.Fullname = asm.FullName;
            userAuth.IsAbleToWrite = asm.AllowedByNIK == null ? false : true;
        }

        private void SetUserAdmin(ref UserAuthenticated userAuth, int empID)
        {
            throw new NotImplementedException();
        }

        private void SetUserSLM(ref UserAuthenticated userAuth, int empID)
        {
            DateTime today = DateTime.UtcNow.ToUtcID();

            IRepository<SLM> repoSLM = _unitOfWork.GetRepository<SLM>();
            repoSLM.Condition = PredicateBuilder.True<SLM>().And(x=>x.NIK == empID && x.ValidTo >= today);

            SLM slm = repoSLM.Find().FirstOrDefault();

            if (slm == null) return;

            userAuth.IsRoleValid = true;
            userAuth.NIK = slm.NIK;
            userAuth.Fullname = slm.FullName;
        }

        private void SetUserNSM(ref UserAuthenticated userAuth, int empID)
        {
            DateTime today = DateTime.UtcNow.ToUtcID();

            IRepository<NSM> repoNSM = _unitOfWork.GetRepository<NSM>();
            repoNSM.Condition = PredicateBuilder.True<NSM>().And(x => x.NIK == empID && x.ValidTo >= today);

            NSM nsm = repoNSM.Find().FirstOrDefault();

            if (nsm == null) return;

            userAuth.IsRoleValid = true;
            userAuth.NIK = nsm.NIK;
            userAuth.Fullname = nsm.FullName;

            if (nsm.Plant != null)
            {
                // set as kacab
                userAuth.RoleCode = RoleCode.KaCab;
                userAuth.Plant = nsm.Plant;
            }
        }

        private void SetUserFSS(ref UserAuthenticated userAuth, int empID)
        {
            DateTime today = DateTime.UtcNow.ToUtcID();

            IRepository<FSS> repoFSS = _unitOfWork.GetRepository<FSS>();
            repoFSS.Condition = PredicateBuilder.True<FSS>().And(x => x.NIK == empID && x.ValidTo >= today);

            FSS fss = repoFSS.Find().FirstOrDefault();

            if (fss == null) return;

            bool allowByNIK = false;
            userAuth.IsRoleValid = true;
            userAuth.NIK = fss.NIK;
            userAuth.Fullname = fss.FullName;
            if(fss.UploadValidTo != null)
            {
                if (fss.UploadValidTo.Value.Date >= today.Date)
                    allowByNIK = true;
            }
            //userAuth.IsAbleToWrite = fss.AllowedByNIK == null ? false : true;
            userAuth.IsAbleToWrite = allowByNIK;
        }

        private void SetUserBUM(ref UserAuthenticated userAuth, int empID)
        {
            DateTime today = DateTime.UtcNow.ToUtcID();

            IRepository<BUM> repoBUM = _unitOfWork.GetRepository<BUM>();
            repoBUM.Condition = PredicateBuilder.True<BUM>().And(x => x.NIK == empID && x.ValidTo >= today);

            BUM bum = repoBUM.Find().FirstOrDefault();

            if (bum == null) return;

            userAuth.IsRoleValid = true;
            userAuth.NIK = bum.NIK;
            userAuth.Fullname = bum.FullName;

            if (RoleCode.RM.Equals(bum.Role))
            {
                userAuth.RoleCode = RoleCode.RM;
            }
            else if (RoleCode.BUM.Equals(bum.Role))
            {
                userAuth.RoleCode = RoleCode.BUM;
            }
            else
            {
                userAuth.IsRoleValid = false;
            }
        }

        private void SetUserKaCab(ref UserAuthenticated userAuth, int empID)
        {
            DateTime today = DateTime.UtcNow.ToUtcID();

            IRepository<KaCab> repoKaCab = _unitOfWork.GetRepository<KaCab>();
            repoKaCab.Condition = PredicateBuilder.True<KaCab>().And(x => x.NIK == empID && x.ValidTo >= today);

            KaCab kacab = repoKaCab.Find().FirstOrDefault();

            if (kacab == null) return;

            userAuth.Plant = kacab.Plant;
            userAuth.IsRoleValid = true;
            userAuth.NIK = kacab.NIK;
            userAuth.Fullname = kacab.FullName;
        }

        private string GetRayonType(string rayonType)
        {
            return "";
        }

        public string GetRayonTypeByNIK(int nik)
        {
            DateTime today = DateTime.UtcNow.ToUtcID();

            IRepository<AdminExclusive> repo = _unitOfWork.GetRepository<AdminExclusive>();
            repo.Condition = PredicateBuilder.True<AdminExclusive>().And(x => x.NIK == nik && x.ValidTo >= today);

            AdminExclusive adm = repo.Find().FirstOrDefault();

            return adm.RayonType != null ? "" : adm.RayonType;
        }

        //public bool IsEditable()
        //{
        //    if (_userAuth == null) return false;

        //    if ((RoleCode.FSS.Equals(_userAuth.RoleCode) || RoleCode.ASM.Equals(_userAuth.RoleCode)) && _userAuth.IsAbleToWrite)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public List<RHHeader> GetRHHeaderByUserAuth()
        {
            if (_userAuth == null) return null;

            DateTime today = DateTime.UtcNow.ToUtcID().Date;

            IRepository<RHHeader> repo = _unitOfWork.GetRepository<RHHeader>();
            //repo.Includes = new string[] { "SLMObj1" };
            repo.Includes = new string[] { "BUMObj1", "NSMObj1", "ASMObj1", "FSSObj1", "SLMObj1" };

            repo.Condition = PredicateBuilder.True<RHHeader>().And(x => x.ValidTo >= today);

            if (RoleCode.BUM.Equals(_userAuth.RoleCode) || RoleCode.RM.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.BUM.Equals(_userAuth.NIK));
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode) || RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.NSM.Equals(_userAuth.NIK));
            }
            else if (RoleCode.ASM.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.ASM.Equals(_userAuth.NIK));
            }
            else if (RoleCode.FSS.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.FSS.Equals(_userAuth.NIK));
            }
            else if (RoleCode.SLM.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.SLM.Equals(_userAuth.NIK));
            }
            else if (RoleCode.KaCab.Equals(_userAuth.RoleCode) && _userAuth.Plant != null)
            {
                repo.Condition = repo.Condition.And(x => x.NSM.Equals(_userAuth.NIK) || x.Plant.Equals(_userAuth.Plant.Value.ToString()));
            }
            else
            {
                return null;
            }

            return repo.Find();
        }
    }
}
