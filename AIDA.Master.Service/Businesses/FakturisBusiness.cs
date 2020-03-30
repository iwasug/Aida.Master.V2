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
    public class FakturisBusiness : BaseBusiness
    {
        public AlertMessage Add(FakturisViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.Fakturis))
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

            IRepository<TFakturis> repo = _unitOfWork.GetRepository<TFakturis>();
            TFakturis item = null;

            #region check NIK exist
            repo.Condition = PredicateBuilder.True<TFakturis>().And(x => x.NIK == model.NIK);

            item = repo.Find().FirstOrDefault();

            if (item != null)
            {
                alert.Text = string.Format(StaticMessage.ERR_NIK_EXIST, item.NIK);
                return alert;
            }
            #endregion

            item = new TFakturis()
            {
                NIK = model.NIK,
                FULLNAME = model.Fullname,
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
                alert.Text = string.Format(StaticMessage.SCS_ADD_MASTER, item.NIK, item.FULLNAME);
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

        public AlertMessage Edit(FakturisViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.Fakturis))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (!IsEditable())
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            IRepository<TFakturis> repo = _unitOfWork.GetRepository<TFakturis>();
            repo.Condition = PredicateBuilder.True<TFakturis>().And(x => x.NIK == model.NIK);

            TFakturis item = repo.Find().FirstOrDefault();

            if (item == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            DateTime now = DateTime.UtcNow.ToUtcID();

            item.FULLNAME = model.Fullname;
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
                alert.Text = string.Format(StaticMessage.SCS_EDIT, item.NIK, item.FULLNAME);
            }
            catch (Exception ex)
            {
                alert.Text = StaticMessage.ERR_SAVE_FAILED;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return alert;
        }

        private FakturisViewModel GetFakturisViewModel(DataRow dr)
        {
            FakturisViewModel model = new FakturisViewModel();

            model.NIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]);
            model.Fullname = dr.IsNull("FULLNAME") ? null : dr["FULLNAME"].ToString();
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

        private FakturisViewModel GetFakturisViewModel(TFakturis item)
        {
            FakturisViewModel model = new FakturisViewModel();
            model.NIK = (int) item.NIK;
            model.Fullname = item.FULLNAME;
            model.IsRole = item.Role;
            model.ValidFromDate = item.ValidFrom;
            model.FormattedValidFrom = model.ValidFromDate.ToString(AppConstant.DefaultFormatDate);
            model.ValidToDate = item.ValidTo;
            model.FormattedValidTo = model.ValidToDate.ToString(AppConstant.DefaultFormatDate);

            return model;
        }

        public JDatatableResponse GetDatatableByQuery(JDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "fak.FULLNAME"
                ,"fak.FULLNAME"
                ,"a.NIK"
                ,"fak.Fullname"
                ,"fak.Role"
                ,"fak.ValidFrom"
                ,"fak.ValidTo"
            };

            if (_userAuth == null) return result;

            string query = "SELECT [:SELECT]" + System.Environment.NewLine +
"FROM (" + System.Environment.NewLine +
"	SELECT rth.Fakturis as NIK" + System.Environment.NewLine +
"	FROM RTHeader rth" + System.Environment.NewLine +
"	LEFT JOIN TFakturis fak ON fak.NIK = rth.Fakturis" + System.Environment.NewLine +
"	WHERE 1=1" + System.Environment.NewLine +
"		AND rth.ValidTo >= GETDATE()" + System.Environment.NewLine +
"		AND rth.Fakturis <> 0" + System.Environment.NewLine +
"		AND rth.Fakturis IS NOT NULL" + System.Environment.NewLine +
"		[:PRE_CONDITION]" + System.Environment.NewLine +
//"	UNION" + System.Environment.NewLine +
//"	SELECT t.NIK" + System.Environment.NewLine +
//"	FROM TFakturis t" + System.Environment.NewLine +
//"	WHERE 1=1" + System.Environment.NewLine +
//"		AND CreatedBy = '"+_userAuth.NIK.ToString()+"'" + System.Environment.NewLine +
") a" + System.Environment.NewLine +
"LEFT JOIN TFakturis fak ON fak.NIK = a.NIK" + System.Environment.NewLine +
"WHERE 1=1" + System.Environment.NewLine +
"   [:CONDITION]";

            string selectColumn = "DISTINCT a.NIK" + System.Environment.NewLine +
"   ,fak.FULLNAME" + System.Environment.NewLine +
"	,fak.Role" + System.Environment.NewLine +
"	,fak.ValidFrom" + System.Environment.NewLine +
"	,fak.ValidTo";

            string selectCount = "COUNT(DISTINCT a.NIK) AS NumRows";

            if (_userAuth == null) return result;

            string preCondition = "";
            string condition = "";
            Dictionary<string, object> dcParams = new Dictionary<string, object>();

            if (RoleCode.AdminOperation.Equals(_userAuth.RoleCode))
            {

            }
            else if (RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                preCondition += "	AND rth.Plant = @plant";
                dcParams["@plant"] = _userAuth.Plant;
            }
            else
            {
                return result;
            }

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                model.Keyword = model.Keyword.ToLower();

                condition += System.Environment.NewLine;
                condition += "	AND (a.NIK LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(fak.FULLNAME) LIKE '%[:KEYWORD]%')";

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

            List<FakturisViewModel> listData = new List<FakturisViewModel>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    listData.Add(GetFakturisViewModel(dr));
                }
            }

            result.Data = listData;

            return result;
        }

        public FakturisViewModel GetDetail(int id)
        {
            IRepository<TFakturis> repo = _unitOfWork.GetRepository<TFakturis>();
            repo.Condition = PredicateBuilder.True<TFakturis>().And(x=>x.NIK == id);

            TFakturis item = repo.Find().FirstOrDefault();

            if (item == null) return null;

            return GetFakturisViewModel(item);
        }

        public bool IsEditable()
        {
            if(_userAuth != null && RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                return true;
            }

            return false;
        }
    }
}
