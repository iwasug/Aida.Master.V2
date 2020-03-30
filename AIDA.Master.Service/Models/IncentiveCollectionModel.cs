using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class IncentiveCollectionReportViewModel
    {
        public string FormattedMonthYear { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public bool IsQuarter { get; set; }

        public int? Plant { get; set; }

        public List<CollectionReportRaw> ListRawSLM { get; set; }

        public List<CollectionReportSummary> ListSummarySLM { get; set; }

        public bool IsEnableSummarySLM { get; set; }

        public List<CollectionReportRaw> ListRawFSS { get; set; }

        public List<CollectionReportSummary> ListSummaryFSS { get; set; }

        public bool IsEnableSummaryFSS { get; set; }

        public List<CollectionReportRaw> ListRawASM { get; set; }

        public List<CollectionReportSummary> ListSummaryASM { get; set; }

        public bool IsEnableSummaryASM { get; set; }

        public List<CollectionReportRaw> ListRawData { get; set; }

        public List<CollectionReportSummary> ListSummaryData { get; set; }
    }

    public class CollectionReportRaw
    {
        public string Description { get; set; }

        public int NIK { get; set; }

        public string Fullname { get; set; }

        //public string Role { get; set; }

        public Decimal TotalTargetCollect { get; set; }

        public Decimal TotalActualCollect { get; set; }

        public Decimal PercentageCollection { get; set; }

        public Decimal CollectionIncentives { get; set; }
    }

    //public class CollectionReportRawSLM : CollectionReportRaw
    //{
    //    public Decimal TotalActual { get; set; }
    //}

    //public class CollectionReportRawFSS : CollectionReportRaw
    //{
    //    public Decimal TotalSales { get; set; }
    //}

    //public class CollectionReportRawASM : CollectionReportRaw
    //{
    //    public Decimal TotalSales { get; set; }
    //}

    public class CollectionReportSummary
    {
        public int NIK { get; set; }

        public string Fullname { get; set; }

        public Decimal TotalIncentives { get; set; }
    }
}
