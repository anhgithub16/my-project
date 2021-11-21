using JWT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace JWT
{
    public class ObjectFactory
    {
        private static IUnityContainer _container;
        public static IUnityContainer container
        {
            get
            {
                if (_container == null)
                    _container = LoadContainer();
                return _container;
            }
        }
        public static IUnityContainer LoadContainer()
        {
            IUnityContainer iuc = new UnityContainer();
            iuc.RegisterType(typeof(IUserDataProvider), typeof(Sql_User_Provider));
            return iuc;
        }
        public static A getInstance<A>()
        {
            return container.Resolve<A>();
        }
    }
}
