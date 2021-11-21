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
    public interface IFeedBackService
    {
        ExcutionResult GetAll();
        ExcutionResult GetById(string id);
        ExcutionResult GetAllByPaging(PagingItem pagingItem);
        ExcutionResult Search(string keyword);
        ExcutionResult Insert(FeedBacks feedBacks, string userId);
        ExcutionResult Update(FeedBacks feedBacks, string userId);
        ExcutionResult Delete(string id, string userId);
    }
    public class FeedBackService:IFeedBackService
    {
        private Ijwt _ijwt;
        private readonly ILogger<FeedBackService> _ilogger;

        public FeedBackService(Ijwt ijwt, ILogger<FeedBackService> iLogger)
        {
            _ijwt = ijwt;
            _ilogger = iLogger;
        }
        public ExcutionResult GetAll()
        {
            ExcutionResult ex = new ExcutionResult();
            try
            {
                var p = FeedBackManager.GetAll();
                ex.Data = p;
            }
            catch (Exception e)
            {
                ex.ErrorCode = 1;
                ex.Message = e.Message;
            }
            return ex;
        }
        public ExcutionResult GetAllByPaging(PagingItem pagingItem)
        {
            ExcutionResult ex = new ExcutionResult();
            try
            {
                var p = FeedBackManager.GetAllByPaging(pagingItem);
                ex.Data = p;
                ex.MetaData = pagingItem;
            }
            catch (Exception e)
            {
                ex.ErrorCode = 1;
                ex.Message = e.Message;
            }
            return ex;
        }

        public ExcutionResult GetById(string id)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = FeedBackManager.GetById(id);
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
                _ilogger.LogError(e.Message, e);
            }
            return result;
        }

        public ExcutionResult Search(string keyword)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = FeedBackManager.Search(keyword);
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
                _ilogger.LogError(e.Message, e);
            }
            return result;
        }

        public ExcutionResult Insert(FeedBacks feedBacks, string userId)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                var param = FeedBackManager.GetById(feedBacks.Id);
                if (param == null)
                {
                    var now = DateTime.Now;
                    feedBacks.CreatedAt = now;
                    feedBacks.CreatedBy = userId;
                    rowAffected = FeedBackManager.Insert(feedBacks);
                }
                else
                {
                    rowAffected.Message = "Id already exist";
                }
            }
            catch (Exception e)
            {
                rowAffected.ErrorCode = 1;
                rowAffected.Message = e.Message;
                _ilogger.LogError(e.Message, e);
            }
            return rowAffected;
        }
        public ExcutionResult Update(FeedBacks feedBacks, string userId)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                var param = FeedBackManager.GetById(feedBacks.Id);
                if (param != null)
                {
                    var now = DateTime.Now;
                    feedBacks.CreatedAt = now;
                    feedBacks.UpdatedBy = userId;
                    rowAffected = FeedBackManager.Update(feedBacks);
                }
                else
                {
                    rowAffected.Message = "Id khong ton tai";
                }
            }
            catch (Exception e)
            {
                rowAffected.ErrorCode = 1;
                rowAffected.Message = e.Message;
                _ilogger.LogError(e.Message, e);
            }
            return rowAffected;
        }
        public ExcutionResult Delete(string id, string userId)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                var param = FeedBackManager.GetById(id);
                if (param != null)
                {
                    var now = DateTime.Now;
                    param.UpdatedAt = now;
                    param.UpdatedBy = userId;
                    rowAffected = FeedBackManager.Delete(param);
                }
                else
                {
                    rowAffected.Message = "Id khong ton tai";
                }
            }
            catch (Exception e)
            {
                rowAffected.ErrorCode = 1;
                rowAffected.Message = e.Message;
                _ilogger.LogError(e.Message, e);
            }
            return rowAffected;
        }
    }
}
