using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Models;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class EmployeesManager
    {
        public static IEmployeeDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<IEmployeeDataProvider>();
                return _provider;
            }
        }
        public static IEmployeeDataProvider _provider;
        public static IEnumerable<Employees> GetAll()
        {
            return provider.GetAll();
        }
        public static IEnumerable<Employees> GetAllByPaging(PagingItem pagingItem)
        {
            return provider.GetAllByPaging(pagingItem);
        }
        public static Employees GetById(string id)
        {
            return provider.GetById(id);
        }
        public static IEnumerable<Employees> Search(string keyword)
        {
            return provider.Search(keyword);
        }
        public static ExcutionResult Insert(Employees employees)
        {
            ExcutionResult rowAffected = provider.Insert(employees);
            return rowAffected;
        }
        public static ExcutionResult Update(Employees employees)
        {
            ExcutionResult rowAffected = provider.Update(employees);
            return rowAffected;
        }
        public static ExcutionResult Delete(Employees employees)
        {
            ExcutionResult rowAffected = provider.Delete(employees);
            return rowAffected;
        }
    }
}
