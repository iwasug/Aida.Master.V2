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
using AIDA.Master.Infrastucture.MailService;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Radyalabs.Core.Helper;
using Radyalabs.Core.Repository;

namespace AIDA.Master.Service.Businesses
{
    public class MasterListBusiness : BaseBusiness
    {
        private const string _formattedDate = "yyyy-MM-dd";
        private static DateTime _defaultValidTo = Convert.ToDateTime(AppConstant.DefaultValidTo);
        private IMailService _mailService;

        public MasterListBusiness() { }

        public MasterListBusiness(IMailService ms)
        {
            _mailService = ms;
        }

        public AlertMessage ClearDouble(ClearDoubleViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            int month = 0;
            int year = 0;
            DateTime dtValidFrom = DateTime.MinValue;
            DateTime dtValidTo = DateTime.MinValue;

            try
            {
                string[] arr = model.FormattedValidDate.Split('-');

                month = Convert.ToInt16(arr[0]);
                year = Convert.ToInt16(arr[1]);

                dtValidFrom = new DateTime(year, month, 1);
                dtValidTo = dtValidFrom.AddDays(-1);
            }
            catch (Exception ex)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            if (model.ListId == null || model.ListId.Count == 0)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            IRepository<RHDetail> repo = _unitOfWork.GetRepository<RHDetail>();

            var orCondition = PredicateBuilder.False<RHDetail>();

            foreach (var id in model.ListId)
            {
                orCondition = orCondition.Or(x=>x.ID == id);
            }

            repo.Condition = PredicateBuilder.True<RHDetail>().And(orCondition);

            List<RHDetail> list = repo.Find();

            if (list == null || list.Count == 0)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            List<int> listSuccessId = new List<int>();

            listSuccessId = model.ListId;

            DateTime itemValidFrom = DateTime.MinValue;

            foreach (var item in list)
            {
                try
                {
                    if (item.ValidFrom.Year == year && item.ValidFrom.Month == month)
                    {
                        repo.Delete(item, true);
                    }
                    else
                    {
                        item.ValidTo = dtValidTo;

                        repo.Update(item, true);
                    }

                    listSuccessId.Add(item.ID);
                }
                catch (Exception ex)
                {
                }
            }

            alert.Status = 1;
            alert.Data = listSuccessId;

            return alert;
        }

        public AlertMessage ExportMasterListSales(RayonDatatableViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            model.Length = -1;

            JDatatableResponse response = GetDatatableSales(model);

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
            cell.SetCellValue("NIK Salesman");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Nama Salesman");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Kode Customer");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Nama Customer");
            cell.CellStyle = cellStyleHeader;
            #endregion

            rowIndex++;

            #region cell data
            List<SalesCustomerViewModel> listData = response.Data as List<SalesCustomerViewModel>;

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

                    // Salesman NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.SLMNik);
                    cell.CellStyle = cellStyleNumber;

                    // Salesman Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.SLMFullname);
                    cell.CellStyle = cellStyleText;

                    // Customer Code
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.CustomerCode);
                    cell.CellStyle = cellStyleNumber;

                    // Customer Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.CustomerName);
                    cell.CellStyle = cellStyleText;

                    rowIndex++;
                }
            }
            #endregion

            #region auto size column
            for (int i = 0; i < 5; i++)
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

        public AlertMessage ExportMasterListTagih(RayonDatatableViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            model.Length = -1;

            JDatatableResponse response = GetDatatableTagih(model);

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
            cell.SetCellValue("NIK Salesman");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Nama Salesman");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Kode Customer");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Nama Customer");
            cell.CellStyle = cellStyleHeader;
            #endregion

            rowIndex++;

            #region cell data
            List<SalesCustomerViewModel> listData = response.Data as List<SalesCustomerViewModel>;

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

                    // Salesman NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.SLMNik);
                    cell.CellStyle = cellStyleNumber;

                    // Salesman Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.SLMFullname);
                    cell.CellStyle = cellStyleText;

                    // Customer Code
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.CustomerCode);
                    cell.CellStyle = cellStyleNumber;

                    // Customer Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.CustomerName);
                    cell.CellStyle = cellStyleText;

                    rowIndex++;
                }
            }
            #endregion

            #region auto size column
            for (int i = 0; i < 5; i++)
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

        public JDatatableResponse GetDatatableSales(RayonDatatableViewModel model)
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

            List<RayonViewModel> listRayon = GetRayonSalesByUserAuth();

            IRepository<RHDetail> repo = _unitOfWork.GetRepository<RHDetail>();
            repo.Includes = new string[] { "CustomerObj" };

            repo.Condition = PredicateBuilder.True<RHDetail>().And(x => x.ValidTo >= today);

            if (string.IsNullOrEmpty(model.RayonCode))
            {
                var orCondition = PredicateBuilder.False<RHDetail>();

                foreach (var item in listRayon)
                {
                    orCondition = orCondition.Or(x => x.RayonCode.Equals(item.RayonCode));
                }

                repo.Condition = repo.Condition.And(orCondition);
            }
            else
            {
                repo.Condition = repo.Condition.And(x=>x.RayonCode.Equals(model.RayonCode));
            }

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
            repoRHH.Condition = PredicateBuilder.True<RHHeader>().And(x => x.ValidTo >= today);
            repoRHH.OrderBy = new SqlOrderBy("CreatedOn", SqlOrderType.Descending);

            RHHeader rhh = repoRHH.Find().FirstOrDefault();

            #endregion

            List<SalesCustomerViewModel> listData = new List<SalesCustomerViewModel>();

            foreach (var item in listItem)
            {
                listData.Add(GetSalesRayonViewModel(item, listRayon));
            }

            result.Data = listData;

            return result;
        }

        public JDatatableResponse GetDatatableSalesByQuery(RayonDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "rhd.CreatedOn"
                ,"rhd.Customer"
                ,"cus.CustomerName"
                ,"rhd.RayonCode"
                ,"rhh.SLM"
                ,"slm.FullName"
                ,"rhh.SLM"
                ,"slm.FullName"
                ,"rhd.ValidFrom"
                ,"rhd.ValidTo"
            };

            #region
            string query = "SELECT [:SELECT]" + System.Environment.NewLine +
