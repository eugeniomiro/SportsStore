﻿// -----------------------------------------------------------------------
// <copyright file="IOrderProcessor.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SportsStore.Domain.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using SportsStore.Domain.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
