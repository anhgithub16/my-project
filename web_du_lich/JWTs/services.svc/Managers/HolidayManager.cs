using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Models;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class HolidayManager
    {
        public static IHolidayDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<IHolidayDataProvider>();
                return _provider;

            }
        }
        public static IHolidayDataProvider _provider;
        public static Holiday GetById(string id)
        {
            return provider.GetById(id);
        }
        public static IEnumerable<Holiday> GetAll()
        {
            return provider.GetAll();
        }
        public static IEnumerable<Holiday> GetAllByPaging(PagingItem pagingItem)
        {
            return provider.GetAllByPaging(pagingItem);
        }
        public static IEnumerable<Holiday> Search(string keyword)
        {
            return provider.Search(keyword);
        }
        public static ExcutionResult Insert(Holiday holiday)
        {
            ExcutionResult rowAffected = provider.Insert(holiday);
            return rowAffected;
        }
        public static ExcutionResult Update(Holiday holiday)
        {
            ExcutionResult rowAffected = provider.Update(holiday);
            return rowAffected;
        }
        public static ExcutionResult Delete(Holiday holiday)
        {
            ExcutionResult rowAffected = provider.Delete(holiday);
            return rowAffected;
        }
    }
}
