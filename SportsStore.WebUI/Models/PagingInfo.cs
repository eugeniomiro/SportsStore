using System;

namespace SportsStore.WebUI.Models
{
    public class PagingInfo
    {
        public Int32 TotalItems { get; set; }
        public Int32 ItemsPerPage { get; set; }
        public Int32 CurrentPage { get; set; }

        public Int32 TotalPages {
            get { return (Int32) Math.Ceiling((decimal) TotalItems / ItemsPerPage); }
        }
    }
}