﻿using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Models
{
    public class MailSetting
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}