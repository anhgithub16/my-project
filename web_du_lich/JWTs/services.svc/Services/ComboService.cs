using Security;
using services.svc.Managers;
using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Services
{
    public interface IComboService
    {
        ExcutionResult GetByTourDetailId(string tourDetailId);

    }
    public class ComboService : IComboService
    {
        private Ijwt _ijwt;
        public ComboService(Ijwt ijwt)
        {
            _ijwt = ijwt;
        }
        public ExcutionResult GetByTourDetailId(string tourDetailId)
        {
            ExcutionResult result = new ExcutionResult();
            try
            {
                var param = ComboManager.GetByTourDetailId(tourDetailId);
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
