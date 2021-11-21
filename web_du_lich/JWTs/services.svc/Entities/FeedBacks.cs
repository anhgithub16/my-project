using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Entities
{
    public class FeedBacks:BaseData
    {
        public string Id { get; set; }
        public string EmployeeCode { get; set; }
        public string Titles { get; set; }
        public string FeedBack { get; set; }
        public string Contents { get; set; }
        public string FileAttached { get; set; }
    }
}
