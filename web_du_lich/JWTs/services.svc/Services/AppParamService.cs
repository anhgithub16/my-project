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
    public interface IAppParamService
    {
        ExcutionResult GetById(string id);
        ExcutionResult GetAll();
        ExcutionResult GetAllByPaging(PagingItem pagingItem);
        ExcutionResult Search(string keyword);
        ExcutionResult Save(AppParam appParam, string userId);
        ExcutionResult Delete(string Id, string userId);
    }
    public class AppParamService:IAppParamService
    {
        private Ijwt _ijwt;
        private readonly ILogger<AppParamService> _iLogger;
        public AppParamService(Ijwt ijwt, ILogger<AppParamService> ilogger)
        {
            _ijwt = ijwt;
            _iLogger = ilogger;
        }

        public ExcutionResult GetById(string id)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = AppParamManager.GetById(id);
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

        public ExcutionResult GetAll()
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = AppParamManager.GetAll();
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
            if (pagingItem == null)
            {
                pagingItem = new PagingItem();
            }
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = AppParamManager.GetAllByPaging(pagingItem);
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

        public ExcutionResult Search(string keyword)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = AppParamManager.Search(keyword);
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

        public ExcutionResult Save(AppParam appParam, string userId)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                var param = AppParamManager.GetById(appParam.Id);
                var now = DateTime.Now;
                if (param == null)
                {
                    appParam.CreatedAt = now;
                    appParam.CreatedBy = userId;
                    rowAffected = AppParamManager.Insert(appParam);
                    
                }
                else
                {
                    appParam.UpdatedAt = now;
                    appParam.UpdatedBy = userId;
                    rowAffected = AppParamManager.Update(appParam);
                }
            }
            catch (Exception e)
            {
                rowAffected.ErrorCode = 1;
                rowAffected.Message = e.Message;
                _iLogger.LogError(e.Message, e);
            }
            return rowAffected;
        }

        public ExcutionResult Delete(string Id, string userId)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = AppParamManager.GetById(Id);
                if(param != null)
                {
                    var now = DateTime.Now;
                    param.UpdatedAt = now;
                    param.UpdatedBy = userId;
                    result = AppParamManager.Delete(param);
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
    }
}
