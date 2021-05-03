using System;
using System.Collections.Generic;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Ninject;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider, IDisposable
    {
        [Inject]
        public UserManager<ApplicationUser> UserManager { get; set; }

        public bool Authenticate(string username, string password)
        {
            var user = UserManager.FindAsync(username, password).Result;
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
            var result = UserManager.CreateAsync(user, password).Result;
            if (!result.Succeeded)
            {
                errors.AddRange(result.Errors);
            }
            return errors;
        }

        public void Dispose()
        {
            if (UserManager != null)
            {
                UserManager.Dispose();
            }
        }
    }
}