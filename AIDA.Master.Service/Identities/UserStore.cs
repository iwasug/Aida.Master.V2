using AIDA.Master.Infrastucture.Data;
using Microsoft.AspNet.Identity;
using Radyalabs.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Identities
{
    public class UserStore : IUserLoginStore<IdentityUser, Guid>
        ,IUserClaimStore<IdentityUser, Guid>
        ,IUserRoleStore<IdentityUser, Guid>
        ,IUserPasswordStore<IdentityUser, Guid>
        ,IUserSecurityStampStore<IdentityUser, Guid>
        ,IUserStore<IdentityUser, Guid>
        ,IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserStore()
        {
            _unitOfWork = new UnitOfWork<_AIDAEntities>();
        }

        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            IRepository<TUser> repoUser = _unitOfWork.GetRepository<TUser>();
            repoUser.Condition = PredicateBuilder.True<TUser>().And(x=>x.UserName.Equals(userName));

            TUser user = repoUser.Find().FirstOrDefault();

            return Task.FromResult(GetIdentityUser(user));
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public IdentityUser GetIdentityUser(TUser user)
        {
            if (user == null) return null;

            IdentityUser identityUser = new IdentityUser();

            identityUser.Id = user.UserID;
            identityUser.UserName = user.UserName;
            identityUser.PasswordHash = user.PasswordHash;
            identityUser.SecurityStamp = user.SecurityStamp;

            return identityUser;
        }
    }
}