"FROM RHDetail rhd" + System.Environment.NewLine +
"LEFT JOIN RHHeader rhh ON rhh.RayonCode = rhd.RayonCode" + System.Environment.NewLine +
"    AND rhh.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN SLM slm ON slm.NIK = rhh.SLM" + System.Environment.NewLine +
"    AND slm.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN FSS fss ON fss.NIK = rhh.FSS" + System.Environment.NewLine +
"    AND fss.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN TCustomer cus ON cus.CustomerCode = rhd.Customer" + System.Environment.NewLine +
"WHERE 1=1" + System.Environment.NewLine +
"    AND rhd.ValidTo >= GETDATE()" + System.Environment.NewLine +
"[:CONDITION]";

            string selectColumn = "rhd.RayonCode" + System.Environment.NewLine +
"   ,rhh.SLM" + System.Environment.NewLine +
"	,slm.FullName AS SLMFullName" + System.Environment.NewLine +
"   ,rhh.FSS" + System.Environment.NewLine +
"	,fss.FullName AS FSSFullName" + System.Environment.NewLine +
"	,rhd.Customer" + System.Environment.NewLine +
"	,cus.CustomerName AS CustomerName" + System.Environment.NewLine +
"	,rhd.ValidFrom" + System.Environment.NewLine +
"	,rhd.ValidTo";

            string selectCount = "COUNT(*) AS NumRows";
            #endregion

            if (_userAuth == null) return result;

            string condition = "";
            Dictionary<string, object> dcParams = new Dictionary<string, object>();

            if (RoleCode.RM.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rhh.BUM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode))
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
            else if (RoleCode.SLM.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rhh.FSS = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.KaCab.Equals(_userAuth.RoleCode) && _userAuth.Plant != null)
            {
                //condition += " AND rhh.Plant = @plant";
                //dcParams["@plant"] = _userAuth.Plant.Value;
                condition += "	AND (rhh.NSM = @nik OR rhh.Plant = @plant)";
                dcParams["@nik"] = _userAuth.NIK;
                dcParams["@plant"] = _userAuth.Plant.Value;
            }
            else
            {
                return result;
            }

            if (!string.IsNullOrEmpty(model.RayonCode))
            {
                condition += " AND rhd.RayonCode = @rayonCode";
                dcParams["@rayonCode"] = model.RayonCode;
            }

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                model.Keyword = model.Keyword.ToLower();

                condition += System.Environment.NewLine;
                condition += "	AND (LOWER(rhd.RayonCode) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rhh.SLM LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(slm.FullName) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rhh.FSS LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(fss.FullName) LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR rhd.Customer LIKE '%[:KEYWORD]%'" + System.Environment.NewLine +
"		OR LOWER(cus.CustomerName) LIKE '%[:KEYWORD]%')";

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

            List<SalesCustomerViewModel> listData = new List<SalesCustomerViewModel>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    listData.Add(GetSalesRayonViewModel(dr));
                }
            }

            result.Data = listData;

            return result;
        }

        public JDatatableResponse GetDatatableTagih(RayonDatatableViewModel model)
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

            List<RayonViewModel> listRayon = GetRayonTagihByQuery();

            IRepository<RTDetail> repo = _unitOfWork.GetRepository<RTDetail>();
            repo.Includes = new string[] { "CustomerObj" };

            repo.Condition = PredicateBuilder.True<RTDetail>().And(x => x.ValidTo >= today);

            if (string.IsNullOrEmpty(model.RayonCode) && listRayon != null)
            {
                var orCondition = PredicateBuilder.False<RTDetail>();

                foreach (var item in listRayon)
                {
                    orCondition = orCondition.Or(x => x.RayonCode.Equals(item.RayonCode));
                }

                repo.Condition = repo.Condition.And(orCondition);
            }
            else
            {
                repo.Condition = repo.Condition.And(x => x.RayonCode.Equals(model.RayonCode));
            }

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

            List<RTDetail> listItem = repo.Find();

            if (listItem == null) return result;

            #region get salesman on a rayon

            IRepository<RTHeader> repoRTH = _unitOfWork.GetRepository<RTHeader>();
            repoRTH.Includes = new string[] { "SLMObj" };
            repoRTH.Condition = PredicateBuilder.True<RTHeader>().And(x => x.ValidTo >= today);
            repoRTH.OrderBy = new SqlOrderBy("CreatedOn", SqlOrderType.Descending);

            RTHeader rth = repoRTH.Find().FirstOrDefault();

            #endregion

            List<SalesCustomerViewModel> listData = new List<SalesCustomerViewModel>();

            foreach (var item in listItem)
            {
                listData.Add(GetSalesRayonViewModel(item, listRayon));
            }

            result.Data = listData;

            return result;
        }

        public JDatatableResponse GetDatatableTagihByQuery(RayonDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "rtd.CreatedOn"
                ,"rtd.Customer"
                ,"cus.CustomerName"
                ,"rtd.RayonCode"
                ,"rth.SLM"
                ,"slm.FullName"
                ,"rth.FSS"
                ,"fss.FullName"
                ,"rtd.ValidFrom"
                ,"rtd.ValidTo"
            };

            string query = "SELECT [:SELECT]" + System.Environment.NewLine +
