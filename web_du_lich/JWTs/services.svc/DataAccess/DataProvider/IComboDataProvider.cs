﻿using services.svc.Entities;
using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.DataAccess.DataProvider
{
    public interface IComboDataProvider:BaseDataProvider<Combo>
    {
        Combo GetByTourDetailId(string tourDetailId);
    }
}
