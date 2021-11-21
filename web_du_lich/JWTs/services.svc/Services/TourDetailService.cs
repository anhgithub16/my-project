using Security;
using services.svc.Managers;
using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Services
{
    public interface ITourDetailService
    {
        
        ExcutionResult GetByCityId(int cityId);
        ExcutionResult GetById(string id);
        ExcutionResult Search(string keyword);
        ExcutionResult GetAll();


    }
    public class TourDetailService : ITourDetailService
    {
        private Ijwt _ijwt;
        public TourDetailService(Ijwt ijwt)
        {
            _ijwt = ijwt;
        }

        public ExcutionResult GetAll()
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = TourDetailManager.GetAll();
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;

            }
            return result;
        }

        public ExcutionResult GetByCityId(int cityId)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = TourDetailManager.GetByCityId(cityId);
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
                var param = TourDetailManager.GetById(id);
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
                var param = TourDetailManager.Search(keyword);
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
