// -----------------------------------------------------------------------
// <copyright file="EfDbContext.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SportsStore.DataAccess.EntityFramework.Concrete
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Domain.Entities;

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
