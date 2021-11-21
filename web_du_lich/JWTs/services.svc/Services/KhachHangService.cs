using Security;
using services.svc.Entities;
using services.svc.Managers;
using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Services
{
    public interface IKhachHangService
    {
        ExcutionResult Insert(KhachHang khachHang);
        ExcutionResult SearchKh(string keyword);
        ExcutionResult GetAll();

    }
    public class KhachHangService : IKhachHangService
    {
        private Ijwt _ijwt;
        public KhachHangService(Ijwt ijwt)
        {
            _ijwt = ijwt;
        }

        public ExcutionResult GetAll()
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = KhachHangManager.GetAll();
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
            }
            return result;
        }

        public ExcutionResult Insert(KhachHang khachHang)
        {
            ExcutionResult rowsAffected = new ExcutionResult();
            try
            {
                var now = DateTime.Now;
                khachHang.CreatedAt = now;
                rowsAffected = KhachHangManager.Insert(khachHang);
                if(rowsAffected.ErrorCode == 1 || rowsAffected.ErrorCode == 2)
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
        public ExcutionResult SearchKh(string keyword)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = KhachHangManager.SearchKh(keyword);
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
