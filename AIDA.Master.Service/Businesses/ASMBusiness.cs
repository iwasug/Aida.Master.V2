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
    public class ASMBusiness : BaseBusiness
    {
        public AlertMessage Add(ASMViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.ASM))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (!IsEditable())
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            DateTime now = DateTime.UtcNow.ToUtcID();
            DateTime validFrom = now.AddMonths(-1).AddDays(1).Date;
            DateTime validTo = new DateTime(9999, 12, 31);

            //model.ValidFromDate = DateTime.ParseExact(model.FormattedValidFrom, AppConstant.DefaultFormatDate, _cultureInfo);
            //model.ValidToDate = DateTime.ParseExact(model.FormattedValidTo, AppConstant.DefaultFormatDate, _cultureInfo);

            IRepository<ASM> repo = _unitOfWork.GetRepository<ASM>();
            ASM item = null;

            #region check NIK exist
            repo.Condition = PredicateBuilder.True<ASM>().And(x => x.NIK == model.NIK);

            item = repo.Find().FirstOrDefault();

            if (item != null)
            {
                alert.Text = string.Format(StaticMessage.ERR_NIK_EXIST, item.NIK);
                return alert;
            }
            #endregion

            item = new ASM()
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
                alert.Text = string.Format(StaticMessage.SCS_ADD_MASTER, item.NIK, item.FullName);
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

        public AlertMessage Edit(ASMViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.ASM))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (!IsEditable())
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            IRepository<ASM> repo = _unitOfWork.GetRepository<ASM>();
            repo.Condition = PredicateBuilder.True<ASM>().And(x => x.NIK == model.NIK);

            ASM item = repo.Find().FirstOrDefault();

            if (item == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            DateTime now = DateTime.UtcNow.ToUtcID();

            item.FullName = model.Fullname;
            item.DefaultRayonType = model.DefaultRayonType;
            item.Role = model.IsRole;
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

        private ASMViewModel GetASMViewModel(DataRow dr)
        {
            ASMViewModel model = new ASMViewModel();

            model.NIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]);
            model.Fullname = dr.IsNull("FullName") ? null : dr["FullName"].ToString();
            model.DefaultRayonType = dr.IsNull("DefaultRayonType") ? null : dr["DefaultRayonType"].ToString();
            model.IsRole = dr.IsNull("Role") ? false : Convert.ToBoolean(dr["Role"]);
            model.AllowWriteBy = dr.IsNull("AllowedByNIK") ? (int?)null : Convert.ToInt32(dr["AllowedByNIK"]);
            model.IsAbleToUpload = model.AllowWriteBy == null ? false : true;
            model.UploadValidTo = dr.IsNull("UploadValidTo") ? (DateTime?)null : Convert.ToDateTime(dr["UploadValidTo"]);
            model.FormattedUploadValidTo = model.UploadValidTo == null ? "" : model.UploadValidTo.Value.ToString(AppConstant.DefaultFormatDate);
            model.ValidFromDate = Convert.ToDateTime(dr["ValidFrom"]);
            model.ValidToDate = Convert.ToDateTime(dr["ValidTo"]);

            model.FormattedValidFrom = model.ValidFromDate.ToString(AppConstant.DefaultFormatDate);
            model.FormattedValidTo = model.ValidToDate.ToString(AppConstant.DefaultFormatDate);

            return model;
        }

        private ASMViewModel GetASMViewModel(ASM item)
        {
            ASMViewModel model = new ASMViewModel();

            model.NIK = item.NIK;
            model.Fullname = item.FullName;
            model.DefaultRayonType = item.DefaultRayonType;
            model.IsRole = item.Role;
            model.AllowWriteBy = item.AllowedByNIK;
            model.IsAbleToUpload = item.AllowedByNIK == null ? false : true;
            model.UploadValidTo = item.UploadValidTo;
            model.FormattedUploadValidTo = item.UploadValidTo == null ? null : item.UploadValidTo.Value.ToString(AppConstant.DefaultFormatDate);
            model.ValidFromDate = item.ValidFrom;
            model.ValidToDate = item.ValidTo;
            model.FormattedValidFrom = model.ValidFromDate.ToString(AppConstant.DefaultFormatDate);
            model.FormattedValidTo = model.ValidToDate.ToString(AppConstant.DefaultFormatDate);

            return model;
        }

        public JDatatableResponse GetDatatableByQuery(JDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "asm.Fullname"
                ,"asm.Fullname"
                ,"asm.AllowedByNIK"
                ,"asm.UploadValidTo"
                ,"asm.NIK"
                ,"asm.Fullname"
                ,"asm.DefaultRayonType"
                ,"asm.Role"
                ,"asm.ValidFrom"
                ,"asm.ValidTo"
            };

            if (_userAuth == null) return result;

            string query = "SELECT [:SELECT]" + System.Environment.NewLine +
