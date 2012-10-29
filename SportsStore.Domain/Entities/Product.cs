// -----------------------------------------------------------------------
// <copyright file="Product.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SportsStore.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Product
    {
        public Int32 ProductID { get; set; }
        [StringLength(100)]
        [Required]
        public String Name { get; set; }
        [StringLength(500)]
        public String Description { get; set; }
        public decimal Price { get; set; }
        [StringLength(50)]
        public String Category { get; set; }
    }
}
