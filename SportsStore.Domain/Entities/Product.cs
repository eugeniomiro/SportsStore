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
    using System.Web.Mvc;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public Int32 ProductID { get; set; }
        
        [StringLength(100)]
        [Required]
        public String Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public String Description { get; set; }
        public decimal Price { get; set; }
        
        [StringLength(50)]
        public String Category { get; set; }
    }
}
