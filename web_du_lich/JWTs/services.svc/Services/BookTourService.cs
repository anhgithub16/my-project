using Security;
using services.svc.Entities;
using services.svc.Managers;
using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Services
{
    public interface IBookTourService
    {
        ExcutionResult Insert(BookTour bookTour);
        ExcutionResult GetAllTrip();
        ExcutionResult GetAllHotel();
        ExcutionResult GetAll();


    }
    public class BookTourService : IBookTourService
    {
        private Ijwt _ijwt;
        public BookTourService(Ijwt ijwt)
        {
            _ijwt = ijwt;
        }

        public ExcutionResult GetAll()
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = BookTourManager.GetAll();
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
            }
            return result;
        }

        public ExcutionResult GetAllHotel()
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = BookTourManager.GetAllHotel();
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
            }
            return result;
        }

        public ExcutionResult GetAllTrip()
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = BookTourManager.GetAllTrip();
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
            }
            return result;
        }

        public ExcutionResult Insert(BookTour bookTour)
        {
            ExcutionResult rowsAffected = new ExcutionResult();
            try
            {
                var now = DateTime.Now;
                bookTour.CreatedAt = now;
                rowsAffected = BookTourManager.Insert(bookTour);
                if (rowsAffected.ErrorCode == 1 || rowsAffected.ErrorCode == 2)
                {
                    rowsAffected.Message = "Insert failed";
                }
            }
            catch (Exception e)
            {
                rowsAffected.ErrorCode = 1;
                rowsAffected.Message = e.Message;
            }
            return rowsAffected;
        }
    }
}
