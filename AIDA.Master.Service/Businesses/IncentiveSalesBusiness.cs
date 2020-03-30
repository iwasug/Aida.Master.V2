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
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Businesses
{
    public class IncentiveSalesBusiness : BaseBusiness
    {
        private void CreateSheetRawASM(ref IWorkbook workbook, Dictionary<string, ICellStyle> dcCellStyle, List<IncentiveReportRawASM> listRawASM)
        {
            int rowIndex = 0;

            ISheet sheet = workbook.CreateSheet("ASM Raw");

            IRow row = sheet.CreateRow(rowIndex);

            row = sheet.CreateRow(rowIndex);

            int cellIndex = 0;

            ICell cell = null;

            #region cell header SLM Raw
            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Description");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("NIK");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("FullName");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Role");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Total Target");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Total Sales");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Achievement");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Incentive Budget");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Incetives");
            cell.CellStyle = dcCellStyle["header"];
            #endregion

            rowIndex++;

            #region cell data SLM Raw
            if (listRawASM != null)
            {
                foreach (var item in listRawASM)
                {
                    row = sheet.CreateRow(rowIndex);
                    cellIndex = 0;

                    // Description
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Description);
                    cell.CellStyle = dcCellStyle["text"];

                    // NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.NIK);
                    cell.CellStyle = dcCellStyle["number"];

                    // Full Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Fullname);
                    cell.CellStyle = dcCellStyle["text"];

                    // Role
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Role);
                    cell.CellStyle = dcCellStyle["text"];

                    // Total Target
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.TotalTarget);
                    cell.CellStyle = dcCellStyle["number"];

                    // Total Sales
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.TotalSales);
                    cell.CellStyle = dcCellStyle["number"];

                    // Achievement
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.Achievement);
                    cell.CellStyle = dcCellStyle["decimal"];

                    // Incentive Budget
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.IncentiveBudget);
                    cell.CellStyle = dcCellStyle["number"];

                    // Incetives
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.Incentives);
                    cell.CellStyle = dcCellStyle["number"];

                    rowIndex++;
                }
            }
            #endregion

            #region auto size column SLM Raw
            for (int i = 0; i < 9; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            #endregion
        }

        private void CreateSheetRawFSS(ref IWorkbook workbook, Dictionary<string, ICellStyle> dcCellStyle, List<IncentiveReportRawFSS> listRawFSS)
        {
            int rowIndex = 0;

            ISheet sheet = workbook.CreateSheet("FSS Raw");

            IRow row = sheet.CreateRow(rowIndex);

            row = sheet.CreateRow(rowIndex);

            int cellIndex = 0;

            ICell cell = null;

            #region cell header SLM Raw
            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Description");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("NIK");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("FullName");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Role");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Total Target");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Total Sales");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Achievement");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Incentive Budget");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Incetives");
            cell.CellStyle = dcCellStyle["header"];
            #endregion

            rowIndex++;

            #region cell data SLM Raw
            if (listRawFSS != null)
            {
                foreach (var item in listRawFSS)
                {
                    row = sheet.CreateRow(rowIndex);
                    cellIndex = 0;

                    // Description
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Description);
                    cell.CellStyle = dcCellStyle["text"];

                    // NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.NIK);
                    cell.CellStyle = dcCellStyle["number"];

                    // Full Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Fullname);
                    cell.CellStyle = dcCellStyle["text"];

                    // Role
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Role);
                    cell.CellStyle = dcCellStyle["text"];

                    // Total Target
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.TotalTarget);
                    cell.CellStyle = dcCellStyle["number"];

                    // Total Sales
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.TotalSales);
                    cell.CellStyle = dcCellStyle["number"];

                    // Achievement
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.Achievement);
                    cell.CellStyle = dcCellStyle["decimal"];

                    // Incentive Budget
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.IncentiveBudget);
                    cell.CellStyle = dcCellStyle["number"];

                    // Incetives
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.Incentives);
                    cell.CellStyle = dcCellStyle["number"];

                    rowIndex++;
                }
            }
            #endregion

            #region auto size column SLM Raw
            for (int i = 0; i < 9; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            #endregion
        }

        private void CreateSheetRawSLM(ref IWorkbook workbook, Dictionary<string, ICellStyle> dcCellStyle, List<IncentiveReportRawSLM> listRawSLM)
        {
            int rowIndex = 0;

            ISheet sheet = workbook.CreateSheet("SLM Raw");

            IRow row = sheet.CreateRow(rowIndex);

            row = sheet.CreateRow(rowIndex);

            int cellIndex = 0;

            ICell cell = null;

            #region cell header SLM Raw
            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Description");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("NIK");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("FullName");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Role");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Total Target");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Total Actual");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Achievement");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Incentive Budget");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Incetives");
            cell.CellStyle = dcCellStyle["header"];
            #endregion

            rowIndex++;

            #region cell data SLM Raw
            if (listRawSLM != null)
            {
                foreach (var item in listRawSLM)
                {
                    row = sheet.CreateRow(rowIndex);
                    cellIndex = 0;

                    // Description
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Description);
                    cell.CellStyle = dcCellStyle["text"];

                    // NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.NIK);
                    cell.CellStyle = dcCellStyle["number"];

                    // Full Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Fullname);
                    cell.CellStyle = dcCellStyle["text"];

                    // Role
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Role);
                    cell.CellStyle = dcCellStyle["text"];

                    // Total Target
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.TotalTarget);
                    cell.CellStyle = dcCellStyle["number"];

                    // Total Actual
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.TotalActual);
                    cell.CellStyle = dcCellStyle["number"];

                    // Achievement
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.Achievement);
                    cell.CellStyle = dcCellStyle["decimal"];

                    // Incentive Budget
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.IncentiveBudget);
                    cell.CellStyle = dcCellStyle["number"];

                    // Incetives
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.Incentives);
                    cell.CellStyle = dcCellStyle["number"];

                    rowIndex++;
                }
            }
            #endregion

            #region auto size column SLM Raw
            for (int i = 0; i < 9; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            #endregion
        }

        private void CreateSheetReportSummary(ref IWorkbook workbook, Dictionary<string, ICellStyle> dcCellStyle, List<IncentiveReportSummary> listSummary, string sheetName)
        {
            int rowIndex = 0;

            ISheet sheet = workbook.CreateSheet(sheetName);

            IRow row = sheet.CreateRow(rowIndex);

            row = sheet.CreateRow(rowIndex);

            int cellIndex = 0;

            ICell cell = null;

            #region cell header SLM Raw
            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("NIK");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("FullName");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Total Incentive");
            cell.CellStyle = dcCellStyle["header"];
            #endregion

            rowIndex++;

            #region cell data
            if (listSummary != null)
            {
                foreach (var item in listSummary)
                {
                    row = sheet.CreateRow(rowIndex);
                    cellIndex = 0;

                    // NIK
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.NIK);
                    cell.CellStyle = dcCellStyle["number"];

                    // Full Name
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue(item.Fullname);
                    cell.CellStyle = dcCellStyle["text"];

                    // Total Incentive
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.TotalIncentives);
                    cell.CellStyle = dcCellStyle["number"];
                    rowIndex++;
                }
            }
            #endregion

            #region auto size column SLM Raw
            for (int i = 0; i < 3; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            #endregion
        }

        public AlertMessage ExportReport(string p, int? t, int? plant)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.IncentiveSales))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            IncentiveSalesReportViewModel model = GetReport(p, t, plant);

            if (model == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            IWorkbook workbook = new XSSFWorkbook();

            Dictionary<string, ICellStyle> dcCellStyle = GetDcCellStyle(workbook);

            CreateSheetRawSLM(ref workbook, dcCellStyle, model.ListRawSLM);
            CreateSheetReportSummary(ref workbook, dcCellStyle, model.ListSummarySLM, "Incentive SLM");
            CreateSheetRawFSS(ref workbook, dcCellStyle, model.ListRawFSS);
            CreateSheetReportSummary(ref workbook, dcCellStyle, model.ListSummaryFSS, "Incentive FSS");

            if (model.IsQuarter)
            {
                CreateSheetRawASM(ref workbook, dcCellStyle, model.ListRawASM);
                CreateSheetReportSummary(ref workbook, dcCellStyle, model.ListSummaryASM, "Incentive ASM");
            }

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);

                alert.Data = ms.ToArray();
            }

            alert.Status = 1;

            return alert;
        }

        private string GetProcedureName()
        {
            string result = null;

            string code = GetSalesGroupRayonCode();

            Dictionary<string, string> dcNSMProcedure = new Dictionary<string, string>()
            {
                { SalesGroupRayonCode.All, "sp_apl_getReportSalesIncentiveAll_v2" },
                { SalesGroupRayonCode.GSV, "sp_apl_getReportSalesIncentiveGSV_v2" },
                { SalesGroupRayonCode.PFV, "sp_apl_getReportSalesIncentivePFV_v2" },
                { SalesGroupRayonCode.MDD, "sp_apl_getReportSalesIncentiveMDD_v2" },
                { SalesGroupRayonCode.SD1, "sp_apl_getReportSalesIncentiveSD1_v2" },
                { SalesGroupRayonCode.SD2, "sp_apl_getReportSalesIncentiveSD2_v2" },
            };

            if (RoleCode.BUM.Equals(_userAuth.RoleCode)
                || RoleCode.RM.Equals(_userAuth.RoleCode)
                || RoleCode.NSM.Equals(_userAuth.RoleCode)
                || RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                if (dcNSMProcedure.ContainsKey(code))
                {
                    result = dcNSMProcedure[code];
                }
            }

            return result;
        }

        public IncentiveSalesReportViewModel GetReport(string p, int? type, int? plant)
        {
            IncentiveSalesReportViewModel result = new IncentiveSalesReportViewModel()
            {
                //FormattedMonthYear = DateTime.UtcNow.ToUtcID().ToString("MM-yyyy"),
                FormattedMonthYear = "",
                Plant = plant
            };

            if (!string.IsNullOrEmpty(p))
            {
                result.FormattedMonthYear = p;
            }

            try
            {
                string[] arr = result.FormattedMonthYear.Split('-');

                result.Month = Convert.ToInt16(arr[0]);
                result.Year = Convert.ToInt16(arr[1]);
            }
            catch (Exception)
            {
                return result;
            }

            if (result.Year == 0 || result.Month == 0)
            {
                return result;
            }

            if (result.Month % 3 == 0)
            {
                result.IsQuarter = true;
            }

            if ((type != null && type.Value == 1) || RoleCode.RM.Equals(_userAuth.RoleCode))
            {
                result.Type = 1;
            }
            else
            {
                result.Type = 0;
            }

            if (RoleCode.NSM.Equals(_userAuth.RoleCode) && result.Plant == null)
            {
                //return result;
            }

            //string sp = GetProcedureName();
            string sp = "sp_apl_getReportSalesIncentiveAll_v2";

            if (result.Type == 1)
            {
                sp = "sp_apl_getReportSalesIncentiveMDD_v2";
            }


            if (string.IsNullOrEmpty(sp))
            {
                return result;
            }

            string connString = GetConnectionString();

            List<SqlParameter> listSqlParam = new List<SqlParameter>()
            {
                new SqlParameter("@inBulan", result.Month),
                new SqlParameter("@inTahun", result.Year),
            };

            if (RoleCode.RM.Equals(_userAuth.RoleCode))
            {
                string strListNSM = GetStrListNSM(result.Year, result.Month, "BUM", "@nik", _userAuth.NIK.ToString());

                if (string.IsNullOrEmpty(strListNSM)) return result;

                listSqlParam.Add(new SqlParameter("@listNIK", strListNSM));
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode))
            {
                string strListNSM = _userAuth.NIK.ToString();
                //if (result.Plant == null)
                //    strListNSM = GetStrListNSM(result.Year, result.Month);
                //else
                //    strListNSM = GetStrListNSM(result.Year, result.Month, "Plant", "@plant", result.Plant.Value.ToString());

                //strListNSM = 

                if (string.IsNullOrEmpty(strListNSM)) return result;

                listSqlParam.Add(new SqlParameter("@listNIK", strListNSM));
            }
            else if (RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                listSqlParam.Add(new SqlParameter("@listNIK", _userAuth.NIK));
            }

            if (RoleCode.AdminExclusive.Equals(_userAuth.RoleCode))
            {
                sp = "sp_apl_getReportSalesIncentiveRayonType";
                listSqlParam.Add(new SqlParameter("@nik", _userAuth.NIK));
            }

            DataTableCollection dtCollection = SqlHelper.ExecuteProcedureWithReturnMultipleTable(connString, sp, listSqlParam);

            if (dtCollection != null)
            {
                result.ListRawSLM = GetReportListRawSLM(dtCollection[0]);
                result.ListSummarySLM = GetReportListSummary(dtCollection[1]);
                result.ListRawFSS = GetReportListRawFSS(dtCollection[2]);
                result.ListSummaryFSS = GetReportListSummary(dtCollection[3]);

                if (result.IsQuarter)
                {
                    result.ListRawASM = GetReportListRawASM(dtCollection[4]);
                    result.ListSummaryASM = GetReportListSummary(dtCollection[5]);
                }
            }

            return result;
        }

        private List<IncentiveReportSummary> GetReportListSummary(DataTable dt)
        {
            if (!(dt != null && dt.Rows.Count > 0)) return null;

            List<IncentiveReportSummary> result = new List<IncentiveReportSummary>();
            IncentiveReportSummary item = null;

            foreach (DataRow dr in dt.Rows)
            {
                item = new IncentiveReportSummary()
                {
                    Fullname = dr["FullName"].ToString()
                };

                item.NIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]);
                item.TotalIncentives = dr.IsNull("TotalIncentives") ? 0 : Convert.ToDecimal(dr["TotalIncentives"]);

                result.Add(item);
            }

            return result;
        }

        private List<IncentiveReportRawASM> GetReportListRawASM(DataTable dt)
        {
            if (!(dt != null && dt.Rows.Count > 0)) return null;

            List<IncentiveReportRawASM> result = new List<IncentiveReportRawASM>();
            IncentiveReportRawASM item = null;

            foreach (DataRow dr in dt.Rows)
            {
                item = new IncentiveReportRawASM()
                {
                    Description = dr["Description"].ToString(),
                    Fullname = dr["FullName"].ToString(),
                    Role = dr["Role"].ToString(),
                };
                item.NIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]);
                item.TotalTarget = dr.IsNull("TotalTarget") ? 0 : Convert.ToDecimal(dr["TotalTarget"]);
                item.TotalSales = dr.IsNull("TotalSales") ? 0 : Convert.ToDecimal(dr["TotalSales"]);
                item.Achievement = dr.IsNull("Achievement") ? 0 : Convert.ToDecimal(dr["Achievement"]);
                item.IncentiveBudget = dr.IsNull("IncentiveBudget") ? 0 : Convert.ToDecimal(dr["IncentiveBudget"]);
                item.Incentives = dr.IsNull("Incentives") ? 0 : Convert.ToDecimal(dr["Incentives"]);

                result.Add(item);
            }

            return result;
        }

        private List<IncentiveReportRawFSS> GetReportListRawFSS(DataTable dt)
        {
            if (!(dt != null && dt.Rows.Count > 0)) return null;

            List<IncentiveReportRawFSS> result = new List<IncentiveReportRawFSS>();
            IncentiveReportRawFSS item = null;

            foreach (DataRow dr in dt.Rows)
            {
                item = new IncentiveReportRawFSS()
                {
                    Description = dr["Description"].ToString(),
                    Fullname = dr["FullName"].ToString(),
                    Role = dr["Role"].ToString(),
                };
                item.NIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]);
                item.TotalTarget = dr.IsNull("TotalTarget") ? 0 : Convert.ToDecimal(dr["TotalTarget"]);
                item.TotalSales = dr.IsNull("TotalSales") ? 0 : Convert.ToDecimal(dr["TotalSales"]);
                item.Achievement = dr.IsNull("Achievement") ? 0 : Convert.ToDecimal(dr["Achievement"]);
                item.IncentiveBudget = dr.IsNull("IncentiveBudget") ? 0 : Convert.ToDecimal(dr["IncentiveBudget"]);
                item.Incentives = dr.IsNull("Incentives") ? 0 : Convert.ToDecimal(dr["Incentives"]);

                result.Add(item);
            }

            return result;
        }

        private List<IncentiveReportRawSLM> GetReportListRawSLM(DataTable dt)
        {
            if(! (dt != null && dt.Rows.Count > 0)) return null;

            List<IncentiveReportRawSLM> result = new List<IncentiveReportRawSLM>();
            IncentiveReportRawSLM item = null;

            foreach (DataRow dr in dt.Rows)
            {
                item = new IncentiveReportRawSLM()
                {
                    Description = dr["Description"].ToString(),
                    Fullname = dr["FullName"].ToString(),
                    Role = dr["Role"].ToString(),
                };
                item.NIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]);
                item.TotalTarget = dr.IsNull("TotalTarget") ? 0 : Convert.ToDecimal(dr["TotalTarget"]);
                item.TotalActual = dr.IsNull("TotalActual") ? 0 : Convert.ToDecimal(dr["TotalActual"]);
                item.Achievement = dr.IsNull("Achievement") ? 0 : Convert.ToDecimal(dr["Achievement"]);
                item.IncentiveBudget = dr.IsNull("IncentiveBudget") ? 0 : Convert.ToDecimal(dr["IncentiveBudget"]);
                item.Incentives = dr.IsNull("Incentives") ? 0 : Convert.ToDecimal(dr["Incentives"]);

                result.Add(item);
            }

            return result;
        }
    }
}