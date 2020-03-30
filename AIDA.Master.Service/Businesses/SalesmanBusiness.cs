using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    public class SalesmanBusiness : BaseBusiness
    {
        public AlertMessage Add(SalesmanViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.Salesman))
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

            IRepository<SLM> repo = _unitOfWork.GetRepository<SLM>();
            SLM item = null;

            #region check NIK exist
            repo.Condition = PredicateBuilder.True<SLM>().And(x=>x.NIK == model.NIK);

            item = repo.Find().FirstOrDefault();

            if (item != null)
            {
                alert.Text = string.Format(StaticMessage.ERR_NIK_EXIST, item.NIK);
                return alert;
            }
            #endregion

            item = new SLM()
            {
                NIK = model.NIK,
                FullName = model.Fullname,
                Role = model.IsRole,
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

        public AlertMessage Edit(SalesmanViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.Salesman))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (!IsEditable())
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            IRepository<SLM> repo = _unitOfWork.GetRepository<SLM>();
            repo.Condition = PredicateBuilder.True<SLM>().And(x=>x.NIK == model.NIK);

            SLM slm = repo.Find().FirstOrDefault();

            if (slm == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            DateTime now = DateTime.UtcNow.ToUtcID();

            //IRepository<RHHeader> repoHeader = _unitOfWork.GetRepository<RHHeader>();
            //repoHeader.Condition = PredicateBuilder.True<RHHeader>().And(x => x.SLM == model.NIK);

            //RHHeader rhHeader = repoHeader.Find().FirstOrDefault();

            //if (rhHeader != null)
            //{
            //    rhHeader.ValidFrom = DateTime.Parse(model.FormattedValidFrom);
            //    rhHeader.ValidTo = DateTime.Parse(model.FormattedValidTo);
            //    rhHeader.UpdatedBy = _userAuth.NIK.ToString();
            //    rhHeader.UpdatedOn = now;
            //}

            slm.FullName = model.Fullname;
            //slm.Role = model.IsRole;
            slm.ValidFrom = DateTime.Parse(model.FormattedValidFrom);
            slm.ValidTo = DateTime.Parse(model.FormattedValidTo);
            slm.UpdatedBy = _userAuth.NIK.ToString();
            slm.UpdatedOn = now;

            try
            {
                _unitOfWork.BeginTransaction();

                repo.Update(slm);
                //repoHeader.Update(rhHeader);

                _unitOfWork.Commit();

                alert.Status = 1;
                alert.Text = string.Format(StaticMessage.SCS_EDIT, slm.NIK, slm.FullName);
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

        public SalesmanViewModel GetDetail(int id)
        {
            IRepository<SLM> repo = _unitOfWork.GetRepository<SLM>();
            repo.Condition = PredicateBuilder.True<SLM>().And(x=>x.NIK == id);

            SLM slm = repo.Find().FirstOrDefault();

            if (slm == null) return null;

            SalesmanViewModel model = GetSalesmanViewModel(slm);

            return model;
        }

        public JDatatableResponse GetDatatable(JDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "CreatedOn"
                ,"NIK"
                ,"Fullname"
                ,"Role"
                ,"ValidFrom"
                ,"ValidTo"
            };

            if (_userAuth == null) return result;

            DateTime today = DateTime.UtcNow.ToUtcID().Date;

            IRepository<RHHeader> repo = _unitOfWork.GetRepository<RHHeader>();
            repo.Includes = new string[] { "SLMObj1" };
            repo.Condition = PredicateBuilder.True<RHHeader>().And(x=>x.ValidTo >= today && x.SLM == _userAuth.NIK);

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                repo.Condition = repo.Condition.And(x => x.SLMObj1.FullName.Contains(model.Keyword));
            }

            SetDatatableRepository(model, arrOrderColumn, ref repo, ref result);

            if (model.Length > -1 && result.TotalRecords == 0)
            {
                return result;
            }

            List<RHHeader> listItem = repo.Find();

            if (listItem == null) return result;

            List<SalesmanViewModel> listData = new List<SalesmanViewModel>();

            foreach (var item in listItem)
            {
                if (item.SLMObj1 == null) continue;

                listData.Add(GetSalesmanViewModel(item));
            }

            result.Data = listData;

            return result;
        }

        public JDatatableResponse GetDatatableByQuery(JDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "Fullname"
                ,"Fullname"
                ,"NIK"
                ,"Fullname"
                ,"Role"
                ,"ValidFrom"
                ,"ValidTo"
            };

            if (_userAuth == null) return result;

            string query = "SELECT [:SELECT]" + System.Environment.NewLine +
