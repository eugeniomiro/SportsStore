// -----------------------------------------------------------------------
// <copyright file="EFProductRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace SportsStore.DataAccess.EntityFramework.Concrete
{
    using Domain.Abstract;
    using Domain.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EFProductRepository : IProductRepository
    {
        public EFProductRepository(EfDbContext context)
        {
            _context = context;
        }

        private readonly EfDbContext _context;

        public IQueryable<Product> Products => _context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            var prod = _context.Products.Find(product.ProductID);
            if (prod != null)
            {
                _context.Products.Remove(prod);
                _context.SaveChanges();
            }
        }
    }
}
