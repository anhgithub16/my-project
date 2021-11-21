using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Entities
{
    public class Employees:BaseData
    {
        public string id { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
