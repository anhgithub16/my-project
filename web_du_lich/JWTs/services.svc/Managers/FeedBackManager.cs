using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Models;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class FeedBackManager
    {
        public static IFeedBacksDataProvider provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = ObjectFactory.getInstance<IFeedBacksDataProvider>();
                }
                return _provider;

            }
        }
        public static IFeedBacksDataProvider _provider;
        public static IEnumerable<FeedBacks> GetAll()
        {
            return provider.GetAll();
        }
        public static FeedBacks GetById(string id)
        {
            return provider.GetById(id);
        }
        public static IEnumerable<FeedBacks> GetAllByPaging(PagingItem pagingItem)
        {
            return provider.GetAllByPaging(pagingItem);
        }
        public static IEnumerable<FeedBacks> Search(string keyword)
        {
            return provider.Search(keyword);
        }
        public static ExcutionResult Insert(FeedBacks feedBacks)
        {
            ExcutionResult rowAffected = provider.Insert(feedBacks);
            return rowAffected;
        }
        public static ExcutionResult Update(FeedBacks feedBacks)
        {
            ExcutionResult rowAffected = provider.Update(feedBacks);
            return rowAffected;
        }
        public static ExcutionResult Delete(FeedBacks feedBacks)
        {
            ExcutionResult rowAffected = provider.Delete(feedBacks);
            return rowAffected;
        }
    }
}
