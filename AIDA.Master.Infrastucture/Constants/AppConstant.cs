using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Infrastucture.Constants
{
    public class AppConstant
    {
        public const string ConnStringName = "_AIDAEntities";
        public const string DefaultValidTo = "9999-12-31";
        public const string DefaultManipulateBy = "AIDA_System";
        public const string DefaultFormatDate = "yyyy-MM-dd";
    }

    public static class RoleCode
    {
        public const string Admin = "ADM";
        public const string ASM = "ASM";
        public const string BUM = "BUM";
        public const string RM = "RM";
        public const string FSS = "FSS";
        public const string NSM = "NSM";
        public const string SLM = "SLM";
        public const string KaCab = "KCB";
        public const string AdminOperation = "AOP";
        public const string AdminExclusive = "ASL";
    }

    public class ModuleCode
    {
        public const string MasterSales = "MSL";
        public const string MasterSalesHier = "MSL-HIE";
        public const string MasterSalesList = "MSL-LST";
        public const string MasterSalesTarget = "MSL-TGT";
        public const string MasterTagih = "MTG";
        public const string MasterTagihHier = "MTG-HIE";
        public const string MasterTagihList = "MTG-LST";
        public const string Report = "RPT";
        public const string IncentiveSales = "RPT-INS";
        public const string IncentiveCollection = "RPT-INC";
        public const string IncentiveCollectionCollector = "RPT-INC-COL";
        public const string IncentiveCollectionFakturis = "RPT-INC-FAK";
        public const string IncentiveCollectionSPVFakturis = "RPT-INC-SPV";
        public const string Supervisor = "FSS";
        public const string ASM = "ASM";
        public const string Salesman = "SLM";
        public const string Collector = "COLLECTOR";
        public const string Fakturis = "FAKTURIS";
        public const string SPVFakturis = "SPV-FAK";
        public const string WorkingInstruction = "WI";
        public const string ImportCollection = "IMP-COL";

        public static Dictionary<string, List<string>> DcRole = new Dictionary<string, List<string>>()
        {
            { MasterSales, new List<string>() { RoleCode.FSS, RoleCode.ASM, RoleCode.RM, RoleCode.KaCab, RoleCode.NSM } }, //parent menu
            { MasterSalesHier, new List<string>() { RoleCode.FSS, RoleCode.ASM, RoleCode.RM, RoleCode.KaCab, RoleCode.NSM } },
            { MasterSalesList, new List<string>() { RoleCode.FSS, RoleCode.ASM, RoleCode.RM, RoleCode.KaCab, RoleCode.NSM } },
            { MasterSalesTarget, new List<string>() { RoleCode.FSS, RoleCode.ASM, RoleCode.RM, RoleCode.KaCab, RoleCode.NSM } },
            { MasterTagih, new List<string>() { RoleCode.FSS, RoleCode.RM, RoleCode.KaCab, RoleCode.NSM } }, //parent menu
            { MasterTagihHier, new List<string>() { RoleCode.FSS, RoleCode.RM, RoleCode.KaCab, RoleCode.NSM } },
            { MasterTagihList, new List<string>() { RoleCode.FSS, RoleCode.RM, RoleCode.KaCab, RoleCode.NSM } },
            { Supervisor, new List<string>() { RoleCode.ASM, RoleCode.KaCab, RoleCode.NSM } },
            { ASM, new List<string>() { RoleCode.KaCab, RoleCode.NSM } },
            { Salesman, new List<string>() { RoleCode.KaCab, RoleCode.ASM, RoleCode.FSS, RoleCode.NSM } },
            { Collector, new List<string>() { RoleCode.AdminOperation, RoleCode.KaCab } },
            { Fakturis, new List<string>() { RoleCode.AdminOperation, RoleCode.KaCab } },
            { SPVFakturis, new List<string>() { RoleCode.AdminOperation, RoleCode.KaCab } },
            { Report, new List<string>() { RoleCode.RM, RoleCode.AdminOperation, RoleCode.KaCab, RoleCode.AdminExclusive, RoleCode.NSM } }, //parent menu
            { IncentiveSales, new List<string>() { RoleCode.NSM, RoleCode.RM, RoleCode.KaCab, RoleCode.AdminExclusive } },
            { IncentiveCollection, new List<string>() { RoleCode.NSM, RoleCode.RM, RoleCode.KaCab, RoleCode.AdminExclusive } },
            { IncentiveCollectionCollector, new List<string>() { RoleCode.AdminOperation, RoleCode.KaCab } },
            { IncentiveCollectionFakturis, new List<string>() { RoleCode.AdminOperation, RoleCode.KaCab } },
            { IncentiveCollectionSPVFakturis, new List<string>() { RoleCode.AdminOperation, RoleCode.KaCab } },
            { WorkingInstruction, new List<string>(){ RoleCode.RM, RoleCode.NSM, RoleCode.KaCab, RoleCode.ASM, RoleCode.FSS, RoleCode.AdminOperation } },
            { ImportCollection, new List<string>() { RoleCode.AdminOperation } },
        };
    }

    public class PostedFileType
    {
        public const string XLS = ".xls";
        public const string XLSX = ".xlsx";

        public static Dictionary<string, List<string>> ContentTypes = new Dictionary<string, List<string>>()
        {
            { XLS, new List<string>(){ "application/vnd.ms-excel" } },
            { XLSX, new List<string>(){ "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" } }
        };
    }

    public class SalesGroupRayonCode
    {
        public const string MDD = "MDD";
        public const string SD1 = "SD1";
        public const string SD2 = "SD2";
        public const string GSV = "GSV";
        public const string PFV = "PFV";
        public const string All = "ALL";
        public const string AllByPlant = "PLN";

        public static Dictionary<string, bool> DcEnableSummarySLM = new Dictionary<string, bool>()
        {
            { AllByPlant, true },
            { All, true },
            { MDD, true },
            { SD1, true },
            { SD2, true },
            { GSV, false },
            { PFV, false }
        };

        public static Dictionary<string, bool> DcEnableSummaryFSS = new Dictionary<string, bool>()
        {
            { AllByPlant, true },
            { All, true },
            { MDD, true },
            { SD1, true },
            { SD2, true },
            { GSV, false },
            { PFV, false }
        };

        public static Dictionary<string, bool> DcEnableSummaryASM = new Dictionary<string, bool>()
        {
            { AllByPlant, true },
            { All, true },
            { MDD, true },
            { SD1, true },
            { SD2, true },
            { GSV, false },
            { PFV, false }
        };
    }

    public class EmailConstant
    {
        public const string SubjectPrefix = "";
        public const string ViewHirarkiSalesUploaded = "HirarkiSalesUploaded.html";
        public const string ViewMasterListSalesUploaded = "MasterListSalesUploaded.html";
        public const string ViewPartialMasterListDouble = "PartialMasterListDouble.html";
    }
}
