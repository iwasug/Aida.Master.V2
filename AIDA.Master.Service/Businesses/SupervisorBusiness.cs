using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Infrastucture.Data;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using Radyalabs.Core.Helper;
using Radyalabs.Core.Repository;

namespace AIDA.Master.Service.Businesses
{
    public class SupervisorBusiness : BaseBusiness
    {
        public AlertMessage Add(SupervisorViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.Supervisor))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (!IsEditable())
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            //model.ValidFromDate = DateTime.ParseExact(model.FormattedValidFrom, AppConstant.DefaultFormatDate, _cultureInfo);
            //model.ValidToDate = DateTime.ParseExact(model.FormattedValidTo, AppConstant.DefaultFormatDate, _cultureInfo);

            IRepository<FSS> repo = _unitOfWork.GetRepository<FSS>();
            FSS item = null;

            repo.Condition = PredicateBuilder.True<FSS>().And(x=>x.NIK == model.NIK);

            item = repo.Find().FirstOrDefault();

            if (item != null)
            {
                alert.Text = string.Format(StaticMessage.ERR_NIK_EXIST, model.NIK);
                return alert;
            }
    
            DateTime now = DateTime.UtcNow.ToUtcID();
            DateTime validFrom = now.AddMonths(-1).AddDays(1).Date;
            DateTime validTo = new DateTime(9999, 12, 31);

            item = new FSS()
            {
                NIK = model.NIK,
                FullName = model.Fullname,
                Role = model.IsRole,
                DefaultRayonType = model.DefaultRayonType,
                ValidFrom = validFrom,
                ValidTo = validTo,
                CreatedBy = _userAuth.NIK.ToString(),
                CreatedOn = now,
                UpdatedBy = _userAuth.NIK.ToString(),
                UpdatedOn = now,
            };

            try
            {
                _unitOfWork.BeginTransaction();

                repo.Insert(item);

                _unitOfWork.Commit();

                alert.Status = 1;
                alert.Text = string.Format(StaticMessage.SCS_ADD_MASTER, model.NIK, model.Fullname);
            }
            catch (Exception ex)
            {
                _logger.Write("error", DateTime.Now, ex.Message, _userAuth.Fullname, ex);
                alert.Text = StaticMessage.ERR_SAVE_FAILED;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return alert;
        }

        public AlertMessage Edit(SupervisorViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.Supervisor))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (!IsEditable())
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            IRepository<FSS> repo = _unitOfWork.GetRepository<FSS>();
            repo.Condition = PredicateBuilder.True<FSS>().And(x => x.NIK == model.NIK);

            FSS item = repo.Find().FirstOrDefault();

            if (item == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            DateTime now = DateTime.UtcNow.ToUtcID();

            item.FullName = model.Fullname;
            //item.Role = model.IsRole;
            item.DefaultRayonType = model.DefaultRayonType;
            item.ValidFrom = DateTime.Parse(model.FormattedValidFrom);
            item.ValidTo = DateTime.Parse(model.FormattedValidTo);
            item.UpdatedBy = _userAuth.NIK.ToString();
            item.UpdatedOn = now;

            try
            {
                _unitOfWork.BeginTransaction();

                repo.Update(item);

                _unitOfWork.Commit();

                alert.Status = 1;
                alert.Text = string.Format(StaticMessage.SCS_EDIT, item.NIK, item.FullName);
            }
            catch (Exception ex)
            {
                _logger.Write("error", DateTime.Now, ex.Message, _userAuth.Fullname, ex);
                alert.Text = StaticMessage.ERR_SAVE_FAILED;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return alert;
        }

        public JDatatableResponse GetDatatable(JDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "CreatedOn"
                ,"NIK"
                ,"Fullname"
                ,"Role"
                ,"DefaultRayonType"
                ,"ValidFrom"
                ,"ValidTo"
            };

            if (_userAuth == null) return result;

            DateTime today = DateTime.UtcNow.ToUtcID().Date;

            IRepository<RHHeader> repo = _unitOfWork.GetRepository<RHHeader>();
            repo.Includes = new string[] { "FSSObj1" };
            repo.Condition = PredicateBuilder.True<RHHeader>().And(x=>x.ValidTo >= today);

            if (RoleCode.BUM.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.BUM == _userAuth.NIK);
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.NSM == _userAuth.NIK);
            }
            else if (RoleCode.ASM.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.ASM == _userAuth.NIK);
            }
            else
            {
                return result;
            }

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                repo.Condition = repo.Condition.And(x => x.FSSObj1.FullName.Contains(model.Keyword)
                    || x.FSSObj1.DefaultRayonType.Contains(model.Keyword));
            }

            SetDatatableRepository(model, arrOrderColumn, ref repo, ref result);

            if (model.Length > -1 && result.TotalRecords == 0)
            {
                return result;
            }

            List<RHHeader> listItem = repo.Find();

            if (listItem == null) return result;

            List<SupervisorViewModel> listData = new List<SupervisorViewModel>();

            foreach (var item in listItem)
            {
                if (item.FSSObj1 == null) continue;

                listData.Add(GetSupervisorViewModel(item));
            }

            result.Data = listData;

            return result;
        }

        public JDatatableResponse GetDatatableByQuery(JDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "Fullname"
                ,"Fullname"
                ,"AllowedByNIK"
                ,"UploadValidTo"
                ,"NIK"
                ,"Fullname"
                ,"DefaultRayonType"
                ,"Role"
                ,"ValidFrom"
                ,"ValidTo"
            };

            if (_userAuth == null) return result;

