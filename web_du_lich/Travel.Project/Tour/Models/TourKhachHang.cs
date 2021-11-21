using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tour.Entites;

namespace Tour.Models
{
    public class TourKhachHang
    {
        public KhachHang khachHang { get; set; }
        public BookTour bookTour { get; set; }
        public DateTime TimeKhoiHanh { get; set; }
    }
}