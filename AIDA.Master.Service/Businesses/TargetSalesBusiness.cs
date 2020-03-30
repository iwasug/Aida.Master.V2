using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Infrastucture.Data;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Radyalabs.Core.Helper;
using Radyalabs.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AIDA.Master.Service.Businesses
{
    public class TargetSalesBusiness : BaseBusiness
    {
        public JDatatableResponse GetDatatable(TargetSalesDatatableViewModel model)
        {
            JDatatableResponse result = new JDatatableResponse();

            string[] arrOrderColumn = new string[] { "RayonCode"
                ,"RayonCode"
                ,"SLM"
                ,"SLM"
                ,"FSS"
                ,"FSS"
                ,"AchiGroup"
                ,"Division"
                ,"Material"
                ,"Bulan"
                ,"Tahun"
                ,"Target"
            };
            int month = 0;
            int year = 0;
            try
            {
                string[] arr = model.Periode.Split('-');

                month = Convert.ToInt16(arr[0]);
                year = Convert.ToInt16(arr[1]);
            }
            catch (Exception ex)
            {
                return result;
            }

            if (_userAuth == null) return result;
            List<RHHeader> rhHeader = GetRHHeaderByUserAuth();

            var rCode = rhHeader.Select(r => r.RayonCode);

            IRepository<SalesTarget> repo = _unitOfWork.GetRepository<SalesTarget>();

            repo.Condition = PredicateBuilder.True<SalesTarget>();

            repo.Condition = repo.Condition.And(x => x.Bulan == month && x.Tahun == year);

            if(model.RayonCode.Equals("All"))
                repo.Condition = repo.Condition.And(x => rCode.Contains(x.RayonCode));
            else
                repo.Condition = repo.Condition.And(x => x.RayonCode.Equals(model.RayonCode));

            SetDatatableRepository(model, arrOrderColumn, ref repo, ref result);

            if (model.Length > -1 && result.TotalRecords == 0)
            {
                return result;
            }

            List<SalesTarget> listItem = repo.Find();

            if (listItem == null) return result;

            List<TargetSalesViewModel> listData = new List<TargetSalesViewModel>();

            foreach (var item in listItem)
            {
                listData.Add(GetTargetSalesViewModel(item, rhHeader));
            }

            result.Data = listData;

            return result;
        }

        private TargetSalesViewModel GetTargetSalesViewModel(SalesTarget item, List<RHHeader> rhHeaders)
        {
            TargetSalesViewModel model = new TargetSalesViewModel()
            {
                RayonCode = item.RayonCode,
                AchiGroup = item.AchiGroup,
                Material = item.Material,
                Division = item.Division,
                Target = item.Target.Value.ToString("N0"),
                SLM_NIK = item.SLM.Value,
                SLM_Name = rhHeaders.FirstOrDefault(x => x.RayonCode.Equals(item.RayonCode)).SLMObj1.FullName,
                FSS_NIK = item.FSS.Value,
                FSS_Name = rhHeaders.FirstOrDefault(x => x.RayonCode.Equals(item.RayonCode)).FSSObj1.FullName,
                Bulan = item.Bulan.Value,
                Tahun = item.Tahun.Value
            };

            return model;
        }

        public AlertMessage ExportTargetSalesToExcel(TargetSalesDatatableViewModel model)
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

            //										


            #region cell header
            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("FSS");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("FSS_Name");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("SLM");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("SLM_Name");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("RayonCode");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("AchiGroup");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Division");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Material");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Bulan");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Tahun");
            cell.CellStyle = cellStyleHeader;


            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Target");
            cell.CellStyle = cellStyleHeader;
            #endregion

            rowIndex++;

            #region cell data
            List<TargetSalesViewModel> listData = response.Data as List<TargetSalesViewModel>;

            if (listData != null)
            {
                foreach (var item in listData)
                {
                    row = sheet1.CreateRow(rowIndex);
                    cellIndex = 0;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.FSS_NIK);
                    cell.CellStyle = cellStyleNumber;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.FSS_Name);
                    cell.CellStyle = cellStyleNumber;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.SLM_NIK);
                    cell.CellStyle = cellStyleNumber;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.SLM_Name);
                    cell.CellStyle = cellStyleText;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.RayonCode);
                    cell.CellStyle = cellStyleText;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.AchiGroup);
                    cell.CellStyle = cellStyleText;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Division);
                    cell.CellStyle = cellStyleText;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Material);
                    cell.CellStyle = cellStyleText;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Bulan);
                    cell.CellStyle = cellStyleNumber;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Tahun);
                    cell.CellStyle = cellStyleNumber;

                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(double.Parse(item.Target));
                    cell.CellStyle = cellStyleNumber;
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

        public AlertMessage DownloadTemplate(int bulan, int tahun)
        {
            AlertMessage alert = new AlertMessage();

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
            cell.SetCellValue("FSS");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("FSS_Name");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("SLM");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("SLM_Name");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("RayonCode");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("AchiGroup");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Division");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Material");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Bulan");
            cell.CellStyle = cellStyleHeader;

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Tahun");
            cell.CellStyle = cellStyleHeader;


            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Target");
            cell.CellStyle = cellStyleHeader;
            #endregion

            rowIndex++;

            #region cell data
            List<RHHeader> rhHeader = GetRHHeaderByUserAuth();
            string strTemp = "select distinct a.fss, a.fss_name, a.slm, a.slm_name, a.RayonCode, ";
            strTemp += " b.achigroup, ";
            strTemp += " b.division, ";
            strTemp += " b.material ";
            strTemp += "from v_rhheader_fullname a ";
            strTemp += "join rhteam c on a.RayonCode = c.RayonCode ";
            strTemp += " and CAST(GETDATE() as DATE) >= c.ValidFrom and CAST(GETDATE() as DATE) <= c.ValidTo ";
            strTemp += " join rdetail b on a.RayonType = b.RayonType ";
            strTemp += " and CAST(GETDATE() as DATE) >= b.ValidFrom and CAST(GETDATE() as DATE) <= b.ValidTo ";
            strTemp += " where CAST(GETDATE() as DATE) >= a.ValidFrom and CAST(GETDATE() as DATE) <= a.ValidTo ";
            string strRayonCode = string.Join("|", rhHeader.Select(x => x.RayonCode).ToArray());

            strTemp += " and a.RayonCode in(SELECT value FROM STRING_SPLIT('" + strRayonCode + "', '|'))";

            string connString = GetConnectionString();

            DataTable dataTable = SqlHelper.ExecuteQuery(connString, strTemp, null);

            //DateTime now = DateTime.UtcNow.ToUtcID();

            //var rType = rhHeader.Select(r => r.RayonType).Distinct();

            //IRepository<RDetail> repo = _unitOfWork.GetRepository<RDetail>();

            //repo.Condition = PredicateBuilder.True<RDetail>();

            //repo.Condition = repo.Condition.And(x => rType.Contains(x.RayonType));

            //repo.Condition = repo.Condition.And(x => x.ValidTo >= now);

            //List<RDetail> rDetail = repo.Find().ToList();

            foreach (DataRow dr in dataTable.Rows)
            {
                row = sheet1.CreateRow(rowIndex);
                cellIndex = 0;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(dr.IsNull("fss") ? null : dr["fss"].ToString());
                cell.CellStyle = cellStyleNumber;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(dr.IsNull("fss_name") ? null : dr["fss_name"].ToString());
                cell.CellStyle = cellStyleNumber;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(dr.IsNull("slm") ? null : dr["slm"].ToString());
                cell.CellStyle = cellStyleNumber;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(dr.IsNull("slm_name") ? null : dr["slm_name"].ToString());
                cell.CellStyle = cellStyleText;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(dr.IsNull("RayonCode") ? null : dr["RayonCode"].ToString());
                cell.CellStyle = cellStyleText;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(dr.IsNull("achigroup") ? null : dr["achigroup"].ToString());
                cell.CellStyle = cellStyleText;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(dr.IsNull("division") ? null : dr["division"].ToString());
                cell.CellStyle = cellStyleText;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(dr.IsNull("material") ? null : dr["material"].ToString());
                cell.CellStyle = cellStyleText;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(bulan);
                cell.CellStyle = cellStyleNumber;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(tahun);
                cell.CellStyle = cellStyleNumber;

                cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(0);
                cell.CellStyle = cellStyleNumber;
                rowIndex++;
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

        public AlertMessage Preview(ImportTargetSalesViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.MasterSales))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            //if (!IsEditable())
            //{
            //    alert.Text = StaticMessage.ERR_ACCESS_DENIED;
            //    return alert;
            //}

            List<TargetSalesViewModel> listVM = GetListTargetSalesViewModel(model.InputFile);

            if (listVM == null)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            model.ListTargetSales = listVM;

            alert.Data = model;

            return alert;
        }

        private List<TargetSalesViewModel> GetListTargetSalesViewModel(HttpPostedFileBase postedFile)
        {
            IWorkbook workbook = GetWorkbook(postedFile);

            if (workbook == null) return null;

            ISheet sheet = workbook.GetSheetAt(0);

            List<TargetSalesViewModel> result = new List<TargetSalesViewModel>();
            TargetSalesViewModel model = null;

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


                    model = new TargetSalesViewModel();

                    obj = GetObjFromCell(row.GetCell(0));
                    model.FSS_NIK = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());

                    obj = GetObjFromCell(row.GetCell(1));
                    model.FSS_Name = obj == null ? null : obj.ToString().Trim();

                    obj = GetObjFromCell(row.GetCell(2));
                    model.SLM_NIK = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());


                    obj = GetObjFromCell(row.GetCell(3));
                    model.SLM_Name = obj == null ? null : obj.ToString().Trim();

                    obj = GetObjFromCell(row.GetCell(4));
                    model.RayonCode = obj == null ? null : obj.ToString().Trim();

                    obj = GetObjFromCell(row.GetCell(5));
                    model.AchiGroup = obj == null ? null : obj.ToString().Trim();

                    obj = GetObjFromCell(row.GetCell(6));
                    model.Division = obj == null ? null : obj.ToString().Trim();

                    obj = GetObjFromCell(row.GetCell(7));
                    model.Material = obj == null ? null : obj.ToString().Trim();

                    obj = GetObjFromCell(row.GetCell(8));
                    model.Bulan = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());

                    obj = GetObjFromCell(row.GetCell(9));
                    model.Tahun = obj == null ? 0 : Convert.ToInt32(obj.ToString().Trim());

                    obj = GetObjFromCell(row.GetCell(10));
                    model.Target = obj == null ? "0" : double.Parse(obj.ToString().Trim()).ToString("N0"); 
                }
                catch (Exception ex)
                {
                    //model.Remarks = StaticMessage.ERR_INVALID_ROW_XLS;
                }
                finally
                {
                    if (!string.IsNullOrEmpty(model.RayonCode))
                        result.Add(model);
                }
            }

            return result;
        }

        public AlertMessage ImportTargetSales(ImportTargetSalesViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.MasterSales))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            //if (!IsEditable())
            //{
            //    alert.Text = StaticMessage.ERR_ACCESS_DENIED;
            //    return alert;
            //}

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

            try
            {
                IRepository<SalesTarget> repoUploadTargetSales = _unitOfWork.GetRepository<SalesTarget>();
                //repoUploadTargetSales.Condition = PredicateBuilder.True<SLM>().And(x => x.NIK == model.NIK);

                var rayonCode = model.ListTargetSales.Select(x => x.RayonCode);

                repoUploadTargetSales.Condition = PredicateBuilder.True<SalesTarget>();

                repoUploadTargetSales.Condition = repoUploadTargetSales.Condition.And(x => x.Bulan == month && x.Tahun == year);

                repoUploadTargetSales.Condition = repoUploadTargetSales.Condition.And(x => rayonCode.Contains(x.RayonCode));

                var list2Del = repoUploadTargetSales.Find();

                _unitOfWork.BeginTransaction();

                if(list2Del != null)
                {
                    foreach(var item in list2Del)
                    {
                        repoUploadTargetSales.Delete(item);
                    }
                }

                SalesTarget target = null;
                foreach (var item in model.ListTargetSales)
                {
                    DateTime today = DateTime.UtcNow.ToUtcID();
                    target = new SalesTarget()
                    {
                        SLM = item.SLM_NIK,
                        FSS = item.FSS_NIK,
                        RayonCode = item.RayonCode,
                        AchiGroup = item.AchiGroup,
                        Material = item.Material,
                        Division = item.Division,
                        Bulan = month,
                        Tahun = year,
                        Target = float.Parse(item.Target),
                        UpdatedBy = _userAuth.Fullname,
                        UpdatedOn = today
                    };
                    repoUploadTargetSales.Insert(target);
                }

                _unitOfWork.Commit();

                alert.Status = 1;
                alert.Text = string.Format(StaticMessage.SCS_IMPORT, model.ListTargetSales.Count());
            }
            catch (DbEntityValidationException e)
            {
                alert.Text = e.Message;
                foreach (var eve in e.EntityValidationErrors)
                {
                    //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    //_logger.Write("ErrorImportTargetSales", DateTime.UtcNow, string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //    eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        //Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        //    ve.PropertyName, ve.ErrorMessage);
                        //_logger.Write("ErrorImportTargetSales", DateTime.UtcNow, string.Format("- Property: \"{0}\", Error: \"{1}\"",
                        //    ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            finally
            {
                _unitOfWork.Dispose();
            }

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
    }
}
