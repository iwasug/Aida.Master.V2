using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class IncentiveSalesReportViewModel
    {
        public string FormattedMonthYear { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public bool IsQuarter { get; set; }

        public int? Plant { get; set; }

        public int Type { get; set; }

        public List<IncentiveReportRawSLM> ListRawSLM { get; set; }

        public List<IncentiveReportSummary> ListSummarySLM { get; set; }

        public List<IncentiveReportRawFSS> ListRawFSS { get; set; }

        public List<IncentiveReportSummary> ListSummaryFSS { get; set; }

        public List<IncentiveReportRawASM> ListRawASM { get; set; }

        public List<IncentiveReportSummary> ListSummaryASM { get; set; }
    }

    public class IncentiveReportRaw
    {
        public string Description { get; set; }

        public int NIK { get; set; }

        public string Fullname { get; set; }

        public string Role { get; set; }

        public Decimal TotalTarget { get; set; }

        public Decimal Achievement { get; set; }

        public Decimal IncentiveBudget { get; set; }

        public Decimal Incentives { get; set; }
    }

    public class IncentiveReportRawSLM : IncentiveReportRaw
    {
        public Decimal TotalActual { get; set; }
    }

    public class IncentiveReportRawFSS : IncentiveReportRaw
    {
        public Decimal TotalSales { get; set; }
    }

    public class IncentiveReportRawASM : IncentiveReportRaw
    {
        public Decimal TotalSales { get; set; }
    }

    public class IncentiveReportSummary
    {
        public int NIK { get; set; }

        public string Fullname { get; set; }

        public Decimal TotalIncentives { get; set; }
    }
}
