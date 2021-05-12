using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.DataAccess.EntityFramework.Models
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(IdentityDbContext<ApplicationUser> context)
            : base(context)
        { }
    }
}
