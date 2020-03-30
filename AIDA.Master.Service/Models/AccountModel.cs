using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsRemember { get; set; }
    }

    public class UserAuthenticated
    {
        public string LoginAccount { get; set; }

        public int NIK { get; set; }

        public string Fullname { get; set; }

        public bool IsActive { get; set; }

        public string RoleCode { get; set; }

        public bool IsRoleValid { get; set; }

        public bool IsAbleToWrite { get; set; }

        public List<string> ListModule { get; set; }

        public int? Plant { get; set; }
    }
}
