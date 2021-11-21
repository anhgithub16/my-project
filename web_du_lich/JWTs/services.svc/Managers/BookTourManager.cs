using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Models;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class BookTourManager
    {
        public static IBookTourDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<IBookTourDataProvider>();
                return _provider;
            }
        }
        public static IBookTourDataProvider _provider;
        public static ExcutionResult Insert(BookTour bookTour)
        {
            ExcutionResult rowAffected = provider.Insert(bookTour);
            return rowAffected;
        }
        public static IEnumerable<BookTour> GetAllTrip()
        {
            return provider.GetAllTrip();
        }
        public static IEnumerable<BookTour> GetAllHotel()
        {
            return provider.GetAllHotel();
        }
        public static IEnumerable<BookTour> GetAll()
        {
            return provider.GetAll();
        }
    }
}
