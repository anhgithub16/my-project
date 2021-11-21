using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Model
{
    public class ExcutionResultAuth
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object MetaData { get; set; }
    }
}
