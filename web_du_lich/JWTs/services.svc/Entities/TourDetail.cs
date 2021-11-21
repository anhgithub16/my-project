using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Entities
{
    public class TourDetail:BaseData
    {
        public Guid Id { get; set; }
        public int TypeTourId { get; set; }
        public string TenTour { get; set; }
        public string GioiThieu { get; set; }
        public string NoiDung { get; set; }
        public long GiaTour { get; set; }
        public string DiemKhoiHanh { get; set; }
        public DateTime TimeKhoiHanh { get; set; }
        public int SoNgay { get; set; }
        public int SoDem { get; set; }
        public string HinhAnh { get; set; }
        public int Sale { get; set; }
        public int CityId { get; set; }

    }
}
