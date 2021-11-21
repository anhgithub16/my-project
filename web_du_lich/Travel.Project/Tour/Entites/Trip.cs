using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tour.Entites
{
    public class Trip:BaseData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string NoiDung { get; set; }
        public long GiaTrip { get; set; }
        public string DiemKhoiHanh { get; set; }
        public DateTime TimeKhoiHanh { get; set; }
        public int SoNgay { get; set; }
        public int SoDem { get; set; }
        public string HinhAnh { get; set; }
        public int Sale { get; set; }
    }
}