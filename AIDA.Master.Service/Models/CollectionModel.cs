using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class ImportCollectionModel : MasterImportViewModel
    {
    }

    public class UploadCollectionModel
    {
        public int TAHUN { get; set; }
        public int BULAN { get; set; }
        public int PLANT { get; set; }
        public string REFERENCE { get; set; }
        public string CUSTOMER { get; set; }
        public DateTime DUEDATE { get; set; }
        public string CG1 { get; set; }
        public string PH3 { get; set; }
        public string MATERIAL { get; set; }
        public decimal AMOUNT_09 { get; set; }
        public string INTERV { get; set; }
    }
}
