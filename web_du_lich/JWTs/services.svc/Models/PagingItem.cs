using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Models
{
    public class PagingItem
    {
        public int OutRowsNumber { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int NumberOfPage { get; }
        public string OrderBy { get; set; } = "CreatedAt";
        public string DirectionSort { get; set; } = "DESC";
    }
}
