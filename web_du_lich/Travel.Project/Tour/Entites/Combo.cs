using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tour.Entites
{
    public class Combo:BaseData
    {
        public Guid Id { get; set; }
        public Guid TourDetailId { get; set; }
        public string TenCombo { get; set; }
        public string NoiDung { get; set; }
        public string Special { get; set; }
        public string DieuKien { get; set; }

    }
}