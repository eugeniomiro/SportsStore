using System;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Models
{
    public class LogOnViewModel
    {
        [Required]
        public String UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}