using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Entities
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
