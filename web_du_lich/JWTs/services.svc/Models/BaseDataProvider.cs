using services.svc.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Models
{
    public interface BaseDataProvider<T>
    {
        T GetById(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllByPaging(PagingItem pagingItem);
        IEnumerable<T> Search(string keyword);
        ExcutionResult Insert(T employees);
        ExcutionResult Update(T employees);
        ExcutionResult Delete(T employess);
    }
}
