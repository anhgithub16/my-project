using Security;
using services.svc.Managers;
using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Services
{
    public interface ITripService
    {

        ExcutionResult GetAll();
        ExcutionResult GetById(string id);
        ExcutionResult Search(string keyword);


    }
    public class TripService : ITripService
    {
        private Ijwt _ijwt;
        public TripService(Ijwt ijwt)
        {
            _ijwt = ijwt;
        }
        public ExcutionResult GetAll()
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = TripManager.GetAll();
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
            }
            return result;
        }
        public ExcutionResult GetById(string id)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = TripManager.GetById(id);
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
            }
            return result;
        }

        public ExcutionResult Search(string keyword)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = TripManager.Search(keyword);
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;

            }
            return result;
        }
    }
}
