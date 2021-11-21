using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Models;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class CompanyManager
    {
        public static ICompanyDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<ICompanyDataProvider>();
                return _provider;
            }
        }
        public static ICompanyDataProvider _provider;
        public static Company GetById(string id)
        {
            return provider.GetById(id);
        }
        public static IEnumerable<Company> GetAll()
        {
            return provider.GetAll();
        }
        public static IEnumerable<Company> GetAllByPaging(PagingItem pagingItem)
        {
            return provider.GetAllByPaging(pagingItem);
        }
        public static IEnumerable<Company> Search(string keyword)
        {
            return provider.Search(keyword);
        }
        public static ExcutionResult Insert(Company company)
        {
            var rowAffected = provider.Insert(company);
            return rowAffected;
        }
        public static ExcutionResult Update(Company company)
        {
            var rowAffected = provider.Update(company);
            return rowAffected;
        }
        public static ExcutionResult Delete(Company company)
        {
            var rowAffected = provider.Delete(company);
            return rowAffected;
        }
    }
}
