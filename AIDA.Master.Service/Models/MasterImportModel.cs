using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AIDA.Master.Service.Models
{
    public class MasterImportViewModel
    {
        [Required]
        public HttpPostedFileBase InputFile { get; set; }

        [Required]
        public string FormattedValidDate { get; set; }
    }

    public class ImportTargetSalesViewModel : MasterImportViewModel
    {
        public List<TargetSalesViewModel> ListTargetSales { get; set; }
    }

    public class ImportMasterListViewModel : MasterImportViewModel
    {
        public List<SalesCustomerViewModel> ListCustomer { get; set; }
    }

    public class ImportSalesViewModel : MasterImportViewModel
    {
        public List<HierSalesViewModel> ListHierSales { get; set; }
    }

    public class ImportTagihViewModel : MasterImportViewModel
    {
        public List<HierTagihViewModel> ListHierTagih { get; set; }
    }

    public class ClearDoubleViewModel
    {
        public string FormattedValidDate { get; set; }
        public List<int> ListId { get; set; }
    }
}
