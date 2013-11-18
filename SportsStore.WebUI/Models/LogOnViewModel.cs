using System;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Models
{
    public class LogOnViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public String UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Remember me")]
        public Boolean RememberMe { get; set; }
    }
}