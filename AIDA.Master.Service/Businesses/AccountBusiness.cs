using AIDA.Master.Infrastucture.Data;
using AIDA.Master.Service.Identities;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using Microsoft.AspNet.Identity;
using Radyalabs.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Businesses
{
    public class AccountBusiness : BaseBusiness
    {
        private readonly UserManager<IdentityUser, Guid> _userManager;

        public AccountBusiness() { }

        public AccountBusiness(UserManager<IdentityUser, Guid> userManager)
        {
            _userManager = userManager;
        }

        public AlertMessage Login(LoginViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (_userManager == null)
            {
                alert.Text = StaticMessage.ERR_GLOBAL;

                return alert;
            }
            //_logger.Write("info", DateTime.Now, "tes Login", model.Username);
            TUser user = null;
#if DEBUG
            var sss = _userManager.FindByNameAsync(model.Username);
            //user = GetUserByNIK(model.Username.Trim());
            user = GetUserById(sss.Result.Id);
#else
            var sss = _userManager.FindAsync(model.Username, model.Password);

            if (sss.Result == null)
            {
                alert.Text = string.Format(StaticMessage.ERR_LOGIN_FAILED);

                return alert;
            }

            user = GetUserById(sss.Result.Id);    
#endif


            if (user == null)
            {
                alert.Text = StaticMessage.ERR_USER_NOT_FOUND;

                return alert;
            }

            if (! (user.IsActive != (int?)null && user.IsActive.Value == 1))
            {
                alert.Text = StaticMessage.ERR_USER_INACTIVE;

                return alert;
            }

            #region set data to cookie

            AppCookieHelper.Set(sss.Result.Id, false/*model.IsRemember*/, model.Username);
            #endregion

            alert.Status = 1;

            return alert;
        }

        public void Logout()
        {
            AppCookieHelper.Remove();
        }
    }
}
