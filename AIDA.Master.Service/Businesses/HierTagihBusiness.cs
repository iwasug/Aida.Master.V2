using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Infrastucture.Data;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Radyalabs.Core.Helper;
using Radyalabs.Core.Repository;

namespace AIDA.Master.Service.Businesses
{
    public class HierTagihBusiness : BaseBusiness
    {
        private const string _formattedDate = "yyyy-MM-dd";
        private static DateTime _defaultValidTo = Convert.ToDateTime(AppConstant.DefaultValidTo);

        public AlertMessage ExportTagihToExcel(JDatatableViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            model.Length = -1;

            JDatatableResponse response = GetDatatableByQuery(model);

            IWorkbook workbook = new XSSFWorkbook();

            int rowIndex = 0;

            ISheet sheet1 = workbook.CreateSheet("Sheet 1");

            string dateFormat = "yyyy/MM/dd";

            ICreationHelper creationHelper = workbook.GetCreationHelper();

            ICellStyle cellStyleDate = workbook.CreateCellStyle();
            cellStyleDate.DataFormat = creationHelper.CreateDataFormat().GetFormat(dateFormat);

            ICellStyle cellStyleText = workbook.CreateCellStyle();
            cellStyleText.DataFormat = creationHelper.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyleNumber = workbook.CreateCellStyle();
            cellStyleNumber.DataFormat = creationHelper.CreateDataFormat().GetFormat("0");

            var fontBold = workbook.CreateFont();
            fontBold.FontHeightInPoints = 11;
            fontBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            ICellStyle cellStyleHeader = workbook.CreateCellStyle();
            cellStyleHeader.SetFont(fontBold);
            cellStyleHeader.BorderBottom = BorderStyle.Thin;
            cellStyleHeader.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyleHeader.FillPattern = FillPattern.SolidForeground;

            IRow row = sheet1.CreateRow(rowIndex);

            row = sheet1.CreateRow(rowIndex);

            int cellIndex = 0;

            ICell cell = null;

            #region cell header
            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("RayonCode");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Plant");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("KaCab NIK");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("KaCab Name");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("ASM NIK");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("ASM Name");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("FSS NIK");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("FSS Name");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("SLM NIK");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("SLM Name");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Collector NIK");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Collector Name");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("AR Collector NIK");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("AR Collector Name");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("SPV AR Collector NIK");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("SPV AR Collector Name");
            cell.CellStyle = cellStyleHeader;
            #endregion

            rowIndex++;

            #region cell data
            List<HierTagihViewModel> listData = response.Data as List<HierTagihViewModel>;

            if (listData != null)
            {
                foreach (var item in listData)
                {
                    row = sheet1.CreateRow(rowIndex);
                    cellIndex = 0;

                    // RayonCode
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.RayonCode);
                    cell.CellStyle = cellStyleText;

                    // Plant
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.PlantCode);
                    cell.CellStyle = cellStyleNumber;

                    // NSM NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.NSMNik);
                    cell.CellStyle = cellStyleNumber;

                    // NSM Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.NSMFullname);
                    cell.CellStyle = cellStyleText;

                    // ASM NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.ASMNik);
                    cell.CellStyle = cellStyleNumber;

                    // ASM Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.ASMFullname);
                    cell.CellStyle = cellStyleText;

                    // FSS NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.FSSNik);
                    cell.CellStyle = cellStyleNumber;

                    // FSS Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.FSSFullname);
                    cell.CellStyle = cellStyleText;

                    // SLM NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.SLMNik);
                    cell.CellStyle = cellStyleNumber;

                    // SLM Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.SLMFullname);
                    cell.CellStyle = cellStyleText;

                    // Collector NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.CollectorNik ?? 0);
                    cell.CellStyle = cellStyleNumber;

                    // Collector Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.CollectorFullname);
                    cell.CellStyle = cellStyleText;

                    // AR Collector NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.FakturisNik ?? 0);
                    cell.CellStyle = cellStyleNumber;

                    // AR Collector Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.FakturisFullname);
                    cell.CellStyle = cellStyleText;

                    // SPV AR Collector NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.SPVFakturisNik ?? 0);
                    cell.CellStyle = cellStyleNumber;

                    // SPV AR Collector Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.SPVFakturisFullname);
                    cell.CellStyle = cellStyleText;

