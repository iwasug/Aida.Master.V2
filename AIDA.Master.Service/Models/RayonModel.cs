using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class RayonViewModel
    {
        public string RayonCode { get; set; }

        public int SalesmanNIK { get; set; }

        public string SalesmanFullname { get; set; }
    }

    public class RayonTypeViewModel
    {
        public RayonTypeViewModel() { }

        public RayonTypeViewModel(string r)
        {
            RayonType = r;
        }

        public string RayonType { get; set; }
    }
}
