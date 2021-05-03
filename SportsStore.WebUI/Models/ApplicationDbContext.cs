using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.WebUI.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("EfDbContext")
        { }
    }
}