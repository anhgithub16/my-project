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
    public interface IEmployeeService
    {
        ExcutionResult GetAll();
        ExcutionResult GetById(string id);
        ExcutionResult GetAllByPaging(PagingItem pagingItem);
        ExcutionResult Search(string keyword);
        ExcutionResult Insert(Employees employees,string userId);
        ExcutionResult Update(Employees employees,string userId);
        ExcutionResult Delete(string id,string userId);


    }
    public class EmployeesService : IEmployeeService
    {
        private Ijwt _ijwt;
        private readonly ILogger<EmployeesService> _ilogger;
      
        public EmployeesService(Ijwt ijwt,ILogger<EmployeesService> iLogger)
        {
            _ijwt = ijwt;
            _ilogger = iLogger;
        }
        public ExcutionResult GetAll()
        {
            ExcutionResult ex = new ExcutionResult();
            try
            {
                var p = EmployeesManager.GetAll();
                ex.Data = p;
            }
            catch(Exception e)
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
                var p = EmployeesManager.GetAllByPaging(pagingItem);
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
                var param = EmployeesManager.GetById(id);
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
                var param = EmployeesManager.Search(keyword);
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

        public ExcutionResult Insert(Employees employees,string userId)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                var param = EmployeesManager.GetById(employees.id);
                if (param == null)
                {
                    var now = DateTime.Now;
                    employees.CreatedAt = now;
                    employees.CreatedBy = userId;
                    rowAffected = EmployeesManager.Insert(employees);
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
        public ExcutionResult Update(Employees employees,string userId)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                var param = EmployeesManager.GetById(employees.id);
                if (param != null)
                {
                    var now = DateTime.Now;
                    employees.CreatedAt = now;
                    employees.UpdatedBy = userId;
                    rowAffected = EmployeesManager.Update(employees);
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
        public ExcutionResult Delete(string id,string userId)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                var param = EmployeesManager.GetById(id);
                if (param != null)
                {
                    var now = DateTime.Now;
                    param.UpdatedAt = now;
                    param.UpdatedBy = userId;
                    rowAffected = EmployeesManager.Delete(param);
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
