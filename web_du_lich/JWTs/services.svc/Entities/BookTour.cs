using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Entities
{
    public class BookTour:BaseData
    {
        public Guid Id { get; set; }
        public Guid KhachHangId { get; set; }
        public Guid TourDetailId { get; set; }
        public int NguoiLon { get; set; }
        public int TreEm { get; set; }
        public string ThongDiep { get; set; }
        public Guid TripId { get; set; }
        public long TongTien { get; set; }
        public string TenKhachHang { get; set; }
        public string TenTrip { get; set; }
        public string TenHotel { get; set; }

    }
}
