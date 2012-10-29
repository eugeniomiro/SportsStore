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
        [Required(ErrorMessage = "Please enter a product name")]
        public String Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        [Required(ErrorMessage = "Please enter a description")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Please enter a price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Please specify a category")]
        [StringLength(50)]
        public String Category { get; set; }

        public Byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public String ImageMimeType { get; set; }
    }
}
