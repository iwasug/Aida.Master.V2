using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Radyalabs.Core.Helper;
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
    public class IncentiveCollectionBusiness : BaseBusiness
    {
        private void CreateSheetRaw(ref IWorkbook workbook, Dictionary<string, ICellStyle> dcCellStyle, List<CollectionReportRaw> listRaw, string sheetName)
        {
            int rowIndex = 0;

            ISheet sheet = workbook.CreateSheet(sheetName);

            IRow row = sheet.CreateRow(rowIndex);

            row = sheet.CreateRow(rowIndex);

            int cellIndex = 0;

            ICell cell = null;

            #region cell header
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
            cell.SetCellValue("Total Target Collect");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Total Actual Collect");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Percentage Collection");
            cell.CellStyle = dcCellStyle["header"];

            cell = row.CreateCell(cellIndex++);
            cell.SetCellValue("Collection Incentives");
            cell.CellStyle = dcCellStyle["header"];
            #endregion

            rowIndex++;

            #region cell data
            if (listRaw != null)
            {
                foreach (var item in listRaw)
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

                    // Total Target Collect
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.TotalTargetCollect);
                    cell.CellStyle = dcCellStyle["number"];

                    // Total Actual Collect
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.TotalActualCollect);
                    cell.CellStyle = dcCellStyle["number"];

                    // Percentage Collection
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.PercentageCollection);
                    cell.CellStyle = dcCellStyle["decimal"];

                    // Collection Incentives
                    cell = row.CreateCell(cellIndex++);
                    cell.SetCellValue((double)item.CollectionIncentives);
                    cell.CellStyle = dcCellStyle["number"];

                    rowIndex++;
                }
            }
            #endregion

            #region auto size column SLM Raw
            for (int i = 0; i < 7; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            #endregion
        }

        private void CreateSheetReportSummary(ref IWorkbook workbook, Dictionary<string, ICellStyle> dcCellStyle, List<CollectionReportSummary> listSummary, string sheetName)
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

        public AlertMessage ExportReport(string p, int? plant)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.IncentiveCollection))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            IncentiveCollectionReportViewModel model = GetReport(p, plant);

            if (model == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            string dateFormat = "yyyy/MM/dd";

            IWorkbook workbook = new XSSFWorkbook();

            Dictionary<string, ICellStyle> dcCellStyle = GetDcCellStyle(workbook);

            CreateSheetRaw(ref workbook, dcCellStyle, model.ListRawSLM, "SLM Raw");

            if (model.IsEnableSummarySLM)
            {
                CreateSheetReportSummary(ref workbook, dcCellStyle, model.ListSummarySLM, "Incentive SLM");
            }
            
            CreateSheetRaw(ref workbook, dcCellStyle, model.ListRawFSS, "FSS Raw");

            if (model.IsEnableSummaryFSS)
            {
                CreateSheetReportSummary(ref workbook, dcCellStyle, model.ListSummaryFSS, "Incentive FSS");
            }

            if (model.IsQuarter)
            {
                CreateSheetRaw(ref workbook, dcCellStyle, model.ListRawASM, "ASM Raw");

                if (model.IsEnableSummaryASM)
                {
                    CreateSheetReportSummary(ref workbook, dcCellStyle, model.ListSummaryASM, "Incentive ASM");
                }
            }

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);

                alert.Data = ms.ToArray();
            }

            alert.Status = 1;

            return alert;
        }

        public AlertMessage ExportReportCollector(string p, string b)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.IncentiveCollectionCollector))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            IncentiveCollectionReportViewModel model = GetReportCollector(p, b);

            if (model == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            IWorkbook workbook = new XSSFWorkbook();

            Dictionary<string, ICellStyle> dcCellStyle = GetDcCellStyle(workbook);

            CreateSheetRaw(ref workbook, dcCellStyle, model.ListRawData, "Collector Raw");

            CreateSheetReportSummary(ref workbook, dcCellStyle, model.ListSummaryData, "Collector");

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);

                alert.Data = ms.ToArray();
            }

            alert.Status = 1;

            return alert;
        }

        public AlertMessage ExportReportFakturis(string p, string b)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.IncentiveCollectionFakturis))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            IncentiveCollectionReportViewModel model = GetReportFakturis(p, b);

            if (model == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            IWorkbook workbook = new XSSFWorkbook();

            Dictionary<string, ICellStyle> dcCellStyle = GetDcCellStyle(workbook);

            CreateSheetRaw(ref workbook, dcCellStyle, model.ListRawData, "Fakturis Raw");

            CreateSheetReportSummary(ref workbook, dcCellStyle, model.ListSummaryData, "Fakturis");

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);

                alert.Data = ms.ToArray();
            }

            alert.Status = 1;

            return alert;
        }

        public AlertMessage ExportReportSPVFakturis(string p, string b)
        {
            AlertMessage alert = new AlertMessage();

            if (!IsAccessible(ModuleCode.IncentiveCollectionSPVFakturis))
            {
                alert.Text = StaticMessage.ERR_ACCESS_DENIED;
                return alert;
            }

            IncentiveCollectionReportViewModel model = GetReportSPVFakturis(p, b);

            if (model == null)
            {
                alert.Text = StaticMessage.ERR_DATA_NOT_FOUND;
                return alert;
            }

            IWorkbook workbook = new XSSFWorkbook();

            Dictionary<string, ICellStyle> dcCellStyle = GetDcCellStyle(workbook);

            CreateSheetRaw(ref workbook, dcCellStyle, model.ListRawData, "SPV Fakturis Raw");

            CreateSheetReportSummary(ref workbook, dcCellStyle, model.ListSummaryData, "SPV Fakturis");

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);

                alert.Data = ms.ToArray();
            }

            alert.Status = 1;

            return alert;
        }

        private string GetProcedureNameASM(string code)
        {
            string result = null;

            Dictionary<string, string> dcProcedure = new Dictionary<string, string>()
            {
                { SalesGroupRayonCode.All, "sp_apl_getCollectionASM_All_v2" },
                { SalesGroupRayonCode.MDD, "sp_apl_getCollectionASM_v2" },
                { SalesGroupRayonCode.SD1, "sp_apl_getCollectionASM_v2" },
                { SalesGroupRayonCode.SD2, "sp_apl_getCollectionASM_v2" },
                { SalesGroupRayonCode.GSV, "sp_apl_getCollectionASM_GSV_v2" },
                { SalesGroupRayonCode.PFV, "sp_apl_getCollectionASM_v2" },
            };

            if (dcProcedure.ContainsKey(code))
            {
                result = dcProcedure[code];
            }

            return result;
        }

        private string GetProcedureNameFSS(string code)
        {
            string result = null;

            Dictionary<string, string> dcProcedure = new Dictionary<string, string>()
            {
                { SalesGroupRayonCode.All, "sp_apl_getCollectionFSS_All_v2" },
                { SalesGroupRayonCode.MDD, "sp_apl_getCollectionFSS_v2" },
                { SalesGroupRayonCode.SD1, "sp_apl_getCollectionFSS_v2" },
                { SalesGroupRayonCode.SD2, "sp_apl_getCollectionFSS_v2" },
                { SalesGroupRayonCode.GSV, "sp_apl_getCollectionFSS_GSV_v2" },
                { SalesGroupRayonCode.PFV, "sp_apl_getCollectionFSS_PFV_v2" },
            };

            if (dcProcedure.ContainsKey(code))
            {
                result = dcProcedure[code];
            }

            return result;
        }

        private string GetProcedureNameSLM(string code)
        {
            string result = null;

            Dictionary<string, string> dcProcedure = new Dictionary<string, string>()
            {
                { SalesGroupRayonCode.All, "sp_apl_getCollectionSLM_All_v2" },
                { SalesGroupRayonCode.MDD, "sp_apl_getCollectionSLM_v2" },
                { SalesGroupRayonCode.SD1, "sp_apl_getCollectionSLM_v2" },
                { SalesGroupRayonCode.SD2, "sp_apl_getCollectionSLM_v2" },
                { SalesGroupRayonCode.GSV, "sp_apl_getCollectionSLM_GSV_v2" },
                { SalesGroupRayonCode.PFV, "sp_apl_getCollectionSLM_PFV_v2" },
            };

            if (dcProcedure.ContainsKey(code))
            {
                result = dcProcedure[code];
            }


            return result;
        }

        public IncentiveCollectionReportViewModel GetReport(string p, int? plant)
        {
            IncentiveCollectionReportViewModel result = null;

            InitReportResult(ref result, p, plant);

            if (result.Year == 0 || result.Month == 0)
            {
                return result;
            }

            if (result.Month % 3 == 0)
            {
                result.IsQuarter = true;
            }

            if (RoleCode.NSM.Equals(_userAuth.RoleCode) && plant == null)
            {
                //return result;
            }

            string connString = GetConnectionString();

            List<SqlParameter> listSqlParam = new List<SqlParameter>()
            {
                new SqlParameter("@inMonth", result.Month),
                new SqlParameter("@inYear", result.Year),
            };

            string code = GetSalesGroupRayonCode();

            string strListNSM = "";

            if (RoleCode.RM.Equals(_userAuth.RoleCode))
            {
                strListNSM = GetStrListNSM(result.Year, result.Month, "BUM", "@nik", _userAuth.NIK.ToString());

                if (string.IsNullOrEmpty(strListNSM)) return result;

                listSqlParam.Add(new SqlParameter("@listNIK", strListNSM));
            }
            else if (RoleCode.NSM.Equals(_userAuth.RoleCode))
            {
                strListNSM = _userAuth.NIK.ToString();

                if (string.IsNullOrEmpty(strListNSM)) return result;

                //listSqlParam.Add(new SqlParameter("@listNIK", strListNSM));
            }
            else if (RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                listSqlParam.Add(new SqlParameter("@listNIK", _userAuth.NIK));
            }

            #region report SLM
            string spSLM = null;

            #region GetSPAdminExclusiveSLM
            if (RoleCode.AdminExclusive.Equals(_userAuth.RoleCode))
            {
                spSLM = "sp_apl_getCollectionSLMRayonType";
                listSqlParam.Add(new SqlParameter("@nik", _userAuth.NIK));
                code = GetRayonTypeByNIK(_userAuth.NIK);
            }
            #endregion
            else
            {
                spSLM = GetProcedureNameSLM(code);
            }

            if (string.IsNullOrEmpty(spSLM))
            {
                return result;
            }

            if (SalesGroupRayonCode.DcEnableSummarySLM.ContainsKey(code))
            {
                result.IsEnableSummarySLM = SalesGroupRayonCode.DcEnableSummarySLM[code];
            }

            DataTableCollection dtCollectionSLM = SqlHelper.ExecuteProcedureWithReturnMultipleTable(connString, spSLM, listSqlParam);

            if (dtCollectionSLM != null)
            {
                result.ListRawSLM = GetCollectionReportRaw(dtCollectionSLM[0]);

                if (result.IsEnableSummarySLM)
                {
                    result.ListSummarySLM = GetReportListSummary(dtCollectionSLM[1]);
                }
                if (RoleCode.AdminExclusive.Equals(_userAuth.RoleCode))
                {
                    result.ListSummarySLM = GetReportListSummary(dtCollectionSLM[1]);
                    result.IsEnableSummarySLM = true;
                }
            }
            #endregion

            #region report FSS
            string spFSS = null;

            #region GetSPAdminExclusiveFSS
            if(RoleCode.AdminExclusive.Equals(_userAuth.RoleCode))
            {
                spFSS = "sp_apl_getCollectionFSSRayonType";
                //listSqlParam.Add(new SqlParameter("@nik", _userAuth.NIK));
                code = GetRayonTypeByNIK(_userAuth.NIK);
            }
            #endregion
            else
            {
                spFSS = GetProcedureNameFSS(code);
            }

            if (SalesGroupRayonCode.DcEnableSummaryFSS.ContainsKey(code))
            {
                result.IsEnableSummaryFSS = SalesGroupRayonCode.DcEnableSummaryFSS[code];
            }

            DataTableCollection dtCollectionFSS = SqlHelper.ExecuteProcedureWithReturnMultipleTable(connString, spFSS, listSqlParam);

            if (dtCollectionFSS != null)
            {
                result.ListRawFSS = GetCollectionReportRaw(dtCollectionFSS[0]);

                if (result.IsEnableSummaryFSS)
                {
                    result.ListSummaryFSS = GetReportListSummary(dtCollectionFSS[1]);
                }
                if (RoleCode.AdminExclusive.Equals(_userAuth.RoleCode))
                {
                    result.ListSummaryFSS = GetReportListSummary(dtCollectionFSS[1]);
                    result.IsEnableSummaryFSS = true;
                }
            }
            #endregion

            #region report ASM
            if (result.IsQuarter)
            {
                string spASM = GetProcedureNameASM(code);
                #region GetSPAdminExclusiveASM
                if (RoleCode.AdminExclusive.Equals(_userAuth.RoleCode))
                {
                    spASM = "sp_apl_getCollectionFSSRayonType";
                    listSqlParam.Add(new SqlParameter("@nik", _userAuth.NIK));
                    code = GetRayonTypeByNIK(_userAuth.NIK);
                }
                #endregion

                if (SalesGroupRayonCode.DcEnableSummaryASM.ContainsKey(code))
                {
                    result.IsEnableSummaryASM = SalesGroupRayonCode.DcEnableSummaryASM[code];
                }

                if (listSqlParam.FirstOrDefault(x=>x.ParameterName.Equals("@listNIK")) == null && !string.IsNullOrEmpty(strListNSM))
                {
                    listSqlParam.Add(new SqlParameter("@listNIK", strListNSM));
                }

                DataTableCollection dtCollectionASM = SqlHelper.ExecuteProcedureWithReturnMultipleTable(connString, spASM, listSqlParam);

                if (dtCollectionASM != null)
                {
                    result.ListRawASM = GetCollectionReportRaw(dtCollectionASM[0]);

                    if (result.IsEnableSummaryASM)
                    {
                        result.ListSummaryASM = GetReportListSummary(dtCollectionASM[1]);
                    }
                }
            }

            #endregion

            return result;
        }

        public void InitReportResult(ref IncentiveCollectionReportViewModel result, string p, int? plant = null)
        {
            result = new IncentiveCollectionReportViewModel()
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
            catch (Exception ex)
            {

            }
        }

        public IncentiveCollectionReportViewModel GetReportCollector(string p, string b)
        {
            IncentiveCollectionReportViewModel result = null;

            InitReportResult(ref result, p, string.IsNullOrEmpty(b) ? 0 : int.Parse(b));

            if (result.Year == 0 || result.Month == 0)
            {
                return result;
            }

            string connString = GetConnectionString();
          

            List<SqlParameter> listSqlParam = new List<SqlParameter>()
            {
                new SqlParameter("@inMonth", result.Month),
                new SqlParameter("@inYear", result.Year),
            };

            if (RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                listSqlParam.Add(new SqlParameter("@plant", _userAuth.Plant));
            }

            if (RoleCode.AdminOperation.Equals(_userAuth.RoleCode))
            {
                if (result.Plant != 0)
                    listSqlParam.Add(new SqlParameter("@plant", result.Plant));
            }

            #region report Collector
            string spName = "sp_apl_getCollectionCoL_v2";

            if (string.IsNullOrEmpty(spName))
            {
                return result;
            }

            DataTableCollection dtCollection = SqlHelper.ExecuteProcedureWithReturnMultipleTable(connString, spName, listSqlParam);

            if (dtCollection != null)
            {
                result.ListRawData = GetCollectionReportRaw(dtCollection[0]);

                if (dtCollection.Count > 1)
                {
                    result.ListSummaryData = GetReportListSummary(dtCollection[1]);
                }
            }
            #endregion

            return result;
        }

        public IncentiveCollectionReportViewModel GetReportFakturis(string p, string b)
        {
            IncentiveCollectionReportViewModel result = null;

            InitReportResult(ref result, p, string.IsNullOrEmpty(b) ? 0 : int.Parse(b));

            if (result.Year == 0 || result.Month == 0)
            {
                return result;
            }

            string connString = GetConnectionString();
            

            List<SqlParameter> listSqlParam = new List<SqlParameter>()
            {
                new SqlParameter("@inMonth", result.Month),
                new SqlParameter("@inYear", result.Year),
            };

            if (RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                listSqlParam.Add(new SqlParameter("@plant", _userAuth.Plant));
            }

            if (RoleCode.AdminOperation.Equals(_userAuth.RoleCode))
            {
                if(result.Plant != 0)
                    listSqlParam.Add(new SqlParameter("@plant", result.Plant));
            }

            #region report Collector
            string spName = "sp_apl_getCollectionFAK_v2";

            if (string.IsNullOrEmpty(spName))
            {
                return result;
            }

            DataTableCollection dtCollection = SqlHelper.ExecuteProcedureWithReturnMultipleTable(connString, spName, listSqlParam);

            if (dtCollection != null)
            {
                result.ListRawData = GetCollectionReportRaw(dtCollection[0]);

                if (dtCollection.Count > 1)
                {
                    result.ListSummaryData = GetReportListSummary(dtCollection[1]);
                }
            }
            #endregion

            return result;
        }

        public IncentiveCollectionReportViewModel GetReportSPVFakturis(string p, string b)
        {
            IncentiveCollectionReportViewModel result = null;

            InitReportResult(ref result, p, string.IsNullOrEmpty(b) ? 0 : int.Parse(b));

            if (result.Year == 0 || result.Month == 0)
            {
                return result;
            }

            string connString = GetConnectionString();

            List<SqlParameter> listSqlParam = new List<SqlParameter>()
            {
                new SqlParameter("@inMonth", result.Month),
                new SqlParameter("@inYear", result.Year),
            };

            if (RoleCode.KaCab.Equals(_userAuth.RoleCode))
            {
                listSqlParam.Add(new SqlParameter("@plant", _userAuth.Plant));
            }

            if (RoleCode.AdminOperation.Equals(_userAuth.RoleCode))
            {
                if (result.Plant != 0)
                    listSqlParam.Add(new SqlParameter("@plant", result.Plant));
            }

            #region report Collector
            string spName = "sp_apl_getCollectionSPVFAK_v2";

            if (string.IsNullOrEmpty(spName))
            {
                return result;
            }

            DataTableCollection dtCollection = SqlHelper.ExecuteProcedureWithReturnMultipleTable(connString, spName, listSqlParam);

            if (dtCollection != null)
            {
                result.ListRawData = GetCollectionReportRaw(dtCollection[0]);

                if (dtCollection.Count > 1)
                {
                    result.ListSummaryData = GetReportListSummary(dtCollection[1]);
                }
            }
            #endregion

            return result;
        }

        private List<CollectionReportSummary> GetReportListSummary(DataTable dt)
        {
            if (!(dt != null && dt.Rows.Count > 0)) return null;

            List<CollectionReportSummary> result = new List<CollectionReportSummary>();
            CollectionReportSummary item = null;

            foreach (DataRow dr in dt.Rows)
            {
                item = new CollectionReportSummary()
                {
                    Fullname = dr["FullName"].ToString()
                };

                item.NIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]);
                item.TotalIncentives = dr.IsNull("TotalIncentives") ? 0 : Convert.ToDecimal(dr["TotalIncentives"]);

                result.Add(item);
            }

            return result;
        }

        private List<CollectionReportRaw> GetCollectionReportRaw(DataTable dt)
        {
            if (!(dt != null && dt.Rows.Count > 0)) return null;

            List<CollectionReportRaw> result = new List<CollectionReportRaw>();
            CollectionReportRaw item = null;

            foreach (DataRow dr in dt.Rows)
            {
                item = new CollectionReportRaw()
                {
                    Description = dr["Description"].ToString(),
                    Fullname = dr["FullName"].ToString(),
                };
                item.NIK = dr.IsNull("NIK") ? 0 : Convert.ToInt32(dr["NIK"]);
                item.TotalTargetCollect = dr.IsNull("TotalTargetCollect") ? 0 : Convert.ToDecimal(dr["TotalTargetCollect"]);
                item.TotalActualCollect = dr.IsNull("TotalActualCollect") ? 0 : Convert.ToDecimal(dr["TotalActualCollect"]);
                item.PercentageCollection = dr.IsNull("PercentageCollection") ? 0 : Convert.ToDecimal(dr["PercentageCollection"]);
                item.CollectionIncentives = dr.IsNull("CollectionIncentives") ? 0 : Convert.ToDecimal(dr["CollectionIncentives"]);

                result.Add(item);
            }

            return result;
        }
    }
}