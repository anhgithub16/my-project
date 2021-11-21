using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class ScheduleManager
    {
        public static IScheduleDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<IScheduleDataProvider>();
                return _provider;
            }
        }
        public static IScheduleDataProvider _provider;
        public static IEnumerable<Schedule> GetAll()
        {
            return provider.GetAll();
        }
        public static IEnumerable<Schedule> GetByDay(string tripId,int day)
        {
            return provider.GetByDay(tripId,day);
        }
    }
}
