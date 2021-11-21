using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Entities
{
    public class Company:BaseData
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string TaxCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Deputy { get; set; }
        public string Mobile { get; set; }
    }
}
