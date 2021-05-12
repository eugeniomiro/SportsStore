using System;
using System.Collections.Generic;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    using Abstract;
    using DataAccess.EntityFramework.Models;

    public class FormsAuthProvider : IAuthProvider, IDisposable
    {
        public FormsAuthProvider(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public bool Authenticate(string username, string password)
        {
            var user = _userManager.FindAsync(username, password).Result;
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return true;
            }
            return false;
        }

        public IEnumerable<string> RegisterUser(ApplicationUser user, string password)
        {
            var errors = new List<string>();
            var result = _userManager.CreateAsync(user, password).Result;
            if (!result.Succeeded)
            {
                errors.AddRange(result.Errors);
            }
            return errors;
        }

        public void Dispose()
        {
            if (_userManager != null)
            {
                _userManager.Dispose();
            }
        }

        private readonly UserManager<ApplicationUser> _userManager;
    }
}
