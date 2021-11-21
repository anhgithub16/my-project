using Security;
using services.svc.Managers;
using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Services
{
    public interface IScheduleService
    {
        ExcutionResult GetAll();
        ExcutionResult GetByDay(string tripId,int day);

    }
    public class ScheduleService : IScheduleService
    {
        private Ijwt _ijwt;
        public ScheduleService(Ijwt ijwt)
        {
            _ijwt = ijwt;
        }
        public ExcutionResult GetAll()
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = ScheduleManager.GetAll();
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
            }
            return result;
        }

        public ExcutionResult GetByDay(string tripId,int day)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = ScheduleManager.GetByDay(tripId,day);
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
