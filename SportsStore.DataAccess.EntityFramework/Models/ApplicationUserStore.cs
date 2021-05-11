using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.DataAccess.EntityFramework.Models
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
    }
}
