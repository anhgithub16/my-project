using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Models
{
    public class BaseData
    {
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual int IsDeleted { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
    }
}
