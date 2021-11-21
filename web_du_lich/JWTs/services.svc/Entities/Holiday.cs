using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Entities
{
    public class Holiday:BaseData
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime DayOff { get; set; }

    }
}
