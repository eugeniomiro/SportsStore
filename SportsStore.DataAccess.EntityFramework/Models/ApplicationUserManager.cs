using Microsoft.AspNet.Identity;

namespace SportsStore.DataAccess.EntityFramework.Models
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) 
            : base(store)
        { }
    }
}