//            string query = "SELECT [:SELECT]" + System.Environment.NewLine +
//"FROM (" + System.Environment.NewLine +
//"	SELECT fss.NIK, fss.FullName, fss.Role, fss.DefaultRayonType, fss.AllowedByNIK, fss.UploadValidTo" + System.Environment.NewLine +
//"	FROM RHHeader rhh" + System.Environment.NewLine +
//"	LEFT JOIN FSS fss ON fss.NIK = rhh.FSS" + System.Environment.NewLine +
//"	WHERE 1=1" + System.Environment.NewLine +
//"		AND rhh.ValidTo >= GETDATE()" + System.Environment.NewLine +
//"   [:CONDITION]" + System.Environment.NewLine +
//"	UNION" + System.Environment.NewLine +
//"	SELECT NIK, FullName, Role, DefaultRayonType, AllowedByNIK, UploadValidTo" + System.Environment.NewLine +
//"	FROM FSS" + System.Environment.NewLine +
//"	WHERE 1=1" + System.Environment.NewLine +
//"       AND ValidTo >= GETDATE()" + System.Environment.NewLine +
//"       AND CreatedBy = '" + _userAuth.NIK + "'" + System.Environment.NewLine +
//") a";

            string query = "SELECT [:SELECT]" + System.Environment.NewLine +
