using Microsoft.Extensions.Logging;
using Security;
using services.svc.Entities;
using services.svc.Managers;
using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Services
{
    public interface IHolidayService
    {
        ExcutionResult GetById(string id);
        ExcutionResult GetAll();
        ExcutionResult GetAllByPaging(PagingItem pagingItem);
        ExcutionResult Search(string keyword);
        ExcutionResult Save(Holiday holiday, string userId);
        ExcutionResult Delete(string id, string userId);
    }
    public class HolidayService : IHolidayService
    {
        private Ijwt _ijwt;
        private readonly ILogger<HolidayService> _iLogger;
        public HolidayService(Ijwt ijwt,ILogger<HolidayService> ilogger)
        {
            _ijwt = ijwt;
            _iLogger = ilogger;
        }
        public ExcutionResult Delete(string id, string userId)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                var param = HolidayManager.GetById(id);
                if (param != null)
                {
                    var now = DateTime.Now;
                    param.UpdatedAt = now;
                    param.UpdatedBy = userId;
                    rowAffected = HolidayManager.Delete(param);
                }
            }
            catch(Exception e)
            {
                rowAffected.ErrorCode = 1;
                rowAffected.Message = e.Message;
                _iLogger.LogError(e.Message, e);
            }
            return rowAffected;
        }

        public ExcutionResult GetAll()
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = HolidayManager.GetAll();
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
                _iLogger.LogError(e.Message, e);
            }
            return result;
        }

        public ExcutionResult GetAllByPaging(PagingItem pagingItem)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = HolidayManager.GetAllByPaging(pagingItem);
                result.Data = param;
                result.MetaData = pagingItem;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
                _iLogger.LogError(e.Message, e);
            }
            return result;
        }

        public ExcutionResult GetById(string id)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = HolidayManager.GetById(id);
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
                _iLogger.LogError(e.Message, e);
            }
            return result;
        }

        public ExcutionResult Save(Holiday holiday, string userId)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = HolidayManager.GetById(holiday.Id);
                var now = DateTime.Now;
                if (param == null)
                {
                    holiday.CreatedAt = now;
                    holiday.CreatedBy = userId;
                    result = HolidayManager.Insert(holiday);
                }
                else
                {
                    holiday.UpdatedAt = now;
                    holiday.UpdatedBy = userId;
                    result = HolidayManager.Update(holiday);
                }
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
                _iLogger.LogError(e.Message, e);
            }
            return result;
        }

        public ExcutionResult Search(string keyword)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = HolidayManager.Search(keyword);
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
                _iLogger.LogError(e.Message, e);
            }
            return result;
        }

      
    }
}
