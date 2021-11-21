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
    public interface ICompanyService
    {
        ExcutionResult GetAll();
        ExcutionResult GetById(string id);
        ExcutionResult GetAllByPaging(PagingItem pagingItem);
        ExcutionResult Search(string keyword);
        ExcutionResult Save(Company company,string userId);
        ExcutionResult Delete(string id,string useId);

    }
    public class CompanyService : ICompanyService
    {
        private Ijwt _ijwt;
        private readonly ILogger<CompanyService> _iLogger;
        public CompanyService(Ijwt ijwt,ILogger<CompanyService> iLogger)
        {
            _ijwt = ijwt;
            _iLogger = iLogger;
        }
        public ExcutionResult Delete(string id, string useId)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                var param = CompanyManager.GetById(id);
                if(param != null)
                {
                    var now = DateTime.Now;
                    param.UpdatedAt = now;
                    param.UpdatedBy = useId;
                    rowAffected = CompanyManager.Delete(param);
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

        public ExcutionResult GetAll()
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = CompanyManager.GetAll();
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
            if(pagingItem == null)
            {
                pagingItem = new PagingItem();
            }
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = CompanyManager.GetAllByPaging(pagingItem);
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
                var param = CompanyManager.GetById(id);
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

        public ExcutionResult Save(Company company, string userId)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                var param = CompanyManager.GetById(company.Id);
                var now = DateTime.Now;
                if(param == null)
                {
                    company.CreatedAt = now;
                    company.CreatedBy = userId;
                    rowAffected = CompanyManager.Insert(company);
                }
                else
                {
                    param.UpdatedAt = now;
                    param.UpdatedBy = userId;
                    rowAffected = CompanyManager.Update(company);
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

        public ExcutionResult Search(string keyword)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = CompanyManager.Search(keyword);
                result.Data = param;
            }
            catch(Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
                _iLogger.LogError(e.Message, e);
            }
            return result;
        }
    }
}