"FROM (" + System.Environment.NewLine +
"	SELECT DISTINCT FSS" + System.Environment.NewLine +
"	FROM RTHeader" + System.Environment.NewLine +
"	WHERE 1=1" + System.Environment.NewLine +
"[:PRE_CONDITION]" + System.Environment.NewLine +
"	UNION" + System.Environment.NewLine +
"	SELECT DISTINCT FSS" + System.Environment.NewLine +
"	FROM RHHeader" + System.Environment.NewLine +
"	WHERE 1=1" + System.Environment.NewLine +
"[:PRE_CONDITION]" + System.Environment.NewLine +
//"   UNION SELECT DISTINCT NIK" + System.Environment.NewLine +
//"   FROM FSS" + System.Environment.NewLine +
//"   WHERE 1=1" + System.Environment.NewLine +
//"        AND CreatedBy = '"+_userAuth.NIK.ToString()+ "'" + System.Environment.NewLine +
") t" + System.Environment.NewLine +
"LEFT JOIN FSS fss ON fss.NIK = t.FSS" + System.Environment.NewLine +
"WHERE 1=1" + System.Environment.NewLine +
"	AND fss.NIK IS NOT NULL" + System.Environment.NewLine +
"	AND ValidTo >= GETDATE()" + System.Environment.NewLine +
"[:CONDITION]";

            string selectColumn = "DISTINCT *";

            string selectCount = "COUNT(DISTINCT FSS) AS NumRows";

            if (_userAuth == null) return result;

            string preCondition = "";
            string condition = "";
            Dictionary<string, object> dcParams = new Dictionary<string, object>();

            if (RoleCode.BUM.Equals(_userAuth.RoleCode))
            {
                preCondition += "	    AND BUM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode) || RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                preCondition += "	    AND NSM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.ASM.Equals(_userAuth.RoleCode))
            {
                preCondition += "	    AND ASM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else
            {
                return result;
            }

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                model.Keyword = model.Keyword.ToLower();

                condition += System.Environment.NewLine;
                condition += "	AND (fss.NIK LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(fss.FullName) LIKE '%[:KEYWORD]%')";

                condition = condition.Replace("[:KEYWORD]", model.Keyword);
            }

            query = query.Replace("[:PRE_CONDITION]", preCondition);
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

            List<SupervisorViewModel> listData = new List<SupervisorViewModel>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    listData.Add(GetSupervisorViewModel(dr));
                }
            }

            result.Data = listData;

            return result;
        }

        public SupervisorViewModel GetDetail(int id)
        {
            IRepository<FSS> repo = _unitOfWork.GetRepository<FSS>();
            repo.Condition = PredicateBuilder.True<FSS>().And(x => x.NIK == id);

            FSS item = repo.Find().FirstOrDefault();

            if (item == null) return null;

            return GetSupervisorViewModel(item);
        }

        private SupervisorViewModel GetSupervisorViewModel(RHHeader item)
        {
            SupervisorViewModel model = new SupervisorViewModel()
            {
                NIK = item.FSSObj1.NIK,
                Fullname = item.FSSObj1.FullName,
                IsRole = item.FSSObj1.Role,
                DefaultRayonType = item.FSSObj1.DefaultRayonType,
                ValidFromDate = item.ValidFrom,
                ValidToDate = item.ValidTo
            };

            model.FormattedValidFrom = item.ValidFrom.ToString(AppConstant.DefaultFormatDate);
            model.FormattedValidTo = item.ValidTo.ToString(AppConstant.DefaultFormatDate);

            return model;
        }

        private SupervisorViewModel GetSupervisorViewModel(DataRow dr)
        {
            SupervisorViewModel model = new SupervisorViewModel();

            model.NIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]);
            model.Fullname = dr.IsNull("FullName") ? null : dr["FullName"].ToString();
            model.IsRole = dr.IsNull("Role") ? false : Convert.ToBoolean(dr["Role"]);
            model.DefaultRayonType = dr.IsNull("DefaultRayonType") ? null : dr["DefaultRayonType"].ToString();
            model.AllowWriteBy = dr.IsNull("AllowedByNIK") ? (int?)null : Convert.ToInt32(dr["AllowedByNIK"]);
            model.IsAbleToUpload = model.AllowWriteBy == null ? false : true;
            model.UploadValidTo = dr.IsNull("UploadValidTo") ? (DateTime?)null : Convert.ToDateTime(dr["UploadValidTo"]);
            model.FormattedUploadValidTo = model.UploadValidTo == null ? "" : model.UploadValidTo.Value.ToString(AppConstant.DefaultFormatDate);

            if (!dr.IsNull("ValidFrom"))
            {
                model.ValidFromDate = Convert.ToDateTime(dr["ValidFrom"]);
                model.FormattedValidFrom = model.ValidFromDate.ToString(AppConstant.DefaultFormatDate);
            }

            if (!dr.IsNull("ValidTo"))
            {
                model.ValidToDate = Convert.ToDateTime(dr["ValidTo"]);
                model.FormattedValidTo = model.ValidToDate.ToString(AppConstant.DefaultFormatDate);
            }

            return model;
        }

        private SupervisorViewModel GetSupervisorViewModel(FSS item)
        {
            SupervisorViewModel model = new SupervisorViewModel();

            model.NIK = item.NIK;
            model.Fullname = item.FullName;
            model.IsRole = item.Role;
            model.DefaultRayonType = item.DefaultRayonType;
            model.AllowWriteBy = item.AllowedByNIK;
            model.IsAbleToUpload = item.AllowedByNIK == null ? false : true;
            model.UploadValidTo = item.UploadValidTo;
            model.FormattedUploadValidTo = model.UploadValidTo == null ? null : model.UploadValidTo.Value.ToString(AppConstant.DefaultFormatDate);

            model.ValidFromDate = item.ValidFrom;
            model.ValidToDate = item.ValidTo;

            model.FormattedValidFrom = model.ValidFromDate.ToString(AppConstant.DefaultFormatDate);
            model.FormattedValidTo = model.ValidToDate.ToString(AppConstant.DefaultFormatDate);

            return model;
        }

        public bool IsEditable()
        {
            if (RoleCode.KaCab.Equals(_userAuth.RoleCode) || RoleCode.ASM.Equals(_userAuth.RoleCode))
            {
                return true;
            }

            return false;
        }

        public AlertMessage UpdateAccess(SupervisorUpdateAccessViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.Supervisor))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (model == null || model.ListData.Count == 0)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            IRepository<FSS> repoFSS = _unitOfWork.GetRepository<FSS>();

            var orCondition = PredicateBuilder.False<FSS>();

            foreach (var item in model.ListData)
            {
                orCondition = orCondition.Or(x=>x.NIK == item.NIK);
            }

            repoFSS.Condition = PredicateBuilder.True<FSS>().And(orCondition);

            List<FSS> listFSS = repoFSS.Find();

            if (listFSS == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            SupervisorNIKValUpdateAccess valAccess = null;
            List<SupervisorNIKValUpdateAccess> listValAccess = new List<SupervisorNIKValUpdateAccess>();

            try
            {
                _unitOfWork.BeginTransaction();

                foreach (var fss in listFSS)
                {
                    valAccess = model.ListData.FirstOrDefault(x => x.NIK == fss.NIK);

                    if (valAccess != null)
                    {
                        if (valAccess.IsAllowed)
                        {
                            fss.AllowedByNIK = _userAuth.NIK;
                            fss.UploadValidTo = valAccess.UploadValidTo;
                        }
                        else
                        {
                            fss.AllowedByNIK = null;
                            fss.UploadValidTo = null;
                        }

                        repoFSS.Update(fss);
                    }

                    listValAccess.Add(new SupervisorNIKValUpdateAccess()
                    {
                        NIK = fss.NIK,
                        IsAllowed = fss.AllowedByNIK == null ? false : true,
                        FormattedUploadValidTo = fss.UploadValidTo == null ? "" : fss.UploadValidTo.Value.ToString(AppConstant.DefaultFormatDate)
                    });
                }

                _unitOfWork.Commit();

                alert.Status = 1;
                alert.Data = listValAccess;
            }
            catch (Exception ex)
            {
                _logger.Write("error", DateTime.Now, ex.Message, _userAuth.Fullname, ex);
                alert.Text = StaticMessage.ERR_SAVE_FAILED;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return alert;
        }
    }
}
