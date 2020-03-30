using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class SidebarMenuModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }

        public string Module { get; set; }

        public List<SidebarMenuModel> ListSubMenu { get; set; }
    }
}
