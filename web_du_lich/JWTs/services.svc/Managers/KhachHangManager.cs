using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Models;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.svc.Managers
{
    public class KhachHangManager
    {
        public static IKhachHangDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<IKhachHangDataProvider>();
                return _provider;
            }
        }

        public static IKhachHangDataProvider _provider;

        public static ExcutionResult Insert(KhachHang khachHang)
        {
            ExcutionResult rowAffected = provider.Insert(khachHang);
            return rowAffected;
        }
        public static KhachHang SearchKh(string keyword)
        {
            return  provider.SearchKh(keyword);
           
        }
        public static IEnumerable<KhachHang> GetAll()
        {
            return provider.GetAll();
        }

    }
}