                    rowIndex++;
                }
            }
            #endregion

            #region auto size column
            for (int i = 0; i < 16; i++)
            {
                sheet1.AutoSizeColumn(i);
            }
            #endregion

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);

                alert.Data = ms.ToArray();
            }

            alert.Status = 1;

            return alert;
        }

        public JDatatableResponse GetDatatable(JDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "CreatedOn"
                ,"RayonCode"
                ,"Plant"
                ,"NSM"
                ,"NSMObj.FullName"
                ,"ASM"
                ,"ASMObj.FullName"
                ,"FSS"
                ,"FSSObj.FullName"
                ,"SLM"
                ,"SLMObj.FullName"
                ,"Collector"
                ,"Collector" //,"CollectorObj.FULLNAME"
                ,"Fakturis"
                ,"Fakturis" //,"FakturisObj.FULLNAME"
                ,"SPVFakturis"
                ,"SPVFakturis" //,"SPVFakturisObj.FULLNAME"
                ,"ValidFrom"
                ,"ValidTo"
            };

            if (_userAuth == null) return result;

            IRepository<RTHeader> repo = _unitOfWork.GetRepository<RTHeader>();
            repo.Includes = new string[] { "NSMObj", "ASMObj", "FSSObj", "SLMObj", /*"CollectorObj", "FakturisObj", "SPVFakturisObj"*/ };

            repo.Condition = PredicateBuilder.True<RTHeader>();

            if (RoleCode.NSM.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.NSM == _userAuth.NIK);
            }
            else if (RoleCode.ASM.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.ASM == _userAuth.NIK);
            }
            else if (RoleCode.FSS.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.FSS == _userAuth.NIK);
            }
            else if (RoleCode.SLM.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.SLM == _userAuth.NIK);
            }
            else if (RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x=>x.Plant.Equals(_userAuth.Plant.Value.ToString()));
            }
            else
            {
                return result;
            }

            DateTime today = DateTime.UtcNow.ToUtcID().Date;

            repo.Condition = repo.Condition.And(x=>x.ValidTo >= today);

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                repo.Condition = repo.Condition.And(x => x.RayonCode.Contains(model.Keyword)
                    || x.Plant.Contains(model.Keyword)
                    || x.NSM.ToString().Contains(model.Keyword)
                    || x.NSMObj.FullName.Contains(model.Keyword)
                    || x.ASM.ToString().Contains(model.Keyword)
                    || x.ASMObj.FullName.Contains(model.Keyword)
                    || x.FSS.ToString().Contains(model.Keyword)
                    || x.FSSObj.FullName.Contains(model.Keyword)
                    || x.SLM.ToString().Contains(model.Keyword)
                    || x.SLMObj.FullName.Contains(model.Keyword)
                    || x.Collector.ToString().Contains(model.Keyword)
                    //|| x.CollectorObj.FULLNAME.Contains(model.Keyword)
                    || x.Fakturis.ToString().Contains(model.Keyword)
                    //|| x.FakturisObj.FULLNAME.Contains(model.Keyword)
                    || x.SPVFakturis.ToString().Contains(model.Keyword));
                    //|| x.SPVFakturisObj.FULLNAME.Contains(model.Keyword));
            }

            SetDatatableRepository(model, arrOrderColumn, ref repo, ref result);

            if (model.Length > -1 && result.TotalRecords == 0)
            {
                return result;
            }

            List<RTHeader> listItem = repo.Find();

            if (listItem == null) return result;

            List<HierTagihViewModel> listData = new List<HierTagihViewModel>();

            foreach (var item in listItem)
            {
                listData.Add(GetHierTagihViewModel(item));
            }

            result.Data = listData;


            return result;
        }

        public JDatatableResponse GetDatatableByQuery(JDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "rth.CreatedOn"
                ,"rth.RayonCode"
                ,"rth.Plant"
                ,"SLM"
                ,"slm.FullName"
                ,"FSS"
                ,"fss.FullName"
                ,"ASM"
                ,"asm.FullName"
                ,"rth.NSM"
                ,"nsm.FullName"
                ,"Collector"
                ,"col.FULLNAME" //,"CollectorObj.FULLNAME"
                ,"Fakturis"
                ,"ftr.FULLNAME" //,"FakturisObj.FULLNAME"
                ,"SPVFakturis"
                ,"spv.FULLNAME" //,"SPVFakturisObj.FULLNAME"
                ,"rth.ValidFrom"
                ,"rth.ValidTo"
            };

            #region query
            string query = "SELECT [:SELECT]" + System.Environment.NewLine +
