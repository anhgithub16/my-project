using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tour.Entites
{
    public class KhachHang:BaseData
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}