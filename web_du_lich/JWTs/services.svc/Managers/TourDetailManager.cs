using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class TourDetailManager
    {
        public static ITourDetailDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<ITourDetailDataProvider>();
                return _provider;
            }
        }
        public static ITourDetailDataProvider _provider;
        public static IEnumerable<TourDetail> GetByCityId(int cityId)
        {
            return provider.GetByCityId(cityId);
        }
        public static TourDetail GetById(string id)
        {
            return provider.GetById(id);
        }
        public static IEnumerable<TourDetail> Search(string keyword)
        {
            return provider.Search(keyword);
        }
        public static IEnumerable<TourDetail> GetAll()
        {
            return provider.GetAll();
        }
    }
}
