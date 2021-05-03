// -----------------------------------------------------------------------
// <copyright file="EfDbContext.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SportsStore.Domain.Concrete
{
    using System.Data.Entity;
    using SportsStore.Domain.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EfDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
