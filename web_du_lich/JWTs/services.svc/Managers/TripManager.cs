using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class TripManager
    {
        public static ITripDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<ITripDataProvider>();
                return _provider;
            }
        }
        public static ITripDataProvider _provider;
        public static IEnumerable<Trip> GetAll()
        {
            return provider.GetAll();
        }
        public static Trip GetById(string id)
        {
            return provider.GetById(id);
        }
        public static IEnumerable<Trip> Search(string keyword)
        {
            return provider.Search(keyword);
        }
    }
}