"FROM RTHeader rth" + System.Environment.NewLine +
"LEFT JOIN NSM nsm ON nsm.NIK = rth.NSM" + System.Environment.NewLine +
"	AND nsm.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN ASM asm ON asm.NIK = rth.ASM" + System.Environment.NewLine +
"	AND asm.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN FSS fss ON fss.NIK = rth.FSS" + System.Environment.NewLine +
"	AND fss.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN SLM slm ON slm.NIK = rth.SLM" + System.Environment.NewLine +
"	AND slm.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN TCollector col ON col.NIK = rth.Collector" + System.Environment.NewLine +
"	AND col.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN TFakturis ftr ON ftr.NIK = rth.Fakturis" + System.Environment.NewLine +
"	AND ftr.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN TSPVFakturis spv ON spv.NIK = rth.SPVFakturis" + System.Environment.NewLine +
"	AND spv.ValidTo >= GETDATE()" + System.Environment.NewLine +
"WHERE 1=1" + System.Environment.NewLine +
"	AND rth.ValidTo >= GETDATE()" + System.Environment.NewLine +
"[:CONDITION]";

            string selectColumn = "rth.RayonCode" + System.Environment.NewLine +
"	,rth.Plant" + System.Environment.NewLine +
"	,rth.NSM AS NSMNIK" + System.Environment.NewLine +
"	,nsm.FullName AS NSMFullName" + System.Environment.NewLine +
"	,rth.ASM AS ASMNIK" + System.Environment.NewLine +
"	,asm.FullName AS ASMFullName" + System.Environment.NewLine +
"	,rth.FSS AS FSSNIK" + System.Environment.NewLine +
"	,fss.FullName AS FSSFullName" + System.Environment.NewLine +
"	,rth.SLM AS SLMNIK" + System.Environment.NewLine +
"	,slm.FullName AS SLMFullName" + System.Environment.NewLine +
"	,rth.Collector AS CollectorNIK" + System.Environment.NewLine +
"	,col.FULLNAME AS CollectorFullName" + System.Environment.NewLine +
"	,rth.Fakturis AS FakturisNIK" + System.Environment.NewLine +
"	,ftr.FULLNAME AS FakturisFullName" + System.Environment.NewLine +
"	,rth.SPVFakturis AS SPVFakturisNIK" + System.Environment.NewLine +
"	,spv.FULLNAME AS SPVFakturisFullName" + System.Environment.NewLine +
"   ,rth.ValidFrom" + System.Environment.NewLine +
"	,rth.ValidTo";

            string selectCount = "COUNT(*) AS NumRows";

            #endregion

            if (_userAuth == null) return result;

            string condition = "";
            Dictionary<string, object> dcParams = new Dictionary<string, object>();

            if (RoleCode.RM.Equals(_userAuth.RoleCode))
            {
                condition += "  AND rth.NSM IN (" + System.Environment.NewLine +
"		SELECT DISTINCT NSM" + System.Environment.NewLine +
"		FROM RHHeader" + System.Environment.NewLine +
"		WHERE 1=1" + System.Environment.NewLine +
"			AND ValidTo >= GETDATE()" + System.Environment.NewLine +
"			AND BUM = @nik" + System.Environment.NewLine +
"	)";

                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rth.NSM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.ASM.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rth.ASM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.FSS.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rth.FSS = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.SLM.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rth.FSS = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.KaCab.Equals(_userAuth.RoleCode) && _userAuth.Plant != null)
            {
                //condition += " AND (rth.Plant = @plant)";
                //condition += "	AND (rth.NSM = @nik /*OR rth.Plant = @plant*/)";
                condition += "	AND (rth.NSM = @nik)";
                dcParams["@nik"] = _userAuth.NIK;
                dcParams["@plant"] = _userAuth.Plant.Value;
            }
            else
            {
                return result;
            }

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                model.Keyword = model.Keyword.ToLower();

                condition += System.Environment.NewLine;
                condition += "	AND (LOWER(rth.RayonCode) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rth.Plant LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rth.NSM LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(nsm.FullName) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rth.ASM LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(asm.FullName) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rth.FSS LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(fss.FullName) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rth.SLM LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(slm.FullName) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rth.Collector LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(col.FULLNAME) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rth.Fakturis LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(ftr.FULLNAME) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rth.SPVFakturis LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(spv.FULLNAME) LIKE '%[:KEYWORD]%')";

                condition = condition.Replace("[:KEYWORD]", model.Keyword);
            }

            query = query.Replace("[:CONDITION]", condition);

            string query1 = query.Replace("[:SELECT]", selectCount);

            string connString = GetConnectionString();

            DataTable dataTable = SqlHelper.ExecuteQuery(connString, query1, dcParams);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                result.TotalRecords = Convert.ToInt64(dataTable.Rows[0]["NumRows"]);
                result.TotalDisplayRecords = result.TotalRecords;
            }

            if (result.TotalRecords == 0) return result;

            query1 = query = query.Replace("[:SELECT]", selectColumn);

            if (model.IndexOrderCol <= arrOrderColumn.Length)
            {
                query1 += System.Environment.NewLine + string.Format("ORDER BY {0} {1}", arrOrderColumn[model.IndexOrderCol], model.OrderType.ToUpper());
            }

            if (model.Length > -1)
            {
                query1 += System.Environment.NewLine + "OFFSET " + model.Start + " ROWS" + System.Environment.NewLine +
"FETCH NEXT " + model.Length + " ROWS ONLY";
            }

            dataTable = SqlHelper.ExecuteQuery(connString, query1, dcParams);

            List<HierTagihViewModel> listData = new List<HierTagihViewModel>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    listData.Add(GetHierTagihViewModel(dr));
                }
            }

            result.Data = listData;

            return result;

        }

        public JDatatableResponse GetDatatableCustomer(RayonDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "CreatedOn"
                ,"RayonCode"
                ,"CreatedOn"
                ,"CreatedOn"
                ,"Customer"
                ,"CustomerObj.CustomerName"
                ,"ValidFrom"
                ,"ValidTo"
            };

            if (_userAuth == null) return result;

            DateTime today = DateTime.UtcNow.ToUtcID().Date;

            IRepository<RHDetail> repo = _unitOfWork.GetRepository<RHDetail>();
            repo.Includes = new string[] { "CustomerObj" };

            repo.Condition = PredicateBuilder.True<RHDetail>().And(x=>x.RayonCode.Equals(model.RayonCode));
            repo.Condition = repo.Condition.And(x => x.ValidTo >= today);

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                repo.Condition = repo.Condition.And(x => x.RayonCode.Contains(model.Keyword)
                    || x.Customer.Contains(model.Keyword)
                    || x.CustomerObj.CustomerName.Contains(model.Keyword));
            }

            SetDatatableRepository(model, arrOrderColumn, ref repo, ref result);

            if (model.Length > -1 && result.TotalRecords == 0)
            {
                return result;
            }

            List<RHDetail> listItem = repo.Find();

            if (listItem == null) return result;

            #region get salesman on a rayon

            IRepository<RHHeader> repoRHH = _unitOfWork.GetRepository<RHHeader>();
            repoRHH.Includes = new string[] { "SLMObj1" };
            repoRHH.Condition = PredicateBuilder.True<RHHeader>().And(x=>x.RayonCode.Equals(model.RayonCode) && x.ValidTo >= today);
            repoRHH.OrderBy = new SqlOrderBy("CreatedOn", SqlOrderType.Descending);

            RHHeader rhh = repoRHH.Find().FirstOrDefault();

            #endregion

            List<SalesCustomerViewModel> listData = new List<SalesCustomerViewModel>();

            foreach (var item in listItem)
            {
                listData.Add(GetSalesRayonViewModel(item, rhh));
            }

            result.Data = listData;

            return result;
        }

        private HierTagihViewModel GetHierTagihViewModel(RTHeader item)
        {
            HierTagihViewModel model = new HierTagihViewModel()
            {
                Id = item.ID,
                RayonCode = item.RayonCode,
                PlantCode = item.Plant,
                NSMNik = item.NSM ?? 0,
                ASMNik = item.ASM ?? 0,
                FSSNik = item.FSS ?? 0,
                SLMNik = item.SLM ?? 0,
                CollectorNik = item.Collector,
                FakturisNik = item.Fakturis,
                SPVFakturisNik = item.SPVFakturis,
                ValidFromDate = item.ValidFrom,
                ValidToDate = item.ValidTo
            };

            model.FormattedValidFrom = item.ValidFrom.ToString(_formattedDate);
            model.FormattedValidTo = item.ValidTo.ToString(_formattedDate);

            model.NSMFullname = item.NSMObj == null ? null : item.NSMObj.FullName;
            model.ASMFullname = item.ASMObj == null ? null : item.ASMObj.FullName;
            model.FSSFullname = item.FSSObj == null ? null : item.FSSObj.FullName;
            model.SLMFullname = item.SLMObj == null ? null : item.SLMObj.FullName;
            //model.CollectorFullname = item.CollectorObj == null ? null : item.CollectorObj.FULLNAME;
            //model.FakturisFullname = item.FakturisObj == null ? null : item.FakturisObj.FULLNAME ;
            //model.SPVFakturisFullname = item.SPVFakturisObj == null ? null : item.SPVFakturisObj.FULLNAME;

            return model;
        }

        private HierTagihViewModel GetHierTagihViewModel(DataRow dr)
        {
            HierTagihViewModel model = new HierTagihViewModel();

            model.RayonCode = dr.IsNull("RayonCode") ? null : dr["RayonCode"].ToString();
            model.PlantCode = dr.IsNull("Plant") ? null : dr["Plant"].ToString();

            model.NSMNik = dr.IsNull("NSMNIK") ? 0 : Convert.ToInt32(dr["NSMNIK"]);
            model.NSMFullname = dr.IsNull("NSMFullName") ? null : dr["NSMFullName"].ToString();

            model.ASMNik = dr.IsNull("ASMNIK") ? 0 : Convert.ToInt32(dr["ASMNIK"]);
            model.ASMFullname = dr.IsNull("ASMFullName") ? null : dr["ASMFullName"].ToString();

            model.FSSNik = dr.IsNull("FSSNIK") ? 0 : Convert.ToInt32(dr["FSSNIK"]);
            model.FSSFullname = dr.IsNull("FSSFullName") ? null : dr["FSSFullName"].ToString();

            model.SLMNik = dr.IsNull("SLMNIK") ? 0 : Convert.ToInt32(dr["SLMNIK"]);
            model.SLMFullname = dr.IsNull("SLMFullName") ? null : dr["SLMFullName"].ToString();

            model.CollectorNik = dr.IsNull("CollectorNIK") ? 0 : Convert.ToInt32(dr["CollectorNIK"]);
            model.CollectorFullname = dr.IsNull("CollectorFullName") ? null : dr["CollectorFullName"].ToString();

            model.FakturisNik = dr.IsNull("FakturisNIK") ? 0 : Convert.ToInt32(dr["FakturisNIK"]);
            model.FakturisFullname = dr.IsNull("FakturisFullName") ? null : dr["FakturisFullName"].ToString();

            model.SPVFakturisNik = dr.IsNull("SPVFakturisNIK") ? 0 : Convert.ToInt32(dr["SPVFakturisNIK"]);
            model.SPVFakturisFullname = dr.IsNull("SPVFakturisFullName") ? null : dr["SPVFakturisFullName"].ToString();

            model.ValidFromDate = dr.IsNull("ValidFrom") ? DateTime.MinValue : Convert.ToDateTime(dr["ValidFrom"]);
            model.FormattedValidFrom = model.ValidFromDate.ToString(AppConstant.DefaultFormatDate);
            model.ValidToDate = dr.IsNull("ValidTo") ? DateTime.MinValue : Convert.ToDateTime(dr["ValidTo"]);
            model.FormattedValidTo = model.ValidToDate.ToString(AppConstant.DefaultFormatDate);

            return model;
        }

        private List<HierTagihViewModel> GetListHierTagihViewModel(HttpPostedFileBase postedFile)
        {
            IWorkbook workbook = GetWorkbook(postedFile);

            if (workbook == null) return null;

            ISheet sheet = workbook.GetSheetAt(0);

            List<HierTagihViewModel> result = new List<HierTagihViewModel>();
            HierTagihViewModel model = null;

            object obj = null;
            int tempInt = 0;

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);

                if (row == null) continue;

                tempInt = 0;
                model = new HierTagihViewModel();

                obj = GetObjFromCell(row.GetCell(0));
                model.RayonCode = obj == null ? null : obj.ToString().Trim();
                obj = GetObjFromCell(row.GetCell(1));
                model.PlantCode = obj == null ? null : obj.ToString().Trim();

                obj = GetObjFromCell(row.GetCell(2));
                //model.NSMNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                {
                    model.NSMNik = tempInt;
                }
                else
                {
                    model.NSMNik = 0;
                }

                obj = GetObjFromCell(row.GetCell(3));
                model.NSMFullname = obj == null ? null : obj.ToString().Trim();

                obj = GetObjFromCell(row.GetCell(4));
                //model.ASMNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                {
                    model.ASMNik = tempInt;
                }
                else
                {
                    model.ASMNik = 0;
                }

                obj = GetObjFromCell(row.GetCell(5));
                model.ASMFullname = obj == null ? null : obj.ToString().Trim();

                obj = GetObjFromCell(row.GetCell(6));
                //model.FSSNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                {
                    model.FSSNik = tempInt;
                }
                else
                {
                    model.FSSNik = 0;
                }


                obj = GetObjFromCell(row.GetCell(7));
                model.FSSFullname = obj == null ? null : obj.ToString().Trim();

                obj = GetObjFromCell(row.GetCell(8));
                //model.SLMNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                {
                    model.SLMNik = tempInt;
                }
                else
                {
                    model.SLMNik = 0;
                }

                obj = GetObjFromCell(row.GetCell(9));
                model.SLMFullname = obj == null ? null : obj.ToString().Trim();

                obj = GetObjFromCell(row.GetCell(10));
                //model.CollectorNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                {
                    model.CollectorNik = tempInt;
                }
                else
                {
                    model.CollectorNik = 0;
                }

                obj = GetObjFromCell(row.GetCell(11));
                model.CollectorFullname = obj == null ? null : obj.ToString().Trim();

                obj = GetObjFromCell(row.GetCell(12));
                //model.FakturisNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                {
                    model.FakturisNik = tempInt;
                }
                else
                {
                    model.FakturisNik = 0;
                }

                obj = GetObjFromCell(row.GetCell(13));
                model.FakturisFullname = obj == null ? null : obj.ToString().Trim();

                obj = GetObjFromCell(row.GetCell(14));
                //model.SPVFakturisNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                {
                    model.SPVFakturisNik = tempInt;
                }
                else
                {
                    model.SPVFakturisNik = 0;
                }

                obj = GetObjFromCell(row.GetCell(15));
                model.SPVFakturisFullname = obj == null ? null : obj.ToString().Trim();

                if (!string.IsNullOrEmpty(model.RayonCode))
                    result.Add(model);
            }

            return result;
        }

        private SalesCustomerViewModel GetSalesRayonViewModel(RHDetail item, RHHeader rhh)
        {
            SalesCustomerViewModel model = new SalesCustomerViewModel()
            {
                RayonCode = item.RayonCode,
                CustomerCode = item.Customer,
                ValidFromDate = item.ValidFrom,
                ValidToDate = item.ValidTo
            };

            model.FormattedValidFrom = item.ValidFrom.ToString(_formattedDate);
            model.FormattedValidTo = item.ValidTo.ToString(_formattedDate);

            model.CustomerName = item.CustomerObj == null ? null : item.CustomerObj.CustomerName;

            if (rhh != null && rhh.SLMObj1 != null)
            {
                model.SLMNik = rhh.SLMObj1.NIK;
                model.SLMFullname = rhh.SLMObj1.FullName;
            }

            return model;
        }

        public AlertMessage ImportTagih(ImportTagihViewModel model)
        {
            AlertMessage alert = new AlertMessage();
            _logger.Write("info", DateTime.Now, string.Format("Upload Master Hier Tagih Total : {0}", model.ListHierTagih.Count), _userAuth.Fullname);
            if (!IsAccessible(ModuleCode.MasterTagih))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            int month = 0;
            int year = 0;

            try
            {
                string[] arr = model.FormattedValidDate.Split('-');

                month = Convert.ToInt16(arr[0]);
                year = Convert.ToInt16(arr[1]);
            }
            catch (Exception ex)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            if (month == 0 || year == 0)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            //IRepository<UploadHier> repoUploadHier = _unitOfWork.GetRepository<UploadHier>();
            IRepository<UploadHierTagih2> repoUploadHierTagih = _unitOfWork.GetRepository<UploadHierTagih2>();
            UploadHierTagih2 hier = null;

            foreach (var item in model.ListHierTagih)
            {
                hier = new UploadHierTagih2()
                {
                    RayonCode = item.RayonCode,
                    Plant = item.PlantCode,
                    NSM = item.NSMNik,
                    ASM = item.ASMNik,
                    FSS = item.FSSNik,
                    SLM = item.SLMNik,
                    Collector = item.CollectorNik.Value,
                    Fakturis = item.FakturisNik.Value,
                    SPVFakturis = item.SPVFakturisNik.Value,
                    CreatedByNIK = _userAuth.NIK
                };

                try
                {
                    repoUploadHierTagih.Insert(hier, true);
                }
                catch (Exception ex)
                {
                    item.Remarks = ex.Message;
                }
            }

            #region execute SP
            List<SqlParameter> listSqlParam = new List<SqlParameter>()
            {
                new SqlParameter("@inMonth", month),
                new SqlParameter("@inYear", year),
                new SqlParameter("@nik", _userAuth.NIK),
            };

            bool isError = false;
            string connString = GetConnectionString();

            Dictionary<int, dynamic> dc = null;

            try
            {
                //dc = SqlHelper.ExecuteProcedureWithReturnRecords(connString, "sp_uploadHier", listSqlParam);
                //dc = SqlHelper.ExecuteProcedureWithReturnRecords(connString, "sp_uploadHierTagih", listSqlParam);
                dc = SqlHelper.ExecuteProcedureWithReturnRecords(connString, "sp_uploadHierTagih_v2", listSqlParam);

                if (dc.ContainsKey(0))
                {
                    isError = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Write("error", DateTime.Now, ex.Message, _userAuth.Fullname, ex);
                isError = true;
            }
            #endregion

            if (isError)
            {
                // delete all records from table UploadML
                var condition = PredicateBuilder.True<UploadHierTagih2>().And(x => x.CreatedByNIK == _userAuth.NIK);
                repoUploadHierTagih.Delete(condition,true);
            }
            else if (dc != null)
            {
                dynamic val;

                DataTable dt = null;

                if (dc.TryGetValue(1, out val))
                {
                    dt = val as DataTable;
                }
            }

            _unitOfWork.Dispose();

            if (isError)
            {
                alert.Text = StaticMessage.ERR_SAVE_FAILED;
            }
            else
            {
                int numSuccess = model.ListHierTagih.Count(x => string.IsNullOrEmpty(x.Remarks));

                alert.Status = 1;
                alert.Text = string.Format(StaticMessage.SCS_IMPORT, numSuccess);
            }

            return alert;
        }

        public bool IsEditable()
        {
            if (_userAuth == null) return false;

            //if (!RoleCode.KaCab.Equals(_userAuth.RoleCode)) return false;

            if (RoleCode.KaCab.Equals(_userAuth.RoleCode) || RoleCode.NSM.Equals(_userAuth.RoleCode)) return true;

            return false;
        }

        public AlertMessage Preview(ImportTagihViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.MasterTagih))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            List<HierTagihViewModel> listVM = GetListHierTagihViewModel(model.InputFile);

            if (listVM == null)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            IRepository<FSS> repoFSS = _unitOfWork.GetRepository<FSS>();
            IRepository<SLM> repoSLM = _unitOfWork.GetRepository<SLM>();
            IRepository<TCollector> repoCollector = _unitOfWork.GetRepository<TCollector>();
            IRepository<TFakturis> repoFakturis = _unitOfWork.GetRepository<TFakturis>();
            IRepository<TSPVFakturis> repoSPVFakturis = _unitOfWork.GetRepository<TSPVFakturis>();
            FSS fss = null;
            SLM slm = null;
            TCollector collector = null;
            TFakturis fakturis = null;
            TSPVFakturis spvFakturis = null;

            DateTime dtMaxValidTo = DateTime.ParseExact(AppConstant.DefaultValidTo, AppConstant.DefaultFormatDate, _cultureInfo);

            Dictionary<int, string> dcExistingFSS = new Dictionary<int, string>();
            Dictionary<int, string> dcExistingSLM = new Dictionary<int, string>();
            Dictionary<long, string> dcExistingCollector = new Dictionary<long, string>();
            Dictionary<long, string> dcExistingFakturis = new Dictionary<long, string>();
            Dictionary<long, string> dcExistingSPVFakturis = new Dictionary<long, string>();

            DateTime today = DateTime.UtcNow.ToUtcID().Date;

            List<string> listRemarks = null;

            foreach (var item in listVM)
            {
                if (!string.IsNullOrEmpty(item.Remarks)) continue;

                if(dcExistingFSS.ContainsKey(item.FSSNik)
                    && dcExistingSLM.ContainsKey(item.SLMNik)
                    && item.CollectorNik != null
                    && dcExistingCollector.ContainsKey(item.CollectorNik.Value)
                    && item.FakturisNik != null
                    && dcExistingFakturis.ContainsKey(item.FakturisNik.Value)
                    && item.SPVFakturisNik != null
                    && dcExistingSPVFakturis.ContainsKey(item.SPVFakturisNik.Value))
                {
                    continue;
                }

                listRemarks = new List<string>();

                #region check FSS
                if (item.FSSNik != 0 && !dcExistingFSS.ContainsKey(item.FSSNik))
                {
                    repoFSS.Condition = PredicateBuilder.True<FSS>().And(x => x.NIK == item.FSSNik);
                    repoFSS.OrderBy = new SqlOrderBy("CreatedOn", SqlOrderType.Descending);

                    fss = repoFSS.Find().FirstOrDefault();

                    if (fss == null)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_FSS_NOT_FOUND, item.FSSNik));
                    }
                    else if (fss.ValidTo < today)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_FSS_NOT_ACTIVE, item.FSSNik));
                    }
                    else
                    {
                        dcExistingFSS[fss.NIK] = fss.FullName;
                    }
                }
                #endregion

                #region check SLM
                if (item.SLMNik != 0 && !dcExistingSLM.ContainsKey(item.SLMNik))
                {
                    repoSLM.Condition = PredicateBuilder.True<SLM>().And(x => x.NIK == item.SLMNik);
                    repoSLM.OrderBy = new SqlOrderBy("CreatedOn", SqlOrderType.Descending);

                    slm = repoSLM.Find().FirstOrDefault();

                    if (slm == null)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_SLM_NOT_FOUND, item.SLMNik));
                    }
                    else if (slm.ValidTo < today)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_SLM_NOT_ACTIVE, item.SLMNik));
                    }
                    else
                    {
                        dcExistingSLM[slm.NIK] = slm.FullName;
                    }
                }
                #endregion

                #region check Controller
                if (item.CollectorNik != null && item.CollectorNik.Value != 0 && !dcExistingCollector.ContainsKey(item.CollectorNik.Value))
                {
                    repoCollector.Condition = PredicateBuilder.True<TCollector>().And(x => x.NIK == item.CollectorNik.Value);
                    repoCollector.OrderBy = new SqlOrderBy("CreatedOn", SqlOrderType.Descending);

                    collector = repoCollector.Find().FirstOrDefault();

                    if (collector == null)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_COLLECTOR_NOT_FOUND, item.CollectorNik.Value));
                    }
                    else if (collector.ValidTo < today)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_COLLECTOR_NOT_ACTIVE, item.CollectorNik.Value));
                    }
                    else
                    {
                        dcExistingCollector[collector.NIK] = collector.FULLNAME;
                    }
                }
                #endregion

                #region check Fakturis
                if (item.FakturisNik != null && item.FakturisNik.Value != 0 && !dcExistingFakturis.ContainsKey(item.FakturisNik.Value))
                {
                    repoFakturis.Condition = PredicateBuilder.True<TFakturis>().And(x => x.NIK == item.FakturisNik.Value);
                    repoFakturis.OrderBy = new SqlOrderBy("CreatedOn", SqlOrderType.Descending);

                    fakturis = repoFakturis.Find().FirstOrDefault();

                    if (fakturis == null)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_FAKTURIS_NOT_FOUND, item.FakturisNik.Value));
                    }
                    else if (fakturis.ValidTo < today)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_FAKTURIS_NOT_ACTIVE, item.FakturisNik.Value));
                    }
                    else
                    {
                        dcExistingFakturis[fakturis.NIK] = fakturis.FULLNAME;
                    }
                }
                #endregion

                #region check SPV Fakturis
                if (item.SPVFakturisNik != null && item.SPVFakturisNik.Value != 0 && !dcExistingFakturis.ContainsKey(item.SPVFakturisNik.Value))
                {
                    repoSPVFakturis.Condition = PredicateBuilder.True<TSPVFakturis>().And(x => x.NIK == item.SPVFakturisNik.Value);
                    repoSPVFakturis.OrderBy = new SqlOrderBy("CreatedOn", SqlOrderType.Descending);

                    spvFakturis = repoSPVFakturis.Find().FirstOrDefault();

                    if (spvFakturis == null)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_SPV_FAKTURIS_NOT_FOUND, item.SPVFakturisNik.Value));
                    }
                    else if (spvFakturis.ValidTo < today)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_SPV_FAKTURIS_NOT_ACTIVE, item.SPVFakturisNik.Value));
                    }
                    else
                    {
                        dcExistingSPVFakturis[spvFakturis.NIK] = spvFakturis.FULLNAME;
                    }
                }
                #endregion

                if (listRemarks.Count > 0)
                {
                    item.Remarks = String.Join("; ", listRemarks.ToArray());
                }
            }

            int numError = listVM.Count(x=>! string.IsNullOrEmpty(x.Remarks));

            if (numError > 0)
            {
                alert.Status = 0;
                alert.Text = string.Format(StaticMessage.ERR_PREVIEW_CONTAINS_ERROR, numError);
            }

            model.ListHierTagih = listVM;

            alert.Data = model;

            return alert;
        }
    }
}
