// -----------------------------------------------------------------------
// <copyright file="EFProductRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SportsStore.Domain.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SportsStore.Domain.Abstract;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EFProductRepository : IProductRepository
    {
        private EfDbContext context = new EfDbContext();

        public IQueryable<Entities.Product> Products
        {
            get { return context.Products; }
        }
    }
}
