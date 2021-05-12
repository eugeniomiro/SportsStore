// -----------------------------------------------------------------------
// <copyright file="IProductRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace SportsStore.Domain.Abstract
{
    using Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        void DeleteProduct(Product product);
    }
}
