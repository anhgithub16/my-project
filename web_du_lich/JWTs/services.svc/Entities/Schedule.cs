using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Entities
{
    public class Schedule:BaseData
    {
        public Guid Id { get; set; }
        public Guid TripId { get; set; }
        public int Day { get; set; }
        public string Hour { get; set; }
        public string NoiDung { get; set; }
        public string MoTaDay { get; set; }
        public int Stt { get; set; }



    }
}
