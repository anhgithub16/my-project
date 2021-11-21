using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Models
{
    public class ExcutionResult
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object MetaData { get; set; }
    }
}
