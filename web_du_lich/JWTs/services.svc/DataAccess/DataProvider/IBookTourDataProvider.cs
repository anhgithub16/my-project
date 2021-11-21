using services.svc.Entities;
using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.DataAccess.DataProvider
{
    public interface IBookTourDataProvider:BaseDataProvider<BookTour>
    {
        IEnumerable<BookTour> GetAllTrip();
        IEnumerable<BookTour> GetAllHotel();


    }
}
