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
    public class HierSalesBusiness : BaseBusiness
    {
        private const string _formattedDate = "yyyy-MM-dd";
        private static DateTime _defaultValidTo = Convert.ToDateTime(AppConstant.DefaultValidTo);
        private IMailService _mailService;

        public HierSalesBusiness() { }

        public HierSalesBusiness(IMailService mailService)
        {
            _mailService = mailService;
        }

        public AlertMessage ExportSalesToExcel(JDatatableViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            model.Length = -1;

            JDatatableResponse response = GetDatatable(model);

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
            cell.SetCellValue("Rayon Type");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("RM NIK");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("RM Name");
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
            #endregion

            rowIndex++;

            #region cell data
            List<HierSalesViewModel> listData = response.Data as List<HierSalesViewModel>;

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

                    // RayonType
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.RayonType);
                    cell.CellStyle = cellStyleText;

                    // BUM NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.BUMNik);
                    cell.CellStyle = cellStyleNumber;

                    // BUM Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.BUMFullname);
                    cell.CellStyle = cellStyleText;

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

                    rowIndex++;
                }
            }
            #endregion

            #region auto size column
            for (int i = 0; i < 13; i++)
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

        public AlertMessage ExportSalesToExcelV2(JDatatableViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            model.Length = -1;

            JDatatableResponse response = GetDatatable(model);

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
            cell.SetCellValue("Rayon Type");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("RM NIK");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("RM Name");
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
            #endregion

            rowIndex++;

            #region cell data
            List<HierSalesViewModel> listData = response.Data as List<HierSalesViewModel>;

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

                    // RayonType
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.RayonType);
                    cell.CellStyle = cellStyleText;

                    // BUM NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.BUMNik);
                    cell.CellStyle = cellStyleNumber;

                    // BUM Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.BUMFullname);
                    cell.CellStyle = cellStyleText;

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

                    rowIndex++;
                }
            }
            #endregion

            #region auto size column
            for (int i = 0; i < 13; i++)
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
                ,"RayonType"
                ,"Plant"
                ,"SLM"
                ,"SLMObj1.FullName"
                ,"FSS"
                ,"FSSObj1.FullName"
                ,"ASM"
                ,"ASMObj1.FullName"
                ,"NSM"
                ,"NSMObj1.FullName"
                ,"BUM"
                ,"BUMObj1.FullName"
                ,"ValidFrom"
                ,"ValidTo"
            };

            if (_userAuth == null) return result;

            IRepository<RHHeader> repo = _unitOfWork.GetRepository<RHHeader>();
            repo.Includes = new string[] { "BUMObj1", "NSMObj1", "ASMObj1", "FSSObj1", "SLMObj1" };

            repo.Condition = PredicateBuilder.True<RHHeader>();

            if (RoleCode.RM.Equals(_userAuth.RoleCode))
            {
                repo.Condition = repo.Condition.And(x => x.BUM.Equals(_userAuth.NIK));
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode))
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
                repo.Condition = repo.Condition.And(x => x.NSM.Equals(_userAuth.NIK) /*|| x.Plant.Equals(_userAuth.Plant.Value.ToString())*/);
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
                    || x.RayonType.Contains(model.Keyword)
                    || x.Plant.Contains(model.Keyword)
                    || x.BUM.ToString().Contains(model.Keyword)
                    || x.BUMObj1.FullName.Contains(model.Keyword)
                    || x.NSM.ToString().Contains(model.Keyword)
                    || x.NSMObj1.FullName.Contains(model.Keyword)
                    || x.ASM.ToString().Contains(model.Keyword)
                    || x.ASMObj1.FullName.Contains(model.Keyword)
                    || x.FSS.ToString().Contains(model.Keyword)
                    || x.FSSObj1.FullName.Contains(model.Keyword)
                    || x.SLM.ToString().Contains(model.Keyword)
                    || x.SLMObj1.FullName.Contains(model.Keyword));
            }

            SetDatatableRepository(model, arrOrderColumn, ref repo, ref result);

            if (model.Length > -1 && result.TotalRecords == 0)
            {
                return result;
            }

            List<RHHeader> listItem = repo.Find();

            if (listItem == null) return result;

            List<HierSalesViewModel> listData = new List<HierSalesViewModel>();

            foreach (var item in listItem)
            {
                listData.Add(GetHierSalesViewModel(item));
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

        private HierSalesViewModel GetHierSalesViewModel(RHHeader item)
        {
            HierSalesViewModel model = new HierSalesViewModel()
            {
                Id = item.ID,
                RayonCode = item.RayonCode,
                RayonType = item.RayonType,
                PlantCode = item.Plant,
                BUMNik = item.BUM,
                NSMNik = item.NSM,
                ASMNik = item.ASM,
                FSSNik = item.FSS,
                SLMNik = item.SLM,
                ValidFromDate = item.ValidFrom,
                ValidToDate = item.ValidTo
            };

            model.FormattedValidFrom = item.ValidFrom.ToString(_formattedDate);
            model.FormattedValidTo = item.ValidTo.ToString(_formattedDate);

            model.BUMFullname = item.BUMObj1 == null ? null : item.BUMObj1.FullName;
            model.NSMFullname = item.NSMObj1 == null ? null : item.NSMObj1.FullName;
            model.ASMFullname = item.ASMObj1 == null ? null : item.ASMObj1.FullName;
            model.FSSFullname = item.FSSObj1 == null ? null : item.FSSObj1.FullName;
            model.SLMFullname = item.SLMObj1 == null ? null : item.SLMObj1.FullName;

            return model;
        }

        private List<HierSalesViewModel> GetListHierSalesViewModel(HttpPostedFileBase postedFile)
        {
            IWorkbook workbook = GetWorkbook(postedFile);

            if (workbook == null) return null;

            ISheet sheet = workbook.GetSheetAt(0);

            List<HierSalesViewModel> result = new List<HierSalesViewModel>();
            HierSalesViewModel model = null;

            object obj = null;
            int tempInt = 0;
            List<string> listRemarks = null;

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                tempInt = 0;
                listRemarks = new List<string>();

                try
                {
                    IRow row = sheet.GetRow(i);

                    if (row == null) continue;

                    model = new HierSalesViewModel();

                    obj = GetObjFromCell(row.GetCell(0));
                    model.RayonCode = obj == null ? null : obj.ToString().Trim();

                    if (string.IsNullOrEmpty(model.RayonCode))
                    {
                        listRemarks.Add("RayonCode kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(1));
                    model.PlantCode = obj == null ? null : obj.ToString().Trim();

                    if (string.IsNullOrEmpty(model.PlantCode))
                    {
                        listRemarks.Add("Plant kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(2));
                    model.RayonType = obj == null ? null : obj.ToString().Trim();

                    if (string.IsNullOrEmpty(model.RayonType))
                    {
                        listRemarks.Add("Rayon Type kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(3));
                    model.BUMNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());

                    if (model.BUMNik == 0)
                    {
                        listRemarks.Add("BUM NIK kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(4));
                    model.BUMFullname = obj == null ? null : obj.ToString().Trim();

                    if (string.IsNullOrEmpty(model.BUMFullname))
                    {
                        listRemarks.Add("BUM Name kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(5));
                    //model.NSMNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                    if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                    {
                        model.NSMNik = tempInt;
                    }

                    if (model.NSMNik == 0)
                    {
                        listRemarks.Add("NSM NIK kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(6));
                    model.NSMFullname = obj == null ? null : obj.ToString().Trim();

                    if (string.IsNullOrEmpty(model.NSMFullname))
                    {
                        listRemarks.Add("NSM Name kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(7));
                    //model.ASMNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                    if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                    {
                        model.ASMNik = tempInt;
                    }

                    if (model.ASMNik == 0)
                    {
                        listRemarks.Add("ASM NIK kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(8));
                    model.ASMFullname = obj == null ? null : obj.ToString().Trim();

                    if (string.IsNullOrEmpty(model.ASMFullname))
                    {
                        listRemarks.Add("ASM Name kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(9));
                    //model.FSSNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                    if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                    {
                        model.FSSNik = tempInt;
                    }

                    if (model.FSSNik == 0)
                    {
                        listRemarks.Add("FSS NIK kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(10));
                    model.FSSFullname = obj == null ? null : obj.ToString().Trim();

                    if (string.IsNullOrEmpty(model.FSSFullname))
                    {
                        listRemarks.Add("FSS Name kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(11));
                    //model.SLMNik = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());
                    if (obj != null && int.TryParse(obj.ToString().Trim(), out tempInt))
                    {
                        model.SLMNik = tempInt;
                    }

                    if (model.SLMNik == 0)
                    {
                        listRemarks.Add("SLM NIK kosong");
                    }

                    obj = GetObjFromCell(row.GetCell(12));
                    model.SLMFullname = obj == null ? null : obj.ToString().Trim();

                    if (string.IsNullOrEmpty(model.SLMFullname))
                    {
                        listRemarks.Add("SLM Name kosong");
                    }

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

        public async Task<AlertMessage> ImportSales(ImportSalesViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            _logger.Write("info", DateTime.Now, string.Format("Upload Master Hier Sales Total : {0}", model.ListHierSales.Count), _userAuth.Fullname);

            if (!IsAccessible(ModuleCode.MasterSales))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (!IsEditable())
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

            IRepository<UploadHier2> repoUploadHier = _unitOfWork.GetRepository<UploadHier2>();

            UploadHier2 hier = null;

            foreach (var item in model.ListHierSales)
            {
                item.Remarks = null;

                hier = new UploadHier2()
                {
                    RayonCode = item.RayonCode,
                    Plant = item.PlantCode,
                    RayonType = item.RayonType,
                    BUM = item.BUMNik,
                    NSM = item.NSMNik,
                    ASM = item.ASMNik,
                    FSS = item.FSSNik,
                    SLM = item.SLMNik,
                    CreatedByNIK = _userAuth.NIK
                };

                try
                {
                    repoUploadHier.Insert(hier, true);
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
                //dc = SqlHelper.ExecuteProcedureWithReturnRecords(connString, "sp_uploadHier", listSqlParam
                dc = SqlHelper.ExecuteProcedureWithReturnRecords(connString, "sp_uploadHier_v2", listSqlParam);

                if (dc.ContainsKey(0))
                {
                    isError = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Write("Error", DateTime.Now, ex.Message, _userAuth.Fullname, ex);
                isError = true;
            }
            #endregion

            if (isError)
            {
                // delete all records from table UploadML
                var condition = PredicateBuilder.True<UploadHier2>().And(x => x.CreatedByNIK == _userAuth.NIK);

                repoUploadHier.Delete(condition, true);
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
                int numSuccess = model.ListHierSales.Count(x => string.IsNullOrEmpty(x.Remarks));

                alert.Status = 1;
                alert.Text = string.Format(StaticMessage.SCS_IMPORT, numSuccess);

                await SendEmailUpload();
            }

            return alert;
        }

        private async Task SendEmailUpload()
        {
            #region send email
            NSM nsm = GetNsmByUserAuth();

            if (nsm == null) return;

            if (nsm.Email == null) return;

            DateTime today = DateTime.UtcNow.ToUtcID();

            string subject = EmailConstant.SubjectPrefix + string.Format("Upload Hirarki Sales Oleh {0} ({1})", _userAuth.Fullname, _userAuth.NIK);
            string view = EmailConstant.ViewHirarkiSalesUploaded;

            List<string> listTo = new List<string>() { nsm.Email };

            Dictionary<string, string> dcParams = new Dictionary<string, string>()
            {
                { "{:FULLNAME}", nsm.FullName },
                { "{:UPLOADER_NIK}", _userAuth.NIK.ToString() },
                { "{:UPLOADER_NAME}", _userAuth.Fullname },
                { "{:FORMATTED_DATE}", today.ToString("yyyy-MM-dd HH:mm") },
            };

            #if !DEBUG
            await _mailService.Send(view, dcParams, subject, listTo);
            #endif
            #endregion
        }



        public AlertMessage Preview(ImportSalesViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.MasterSales))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            if (!IsEditable())
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            List<HierSalesViewModel> listVM = GetListHierSalesViewModel(model.InputFile);

            if (listVM == null)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            IRepository<FSS> repoFSS = _unitOfWork.GetRepository<FSS>();
            IRepository<SLM> repoSLM = _unitOfWork.GetRepository<SLM>();
            FSS fss = null;
            SLM slm = null;

            DateTime dtMaxValidTo = DateTime.ParseExact(AppConstant.DefaultValidTo, AppConstant.DefaultFormatDate, _cultureInfo);

            Dictionary<int, string> dcExistingFSS = new Dictionary<int, string>();
            Dictionary<int, string> dcExistingSLM = new Dictionary<int, string>();

            DateTime today = DateTime.UtcNow.ToUtcID().Date;

            List<string> listRemarks = null;

            foreach (var item in listVM)
            {
                if (!string.IsNullOrEmpty(item.Remarks)) continue;

                if(dcExistingFSS.ContainsKey(item.FSSNik) && dcExistingSLM.ContainsKey(item.SLMNik))
                {
                    continue;
                }

                listRemarks = new List<string>();

                if (item.FSSNik != 0 && ! dcExistingFSS.ContainsKey(item.FSSNik))
                {
                    repoFSS.Condition = PredicateBuilder.True<FSS>().And(x => x.NIK == item.FSSNik);
                    repoFSS.OrderBy = new SqlOrderBy("CreatedOn", SqlOrderType.Descending);

                    fss = repoFSS.Find().FirstOrDefault();

                    if (fss == null)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_FSS_NOT_FOUND, item.FSSNik));
                    }
                    else if(fss.ValidTo < today)
                    {
                        listRemarks.Add(string.Format(StaticMessage.ERR_FSS_NOT_ACTIVE, item.FSSNik));
                    }
                    else
                    {
                        dcExistingFSS[fss.NIK] = fss.FullName;
                    }
                }

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

                if (listRemarks.Count > 0)
                {
                    item.Remarks = String.Join("; ", listRemarks.ToArray());
                }
            }

            int numError = listVM.Count(x => !string.IsNullOrEmpty(x.Remarks));

            if (numError > 0)
            {
                alert.Status = 0;
                alert.Text = string.Format(StaticMessage.ERR_PREVIEW_CONTAINS_ERROR, numError);
            }

            model.ListHierSales = listVM;

            alert.Data = model;

            return alert;
        }
        public bool IsEditable()
        {
            if (_userAuth == null) return false;

            if ((RoleCode.FSS.Equals(_userAuth.RoleCode) || RoleCode.ASM.Equals(_userAuth.RoleCode)) && _userAuth.IsAbleToWrite)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