"FROM RTDetail rtd" + System.Environment.NewLine +
"LEFT JOIN RTHeader rth ON rth.RayonCode = rtd.RayonCode" + System.Environment.NewLine +
"    AND rth.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN SLM slm ON slm.NIK = rth.SLM" + System.Environment.NewLine +
"    AND slm.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN FSS fss ON fss.NIK = rth.FSS" + System.Environment.NewLine +
"    AND fss.ValidTo >= GETDATE()" + System.Environment.NewLine +
"LEFT JOIN TCustomer cus ON cus.CustomerCode = rtd.Customer" + System.Environment.NewLine +
"WHERE 1=1" + System.Environment.NewLine +
"    AND rtd.ValidTo >= GETDATE()" + System.Environment.NewLine +
"[:CONDITION]";

            string selectColumn = "rtd.RayonCode" + System.Environment.NewLine +
"   ,rth.SLM" + System.Environment.NewLine +
"	,slm.FullName AS SLMFullName" + System.Environment.NewLine +
"   ,rth.FSS" + System.Environment.NewLine +
"	,fss.FullName AS FSSFullName" + System.Environment.NewLine +
"	,rtd.Customer" + System.Environment.NewLine +
"	,cus.CustomerName AS CustomerName" + System.Environment.NewLine +
"	,rtd.ValidFrom" + System.Environment.NewLine +
"	,rtd.ValidTo";

            string selectCount = "COUNT(*) AS NumRows";

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
                //condition += " AND rth.Plant = @plant";
                
                condition += "	AND (rth.NSM = @nik OR rth.Plant = @plant)";
                dcParams["@nik"] = _userAuth.NIK;
                dcParams["@plant"] = _userAuth.Plant.Value;
            }
            else
            {
                return result;
            }

            if (! string.IsNullOrEmpty(model.RayonCode))
            {
                condition += " AND rtd.RayonCode = @rayonCode";
                dcParams["@rayonCode"] = model.RayonCode;
            }

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                model.Keyword = model.Keyword.ToLower();

                condition += System.Environment.NewLine;
                condition += "	AND (LOWER(rtd.RayonCode) LIKE '%' + @keyword + '%'" + System.Environment.NewLine +
