// -----------------------------------------------------------------------
// <copyright file="IOrderProcessor.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SportsStore.Domain.Abstract
{
    using Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