"FROM (" + System.Environment.NewLine +
"	SELECT DISTINCT ASM" + System.Environment.NewLine +
"	FROM RTHeader" + System.Environment.NewLine +
"	WHERE 1=1" + System.Environment.NewLine +
"		AND ValidTo >= GETDATE()" + System.Environment.NewLine +
"       [:PRE_CONDITION]" + System.Environment.NewLine +
"	UNION" + System.Environment.NewLine +
"	SELECT DISTINCT ASM" + System.Environment.NewLine +
"	FROM RHHeader" + System.Environment.NewLine +
"	WHERE 1=1" + System.Environment.NewLine +
"		AND ValidTo >= GETDATE()" + System.Environment.NewLine +
"       [:PRE_CONDITION]" + System.Environment.NewLine +
"    UNION" + System.Environment.NewLine +
"	    SELECT DISTINCT NIK" + System.Environment.NewLine +
"	    FROM ASM" + System.Environment.NewLine +
"	    WHERE 1=1" + System.Environment.NewLine +
"		    AND CreatedBy = '"+_userAuth.NIK.ToString()+"'" + System.Environment.NewLine +
") t" + System.Environment.NewLine +
"LEFT JOIN ASM asm ON asm.NIK = t.ASM" + System.Environment.NewLine +
"WHERE 1=1" + System.Environment.NewLine +
"	AND asm.NIK IS NOT NULL" + System.Environment.NewLine +
"   [:CONDITION]";

            string selectColumn = "DISTINCT *";

            string selectCount = "COUNT(DISTINCT ASM) AS NumRows";

            if (_userAuth == null) return result;

            string condition = "";
            string preCondition = "";
            Dictionary<string, object> dcParams = new Dictionary<string, object>();

            if (RoleCode.NSM.Equals(_userAuth.RoleCode) || RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                preCondition += "AND NSM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.BUM.Equals(_userAuth.RoleCode))
            {
                //preCondition += "	    AND BUM = @nik";
                //dcParams["@nik"] = _userAuth.NIK;
            }
            else
            {
                return result;
            }

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                model.Keyword = model.Keyword.ToLower();

                condition += System.Environment.NewLine;
                condition += "AND (asm.NIK LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"   OR LOWER(asm.FullName) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"   OR LOWER(asm.DefaultRayonType) LIKE '%[:KEYWORD]%')";

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

            List<ASMViewModel> listData = new List<ASMViewModel>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    listData.Add(GetASMViewModel(dr));
                }
            }

            result.Data = listData;

            return result;
        }

        public ASMViewModel GetDetail(int id)
        {
            IRepository<ASM> repo = _unitOfWork.GetRepository<ASM>();
            repo.Condition = PredicateBuilder.True<ASM>().And(x=>x.NIK == id);

            ASM item = repo.Find().FirstOrDefault();

            if (item == null) return null;

            return GetASMViewModel(item);
        }

        public bool IsEditable()
        {
            if (_userAuth != null && RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                return true;
            }

            return false;
        }

        public AlertMessage UpdateAccess(ASMUpdateAccessViewModel model)
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

            IRepository<ASM> repoASM = _unitOfWork.GetRepository<ASM>();

            var orCondition = PredicateBuilder.False<ASM>();

            foreach (var item in model.ListData)
            {
                orCondition = orCondition.Or(x => x.NIK == item.NIK);
            }

            repoASM.Condition = PredicateBuilder.True<ASM>().And(orCondition);

            List<ASM> listASM = repoASM.Find();

            if (listASM == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            ASMNIKValUpdateAccess valAccess = null;
            List<ASMNIKValUpdateAccess> listValAccess = new List<ASMNIKValUpdateAccess>();

            try
            {
                _unitOfWork.BeginTransaction();

                foreach (var asm in listASM)
                {
                    valAccess = model.ListData.FirstOrDefault(x => x.NIK == asm.NIK);

                    if (valAccess != null)
                    {
                        if (valAccess.IsAllowed)
                        {
                            asm.AllowedByNIK = _userAuth.NIK;
                            asm.UploadValidTo = valAccess.UploadValidTo;
                        }
                        else
                        {
                            asm.AllowedByNIK = null;
                            asm.UploadValidTo = null;
                        }

                        repoASM.Update(asm);
                    }

                    listValAccess.Add(new ASMNIKValUpdateAccess()
                    {
                        NIK = asm.NIK,
                        IsAllowed = asm.AllowedByNIK == null ? false : true,
                        FormattedUploadValidTo = asm.UploadValidTo == null ? "" : asm.UploadValidTo.Value.ToString(AppConstant.DefaultFormatDate)
                    });
                }

                _unitOfWork.Commit();

                alert.Status = 1;
                alert.Data = listValAccess;
            }
            catch (Exception ex)
            {
                _logger.Write("Error", DateTime.Now, ex.Message, "System", ex);
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
