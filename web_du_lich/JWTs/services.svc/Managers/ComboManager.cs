using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class ComboManager
    {
        public static IComboDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<IComboDataProvider>();
                return _provider;
            }
        }
        public static IComboDataProvider _provider;
        public static Combo GetByTourDetailId(string tourDetailId)
        {
            return provider.GetByTourDetailId(tourDetailId);
        }
    }
}