"		OR rth.SLM LIKE '%' + @keyword + '%'" + System.Environment.NewLine +
"		OR LOWER(slm.FullName) LIKE '%' + @keyword + '%'" + System.Environment.NewLine +
"		OR rth.FSS LIKE '%' + @keyword + '%'" + System.Environment.NewLine +
"		OR LOWER(fss.FullName) LIKE '%' + @keyword + '%'" + System.Environment.NewLine +
"		OR rtd.Customer LIKE '%' + @keyword + '%'" + System.Environment.NewLine +
"		OR LOWER(cus.CustomerName) LIKE '%' + @keyword + '%')";

                //condition = condition.Replace("[:KEYWORD]", model.Keyword);
                dcParams["@keyword"] = model.Keyword;
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

            List<SalesCustomerViewModel> listData = new List<SalesCustomerViewModel>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    listData.Add(GetSalesRayonViewModel(dr));
                }
            }

            result.Data = listData;

            return result;

        }

        private List<SalesCustomerViewModel> GetListSalesCustomerViewModel(HttpPostedFileBase postedFile)
        {
            IWorkbook workbook = GetWorkbook(postedFile);

            if (workbook == null) return null;

            ISheet sheet = workbook.GetSheetAt(0);

            List<SalesCustomerViewModel> result = new List<SalesCustomerViewModel>();
            SalesCustomerViewModel model = null;

            object obj = null;

            List<string> listRemarks = null;

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                listRemarks = new List<string>();

                try
                {
                    IRow row = sheet.GetRow(i);

                    if (row == null) continue;

                    model = new SalesCustomerViewModel();

                    obj = GetObjFromCell(row.GetCell(0));
                    model.RayonCode = obj == null ? null : obj.ToString().Trim();

                    if (string.IsNullOrEmpty(model.RayonCode))
                    {
                        listRemarks.Add("RayonCode kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(1));
                    model.SLMNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());

                    if (model.SLMNik == 0)
                    {
                        listRemarks.Add("NIK Salesman kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(2));
                    model.SLMFullname = obj == null ? null : obj.ToString().Trim();

                    obj = GetObjFromCell(row.GetCell(3));
                    model.CustomerCode = obj == null ? null : obj.ToString().Trim();

                    if (string.IsNullOrEmpty(model.CustomerCode))
                    {
                        listRemarks.Add("Kode Customer kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(4));
                    model.CustomerName = obj == null ? null : obj.ToString().Trim();

                    if (listRemarks.Count > 0)
                    {
                        model.Remarks = String.Join("; ", listRemarks.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    model.Remarks = StaticMessage.ERR_INVALID_ROW_XLS;
                }
                finally
                {
                    if(!string.IsNullOrEmpty(model.RayonCode))
                        result.Add(model);
                }
            }

            return result;
        }

        public List<RayonViewModel> GetRayonSalesByUserAuth()
        {
            List<RHHeader> listRHH = GetRHHeaderByUserAuth();

            if (listRHH == null) return null;

            List<RayonViewModel> listModel = new List<RayonViewModel>();

            foreach (var item in listRHH)
            {
                listModel.Add(GetRayonViewModelBySales(item));
            }

            return listModel;
        }

        public List<RayonViewModel> GetRayonTagihByUserAuth()
        {
            List<RTHeader> listRTH = GetRTHeaderByUserAuth();

            if (listRTH == null) return null;

            List<RayonViewModel> listModel = new List<RayonViewModel>();

            foreach (var item in listRTH)
            {
                if (listModel.FirstOrDefault(x => x.RayonCode.Equals(item.RayonCode)) != null) continue;

                listModel.Add(GetRayonViewModelByTagih(item));
            }

            return listModel;
        }

        public List<RayonViewModel> GetRayonTagihByQuery()
        {
            string query = "SELECT DISTINCT rth.RayonCode" + System.Environment.NewLine +
"	,slm.NIK" + System.Environment.NewLine +
"	,slm.FullName" + System.Environment.NewLine +
"FROM RTHeader rth" + System.Environment.NewLine +
"LEFT JOIN SLM slm on slm.NIK = rth.SLM" + System.Environment.NewLine +
"	AND slm.ValidTo >= GETDATE()" + System.Environment.NewLine +
"WHERE 1=1" + System.Environment.NewLine +
"	AND rth.ValidTo >= GETDATE()" + System.Environment.NewLine +
"[:CONDITION]";

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
            else if (RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rth.NSM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.FSS.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rth.FSS = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode))
            {
                condition += "	AND rth.NSM = @nik";
                dcParams["@nik"] = _userAuth.NIK;
            }
            else
            {
                return null;
            }

            string connString = GetConnectionString();

            query = query.Replace("[:CONDITION]", condition);

            DataTable dataTable = SqlHelper.ExecuteQuery(connString, query, dcParams);

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return null;
            }

            List<RayonViewModel> listModel = new List<RayonViewModel>();

            foreach (DataRow dr in dataTable.Rows)
            {
                listModel.Add(new RayonViewModel()
                {
                    RayonCode = dr.IsNull("RayonCode") ? null : dr["RayonCode"].ToString(),
                    SalesmanNIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]),
                    SalesmanFullname = dr.IsNull("FullName") ? null : dr["FullName"].ToString(),
                });
            }

            return listModel;
        }

        private RayonViewModel GetRayonViewModelBySales(RHHeader item)
        {
            RayonViewModel model = new RayonViewModel()
            {
                RayonCode = item.RayonCode,
                SalesmanNIK = item.SLM
            };

            if (item.SLMObj1 != null)
            {
                model.SalesmanFullname = item.SLMObj1.FullName;
            }

            return model;
        }

        private RayonViewModel GetRayonViewModelByTagih(RTHeader item)
        {
            RayonViewModel model = new RayonViewModel()
            {
                RayonCode = item.RayonCode,
                SalesmanNIK = item.SLM ?? 0
            };

            if (item.SLMObj != null)
            {
                model.SalesmanFullname = item.SLMObj.FullName;
            }

            return model;
        }

        private SalesCustomerViewModel GetSalesRayonViewModel(RHDetail item, List<RayonViewModel> listRayon)
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

            if (listRayon != null && listRayon.Count > 0)
            {
                RayonViewModel rayon = listRayon.FirstOrDefault(x=>x.RayonCode.Equals(item.RayonCode));

                if (rayon != null)
                {
                    model.SLMNik = rayon.SalesmanNIK;
                    model.SLMFullname = rayon.SalesmanFullname;
                }
            }

            return model;
        }

        private SalesCustomerViewModel GetSalesRayonViewModel(RTDetail item, List<RayonViewModel> listRayon)
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

            if (listRayon != null && listRayon.Count > 0)
            {
                RayonViewModel rayon = listRayon.FirstOrDefault(x => x.RayonCode.Equals(item.RayonCode));

                if (rayon != null)
                {
                    model.SLMNik = rayon.SalesmanNIK;
                    model.SLMFullname = rayon.SalesmanFullname;
                }
            }

            return model;
        }

        private SalesCustomerViewModel GetSalesRayonViewModel(DataRow dr)
        {
            SalesCustomerViewModel model = new SalesCustomerViewModel();

            model.RayonCode = dr.IsNull("RayonCode") ? null : dr["RayonCode"].ToString();
            model.SLMNik = dr.IsNull("SLM") ? 0 : Convert.ToInt32(dr["SLM"]);
            model.SLMFullname = dr.IsNull("SLMFullName") ? null : dr["SLMFullName"].ToString();
            model.FSSNik = dr.IsNull("FSS") ? 0 : Convert.ToInt32(dr["FSS"]);
            model.FSSFullname = dr.IsNull("FSSFullName") ? null : dr["FSSFullName"].ToString();
            model.CustomerCode = dr.IsNull("Customer") ? null : dr["Customer"].ToString();
            model.CustomerName = dr.IsNull("CustomerName") ? null : dr["CustomerName"].ToString();
            model.CustomerName = dr.IsNull("CustomerName") ? null : dr["CustomerName"].ToString();
            model.ValidFromDate = dr.IsNull("ValidFrom") ? DateTime.MinValue : Convert.ToDateTime(dr["ValidFrom"]);
            model.FormattedValidFrom = model.ValidFromDate.ToString(AppConstant.DefaultFormatDate);
            model.ValidToDate = dr.IsNull("ValidTo") ? DateTime.MinValue : Convert.ToDateTime(dr["ValidTo"]);
            model.FormattedValidTo = model.ValidToDate.ToString(AppConstant.DefaultFormatDate);

            return model;
        }

        

        private List<RTHeader> GetRTHeaderByUserAuth()
        {
            if (_userAuth == null) return null;

            DateTime today = DateTime.UtcNow.ToUtcID().Date;

            IRepository<RTHeader> repo = _unitOfWork.GetRepository<RTHeader>();
            repo.Includes = new string[] { "SLMObj" };

            repo.Condition = PredicateBuilder.True<RTHeader>().And(x => x.ValidTo >= today);

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
            else if (RoleCode.KaCab.Equals(_userAuth.RoleCode) && _userAuth.Plant != null)
            {
                repo.Condition = repo.Condition.And(x=>x.NSM == _userAuth.NIK || x.Plant == _userAuth.Plant.Value.ToString());
            }
            else
            {
                return null;
            }

            return repo.Find();
        }

        public async Task<AlertMessage> ImportSales(ImportMasterListViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.MasterSalesList))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (!IsEditableSales())
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

            IRepository<UploadML2> repoUploadML = _unitOfWork.GetRepository<UploadML2>();

            UploadML2 uml = null;

            DateTime now = DateTime.UtcNow.ToUtcID();

            int i = 0;

            try
            {
                _unitOfWork.BeginTransaction();

                foreach (var item in model.ListCustomer)
                {
                    item.Remarks = null;

                    uml = new UploadML2()
                    {
                        ID = TextHelper.GenerateGuid(),
                        RayonCode = item.RayonCode,
                        Customer = item.CustomerCode,
                        CreatedByNIK = _userAuth.NIK,
                        CreatedOn = now
                    };

                    repoUploadML.Insert(uml);

                    i++;
                }

                string err = _unitOfWork.Commit();

                if (!string.IsNullOrEmpty(err))
                {
                    _logger.Write("error", DateTime.Now, err, _userAuth.Fullname);
                    alert.Text = err;
                    return alert;
                }
            }
            catch (Exception ex)
            {
                _logger.Write("Error",DateTime.Now, ex.ToString(), _userAuth.Fullname, ex);
                alert.Text = StaticMessage.ERR_SAVE_FAILED;
                return alert;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            #region execute SP
            List<SqlParameter> listSqlParam = new List<SqlParameter>()
            {
                new SqlParameter("@inMonth", month),
                new SqlParameter("@inYear", year),
                new SqlParameter("@nik", _userAuth.NIK)
            };

            bool isError = false;
            string connString = GetConnectionString();

            Dictionary<int, dynamic> dc = null;

            try
            {
                dc = SqlHelper.ExecuteProcedureWithReturnRecords(connString, "sp_uploadML_v2", listSqlParam);

                if (dc.ContainsKey(0))
                {
                    isError = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Write("Error", DateTime.Now, ex.ToString(), _userAuth.Fullname, ex);
                isError = true;
            }
            #endregion

            int numDouble = 0;

            ImportMasterListViewModel doubleData = null;

            if (isError)
            {
                // delete all records from table UploadML
                var condition = PredicateBuilder.True<UploadML2>().And(x=>x.CreatedByNIK == _userAuth.NIK);

                repoUploadML.Delete(condition, true);
            }
            else if (dc != null)
            {
                #region get double data import
                dynamic val;

                DataTable dt = null;

                if (dc.TryGetValue(1, out val))
                {
                    dt = val as DataTable;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        doubleData = new ImportMasterListViewModel()
                        {
                            FormattedValidDate = model.FormattedValidDate,
                            ListCustomer = new List<SalesCustomerViewModel>()
                        };

                        SalesCustomerViewModel salesCustomer = null;

                        foreach (DataRow dr in dt.Rows)
                        {
                            salesCustomer = new SalesCustomerViewModel();
                            salesCustomer.Id = dr.IsNull("ID") ? null : dr["ID"].ToString();
                            salesCustomer.RayonCode = dr.IsNull("RayonCode") ? null : dr["RayonCode"].ToString();
                            salesCustomer.SLMFullname = dr.IsNull("FullName") ? null : dr["FullName"].ToString();
                            salesCustomer.CustomerCode = dr.IsNull("CustomerCode") ? null : dr["CustomerCode"].ToString();
                            salesCustomer.CustomerName = dr.IsNull("CustomerName") ? null : dr["CustomerName"].ToString();
                            salesCustomer.ValidFromDate = dr.IsNull("ValidFrom") ? DateTime.MinValue : Convert.ToDateTime(dr["ValidFrom"]);
                            salesCustomer.ValidToDate = dr.IsNull("ValidTo") ? DateTime.MinValue : Convert.ToDateTime(dr["ValidTo"]);

                            salesCustomer.FormattedValidFrom = salesCustomer.ValidFromDate.ToString(AppConstant.DefaultFormatDate);
                            salesCustomer.FormattedValidTo = salesCustomer.ValidToDate.ToString(AppConstant.DefaultFormatDate);

                            doubleData.ListCustomer.Add(salesCustomer);
                        }


                        alert.Data = doubleData;

                        numDouble = doubleData.ListCustomer.Count();
                    }
                }
                #endregion
            }

            _unitOfWork.Dispose();

            if (isError)
            {
                alert.Text = StaticMessage.ERR_SAVE_FAILED;
            }
            else
            {
                int numSuccess = model.ListCustomer.Count(x => string.IsNullOrEmpty(x.Remarks));

                if (alert.Data != null)
                {
                    alert.Status = 0;
                    alert.Text = string.Format(StaticMessage.SCS_IMPORT_FOUND_DOUBLE, numDouble);
                }
                else
                {
                    alert.Status = 1;
                    alert.Text = StaticMessage.SCS_IMPORT_NO_DOUBLE;
                }

                await SendEmailUploadSales(doubleData);
            }

            return alert;
        }

        public async Task SendEmailUploadSales(ImportMasterListViewModel doubleData)
        {
            #region send email
            NSM nsm = GetNsmByUserAuth();

            if (nsm == null) return;

            if (nsm.Email == null) return;

            DateTime today = DateTime.UtcNow.ToUtcID();

            string partialViewDouble = "";

            if (doubleData != null && doubleData.ListCustomer != null && doubleData.ListCustomer.Count > 0)
            {
                StringBuilder htmlRows = new StringBuilder();
                foreach (var data in doubleData.ListCustomer)
                {
                    htmlRows.Append("<tr>");
                    htmlRows.Append($"<td style=\"text-align: center; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.Id}</td>");
                    htmlRows.Append($"<td style=\"text-align: center; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.RayonCode}</td>");
                    htmlRows.Append($"<td style=\"text-align: left; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.SLMFullname}</td>");
                    htmlRows.Append($"<td style=\"text-align: center; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.CustomerCode}</td>");
                    htmlRows.Append($"<td style=\"text-align: left; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.CustomerName}</td>");
                    htmlRows.Append($"<td style=\"text-align: center; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.FormattedValidFrom}</td>");
                    htmlRows.Append($"<td style=\"text-align: center; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.FormattedValidTo}</td>");
                    htmlRows.Append("</tr>");
                }

                partialViewDouble = System.IO.File.ReadAllText(Path.Combine(_mailService.GetDirLayout(), EmailConstant.ViewPartialMasterListDouble));
                partialViewDouble = partialViewDouble.Replace("{:ROW_DOUBLE_MASTER_LIST}", htmlRows.ToString());
            }

            List<string> listTo = new List<string>() { nsm.Email };

            Dictionary<string, string> dcParams = new Dictionary<string, string>()
            {
                { "{:FULLNAME}", nsm.FullName },
                { "{:UPLOADER_NIK}", _userAuth.NIK.ToString() },
                { "{:UPLOADER_NAME}", _userAuth.Fullname },
                { "{:FORMATTED_DATE}", today.ToString("yyyy-MM-dd HH:mm") },
            };

            string subject = EmailConstant.SubjectPrefix + string.Format("Upload Master List Sales Oleh {0} ({1})", _userAuth.Fullname, _userAuth.NIK);
            string view = EmailConstant.ViewMasterListSalesUploaded;
            string content = _mailService.GetBodyFromView(view, dcParams);
            content = content.Replace("{:PARTIAL_MASTER_LIST_DOUBLE}", partialViewDouble);

            await _mailService.Send(content, subject, listTo);

            #endregion
        }

        public AlertMessage ImportTagih(ImportMasterListViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.MasterTagihList))
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

            IRepository<UploadML2> repoUploadML = _unitOfWork.GetRepository<UploadML2>();

            UploadML2 uml = null;

            DateTime now = DateTime.UtcNow.ToUtcID();

            foreach (var item in model.ListCustomer)
            {
                item.Remarks = null;

                uml = new UploadML2()
                {
                    ID = TextHelper.GenerateGuid(),
                    RayonCode = item.RayonCode,
                    Customer = item.CustomerCode,
                    CreatedByNIK = _userAuth.NIK,
                    CreatedOn = now
                };

                try
                {
                    repoUploadML.Insert(uml, true);
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
                new SqlParameter("@nik", _userAuth.NIK)
            };

            bool isError = false;
            string connString = GetConnectionString();

            Dictionary<int, dynamic> dc = null;

            try
            {
                dc = SqlHelper.ExecuteProcedureWithReturnRecords(connString, "sp_uploadMLTagih_v2", listSqlParam);

                if (dc.ContainsKey(0))
                {
                    isError = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Write("Error", DateTime.Now, ex.ToString(), _userAuth.Fullname, ex);
                isError = true;
            }
            #endregion

            if (isError)
            {
                // delete all records from table UploadML
                var condition = PredicateBuilder.True<UploadML2>().And(x => x.CreatedByNIK == _userAuth.NIK);

                repoUploadML.Delete(condition, true);
            }

            _unitOfWork.Dispose();

            if (isError)
            {
                alert.Text = StaticMessage.ERR_SAVE_FAILED;
            }
            else
            {
                int numSuccess = model.ListCustomer.Count(x => string.IsNullOrEmpty(x.Remarks));

                alert.Status = 1;

                alert.Text = StaticMessage.SCS_IMPORT_NO_DOUBLE;
            }

            return alert;
        }

        public bool IsEditableSales()
        {
            bool result = false;

            if ((RoleCode.FSS.Equals(_userAuth.RoleCode) || RoleCode.ASM.Equals(_userAuth.RoleCode) || RoleCode.NSM.Equals(_userAuth.RoleCode))  && _userAuth.IsAbleToWrite)
            {
                result = true;
            } 

            return result;
        }

        public bool IsEditableTagih()
        {
            bool result = false;

            if (RoleCode.KaCab.Equals(_userAuth.RoleCode) || RoleCode.NSM.Equals(_userAuth.RoleCode))
            {
                result = true;
            }

            return result;
        }

        public AlertMessage PreviewSales(ImportMasterListViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.MasterSalesList))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (!IsEditableSales())
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

            List<SalesCustomerViewModel> listVM = GetListSalesCustomerViewModel(model.InputFile);

            if (listVM == null)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            IRepository<SLM> repoSLM = _unitOfWork.GetRepository<SLM>();
            IRepository<TCustomer> repoCustomer = _unitOfWork.GetRepository<TCustomer>();

            List<int> listExistingSLM = new List<int>();
            List<int> listNotExistSLM = new List<int>();
            List<int> listNotActiveSLM = new List<int>();
            SLM slm = null;

            List<string> listExistingCustomer = new List<string>();
            List<string> listNotExistCustomer = new List<string>();
            TCustomer customer = null;

            List<string> listRemarks = null;

            DateTime today = DateTime.UtcNow.ToUtcID();

            foreach (var item in listVM)
            {
                listRemarks = new List<string>();

                if (!string.IsNullOrEmpty(item.Remarks)) continue;

                #region check SLM
                if (!listExistingSLM.Exists(x => x == item.SLMNik))
                {
                    if (listNotExistSLM.Exists(x => x == item.SLMNik))
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_SLM_NOT_FOUND, item.SLMNik));
                    }
                    else if (listNotActiveSLM.Exists(x => x == item.SLMNik))
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_SLM_NOT_ACTIVE, item.SLMNik));
                    }
                    else
                    {
                        repoSLM.Condition = PredicateBuilder.True<SLM>().And(x => x.NIK == item.SLMNik);

                        slm = repoSLM.Find().FirstOrDefault();

                        if (slm == null)
                        {
                            listNotExistSLM.Add(item.SLMNik);
                            listRemarks.Add(string.Format(StaticMessage.ERR_SLM_NOT_FOUND, item.SLMNik));
                        }
                        else if (slm.ValidTo < today)
                        {
                            listNotActiveSLM.Add(slm.NIK);
                            listRemarks.Add(string.Format(StaticMessage.ERR_SLM_NOT_ACTIVE, item.SLMNik));
                        }
                        else
                        {
                            listExistingSLM.Add(slm.NIK);
                        }
                    }
                }
                #endregion


                //remarks 2020-03-01
                //    #region check Customer
                //    if (!listExistingCustomer.Exists(x => x.Equals(item.CustomerCode)))
                //    {
                //        if (listNotExistCustomer.Exists(x => x.Equals(item.CustomerCode)))
                //        {
                //            listRemarks.Add(string.Format(StaticMessage.ERR_CUSTOMER_NOT_FOUND, item.CustomerCode));
                //        }
                //        else
                //        {
                //            repoCustomer.Condition = PredicateBuilder.True<TCustomer>().And(x => x.CustomerCode.Equals(item.CustomerCode));

                //            customer = repoCustomer.Find().FirstOrDefault();

                //            if (customer == null)
                //            {
                //                listNotExistCustomer.Add(item.CustomerCode);
                //                listRemarks.Add(string.Format(StaticMessage.ERR_CUSTOMER_NOT_FOUND, item.CustomerCode));
                //            }
                //            else
                //            {
                //                listExistingCustomer.Add(customer.CustomerCode);
                //            }
                //        }
                //    }
                //    #endregion

                //    item.Remarks = String.Join("; ", listRemarks.ToArray());
            }

            int numError = listVM.Count(x => !string.IsNullOrEmpty(x.Remarks));

            model.ListCustomer = listVM;

            alert.Data = model;

            if (numError > 0)
            {
                alert.Status = 0;
                alert.Text = string.Format(StaticMessage.ERR_PREVIEW_CONTAINS_ERROR, numError);
            }
            else
            {
                alert.Status = 1;
            }

            return alert;
        }

        public AlertMessage PreviewTagih(ImportMasterListViewModel model)
        {
            AlertMessage alert = new AlertMessage();

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

            List<SalesCustomerViewModel> listVM = GetListSalesCustomerViewModel(model.InputFile);

            if (listVM == null)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            IRepository<SLM> repoSLM = _unitOfWork.GetRepository<SLM>();
            IRepository<TCustomer> repoCustomer = _unitOfWork.GetRepository<TCustomer>();

            List<int> listExistingSLM = new List<int>();
            List<int> listNotExistSLM = new List<int>();
            List<int> listNotActiveSLM = new List<int>();
            SLM slm = null;

            List<string> listExistingCustomer = new List<string>();
            List<string> listNotExistCustomer = new List<string>();
            TCustomer customer = null;

            List<string> listRemarks = null;

            DateTime today = DateTime.UtcNow.ToUtcID();

            foreach (var item in listVM)
            {
                listRemarks = new List<string>();

                if (!string.IsNullOrEmpty(item.Remarks)) continue;

                //#region check SLM
                //if (!listExistingSLM.Exists(x => x == item.SLMNik))
                //{
                //    if (listNotExistSLM.Exists(x => x == item.SLMNik))
                //    {
                //        listRemarks.Add(string.Format(StaticMessage.ERR_SLM_NOT_FOUND, item.SLMNik));
                //    }
                //    else if (listNotActiveSLM.Exists(x => x == item.SLMNik))
                //    {
                //        listRemarks.Add(string.Format(StaticMessage.ERR_SLM_NOT_ACTIVE, item.SLMNik));
                //    }
                //    else
                //    {
                //        repoSLM.Condition = PredicateBuilder.True<SLM>().And(x => x.NIK == item.SLMNik);

                //        slm = repoSLM.Find().FirstOrDefault();

                //        if (slm == null)
                //        {
                //            listNotExistSLM.Add(item.SLMNik);
                //            listRemarks.Add(string.Format(StaticMessage.ERR_SLM_NOT_FOUND, item.SLMNik));
                //        }
                //        else if (slm.ValidTo < today)
                //        {
                //            listNotActiveSLM.Add(slm.NIK);
                //            listRemarks.Add(string.Format(StaticMessage.ERR_SLM_NOT_ACTIVE, item.SLMNik));
                //        }
                //        else
                //        {
                //            listExistingSLM.Add(slm.NIK);
                //        }
                //    }
                //}
                //#endregion

                //#region check Customer
                //if (!listExistingCustomer.Exists(x => x.Equals(item.CustomerCode)))
                //{
                //    if (listNotExistCustomer.Exists(x => x.Equals(item.CustomerCode)))
                //    {
                //        listRemarks.Add(string.Format(StaticMessage.ERR_CUSTOMER_NOT_FOUND, item.CustomerCode));
                //    }
                //    else
                //    {
                //        repoCustomer.Condition = PredicateBuilder.True<TCustomer>().And(x => x.CustomerCode.Equals(item.CustomerCode));

                //        customer = repoCustomer.Find().FirstOrDefault();

                //        if (customer == null)
                //        {
                //            listNotExistCustomer.Add(item.CustomerCode);
                //            listRemarks.Add(string.Format(StaticMessage.ERR_CUSTOMER_NOT_FOUND, item.CustomerCode));
                //        }
                //        else
                //        {
                //            listExistingCustomer.Add(customer.CustomerCode);
                //        }
                //    }
                //}
                //#endregion

                item.Remarks = String.Join("; ", listRemarks.ToArray());
            }

            int numError = listVM.Count(x => !string.IsNullOrEmpty(x.Remarks));

            model.ListCustomer = listVM;

            alert.Data = model;

            if (numError > 0)
            {
                alert.Status = 0;
                alert.Text = string.Format(StaticMessage.ERR_PREVIEW_CONTAINS_ERROR, numError);
            }
            else
            {
                alert.Status = 1;
            }

            return alert;
        }
    }
}