"FROM (" + System.Environment.NewLine +
"	SELECT slm.NIK, slm.FullName, slm.Role, slm.ValidFrom, slm.ValidTo" + System.Environment.NewLine +
"	FROM RHHeader rhh" + System.Environment.NewLine +
"	LEFT JOIN SLM slm ON slm.NIK = rhh.SLM" + System.Environment.NewLine +
"	WHERE 1=1" + System.Environment.NewLine +
"   [:CONDITION]" + System.Environment.NewLine +
//"	UNION" + System.Environment.NewLine +
//"	SELECT NIK, FullName, Role, ValidFrom, ValidTo" + System.Environment.NewLine +
//"	FROM SLM" + System.Environment.NewLine +
//"	WHERE 1=1" + System.Environment.NewLine +
//"       AND CreatedBy = '" +_userAuth.NIK+"'" + System.Environment.NewLine +
") a" + System.Environment.NewLine +
"WHERE 1=1" + System.Environment.NewLine +
"   AND VALIDTO >= GETDATE()" + System.Environment.NewLine +
"[:CONDITION2]";

            string selectColumn = "DISTINCT *";

            string selectCount = "COUNT(DISTINCT NIK) AS NumRows";

            if (_userAuth == null) return result;

            string condition = "";
            string condition2 = "";
            Dictionary<string, object> dcParams = new Dictionary<string, object>();

            if (RoleCode.BUM.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rhh.BUM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode) || RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rhh.NSM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.ASM.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rhh.ASM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.FSS.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rhh.FSS = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else
            {
                return result;
            }

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                model.Keyword = model.Keyword.ToLower();

                condition2 += "	AND (NIK LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(FullName) LIKE '%[:KEYWORD]%')";

                condition2 = condition2.Replace("[:KEYWORD]", model.Keyword);
            }

            query = query.Replace("[:CONDITION]", condition);
            query = query.Replace("[:CONDITION2]", condition2);

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

            List<SalesmanViewModel> listData = new List<SalesmanViewModel>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    listData.Add(GetSalesmanViewModel(dr));
                }
            }

            result.Data = listData;

            return result;
        }

        private SalesmanViewModel GetSalesmanViewModel(DataRow dr)
        {
            SalesmanViewModel model = new SalesmanViewModel();

            model.NIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]);
            model.Fullname = dr.IsNull("FullName") ? null : dr["FullName"].ToString();
            model.IsRole = dr.IsNull("Role") ? false : Convert.ToBoolean(dr["Role"]);

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

        private SalesmanViewModel GetSalesmanViewModel(RHHeader item)
        {
            SalesmanViewModel model = new SalesmanViewModel()
            {
                NIK = item.SLMObj1.NIK,
                Fullname = item.SLMObj1.FullName,
                IsRole = item.SLMObj1.Role,
                ValidFromDate = item.ValidFrom,
                ValidToDate = item.ValidTo
            };

            model.FormattedValidFrom = item.ValidFrom.ToString(AppConstant.DefaultFormatDate);
            model.FormattedValidTo = item.ValidTo.ToString(AppConstant.DefaultFormatDate);

            return model;
        }

        private SalesmanViewModel GetSalesmanViewModel(SLM slm)
        {
            SalesmanViewModel model = new SalesmanViewModel();

            model.NIK = slm.NIK;
            model.Fullname = slm.FullName;
            model.IsRole = slm.Role;
            model.ValidFromDate = slm.ValidFrom;
            model.ValidToDate = slm.ValidTo;

            model.FormattedValidFrom = model.ValidFromDate.ToString(AppConstant.DefaultFormatDate);
            model.FormattedValidTo = model.ValidToDate.ToString(AppConstant.DefaultFormatDate);

            return model;
        }

        public bool IsEditable()
        {
            if (RoleCode.KaCab.Equals(_userAuth.RoleCode) || RoleCode.ASM.Equals(_userAuth.RoleCode) || RoleCode.FSS.Equals(_userAuth.RoleCode))
            {
                return true;
            }

            return false;
        }
    }
}
