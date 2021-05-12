// -----------------------------------------------------------------------
// <copyright file="EfDbContext.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.DataAccess.EntityFramework.Concrete
{
    using Domain.Entities;
    using Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EfDbContext : IdentityDbContext<ApplicationUser>
    {
        public EfDbContext()
            : base("EfDbContext")
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
