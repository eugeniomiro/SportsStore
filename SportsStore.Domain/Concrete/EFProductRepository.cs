// -----------------------------------------------------------------------
// <copyright file="EFProductRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SportsStore.Domain.Concrete
{
    using System.Linq;
    using SportsStore.Domain.Abstract;
    using SportsStore.Domain.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EFProductRepository : IProductRepository
    {
        private readonly EfDbContext context = new EfDbContext();

        public IQueryable<Product> Products => context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            var prod = context.Products.Find(product.ProductID);
            if (prod != null)
            {
                context.Products.Remove(prod);
                context.SaveChanges();
            }
        }
    }
}
