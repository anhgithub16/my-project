using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Models;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class AppParamManager
    {
        public static IAppParamDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<IAppParamDataProvider>();
                return _provider;
            }
        }
        public static IAppParamDataProvider _provider;
        public static AppParam GetById(string id)
        {
            return provider.GetById(id);
        }
        public static IEnumerable<AppParam> GetAll()
        {
            return provider.GetAll();
        }
        public static IEnumerable<AppParam> GetAllByPaging(PagingItem pagingItem)
        {
            return provider.GetAllByPaging(pagingItem);
        }
        public static IEnumerable<AppParam> Search(string keyword)
        {
            return provider.Search(keyword);
        }
        public static ExcutionResult Insert(AppParam appParam)
        {
            ExcutionResult rowAffected = provider.Insert(appParam);
            return rowAffected;
        }
        public static ExcutionResult Update(AppParam appParam)
        {
            ExcutionResult rowAffected = provider.Update(appParam);
            return rowAffected;
        }
        public static ExcutionResult Delete(AppParam appParam)
        {
            ExcutionResult rowAffected = provider.Delete(appParam);
            return rowAffected;
        }
    }
}
