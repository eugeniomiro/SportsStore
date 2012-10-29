// -----------------------------------------------------------------------
// <copyright file="ShippingDetails.cs" company="">
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
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter a name")]
        public String   Name        { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public String   Line1       { get; set; }
        public String   Line2       { get; set; }
        public String   Line3       { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public String   City        { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        public String   State       { get; set; }

        public String   Zip         { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public String   Country     { get; set; }

        public Boolean  GiftWrap    { get; set; }
    }
}
