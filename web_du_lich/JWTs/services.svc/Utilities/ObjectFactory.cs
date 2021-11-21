using services.svc.DataAccess.DataProvider;
using services.svc.DataAccess.SqlDataProviders;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace services.svc.Utilities
{
    public class ObjectFactory
    {
        public static IUnityContainer _container;
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
            IUnityContainer container = new UnityContainer();
            container.RegisterType(typeof(IEmployeeDataProvider),typeof(Sql_EmployeesDataProvider));
            container.RegisterType(typeof(IFeedBacksDataProvider), typeof(Sql_FeedBacksDataProvider));
            container.RegisterType(typeof(IHolidayDataProvider), typeof(Sql_HolidayDataProvider));
            container.RegisterType(typeof(IAppParamDataProvider), typeof(Sql_AppParamDataProvider));
            container.RegisterType(typeof(ICompanyDataProvider), typeof(Sql_CompanyDataProvider));
            container.RegisterType(typeof(ITourDetailDataProvider), typeof(Sql_TourDetailDataProvider));
            container.RegisterType(typeof(IComboDataProvider), typeof(Sql_ComboDataProvider));
            container.RegisterType(typeof(IKhachHangDataProvider), typeof(Sql_KhachHangDataProvider));
            container.RegisterType(typeof(IBookTourDataProvider), typeof(Sql_BookTourDataProvider));
            container.RegisterType(typeof(ITripDataProvider), typeof(Sql_TripDataProvider));
            container.RegisterType(typeof(IScheduleDataProvider), typeof(Sql_ScheduleDataProvider));




            return container;            
        }
        public static T getInstance<T>()
        {
            return container.Resolve<T>();
        }
    }
}
