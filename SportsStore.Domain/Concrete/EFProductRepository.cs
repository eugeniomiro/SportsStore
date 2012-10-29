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
    using SportsStore.Domain.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EFProductRepository : IProductRepository
    {
        private EfDbContext context = new EfDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0) {
                context.Products.Add(product);
            } else {
                context.Entry(product).State = System.Data.EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            Product prod = context.Products.Find(product.ProductID);
            if (prod != null) {
                context.Products.Remove(prod);
                context.SaveChanges();
            }
        }
    }
}
