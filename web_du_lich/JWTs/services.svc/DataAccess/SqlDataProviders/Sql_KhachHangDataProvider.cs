using JWT.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using services.svc.DataAccess.DataProvider;
using services.svc.Entities;
using services.svc.Models;
using services.svc.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace services.svc.DataAccess.SqlDataProviders
{
    public class Sql_KhachHangDataProvider : IKhachHangDataProvider
    {
        public Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=TRAVEL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }

        #region Get Data
        public IEnumerable<KhachHang> GetAll()
        {
            List<KhachHang> list = new List<KhachHang>();
            try
            {
                Database db = this.GetDatabase();
                string storeName = "KhachHang_GetAll";
                DbCommand dbCommand = db.GetStoredProcCommand(storeName);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    list = FillCollection(dataReader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }

        public IEnumerable<KhachHang> GetAllByPaging(PagingItem pagingItem)
        {
            throw new NotImplementedException();
        }

        public KhachHang GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KhachHang> Search(string keyword)
        {
           
            throw new NotImplementedException();

        }
        public KhachHang SearchKh(string keyword)
        {
            KhachHang list = new KhachHang();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "KhachHang_Search";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "FullName", DbType.String, keyword);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    list = FillObject(dataReader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
        #endregion

        #region Modify Data
        public ExcutionResult Insert(KhachHang khachHang)
        {
            ExcutionResult rowaffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeName = "KhachHang_Insert";
                using(DbCommand dbCommand = db.GetStoredProcCommand(storeName))
                {
                    khachHang.Id = Guid.NewGuid();
                    db.AddInParameter(dbCommand,"Id",DbType.Guid,khachHang.Id);
                    db.AddInParameter(dbCommand,"FullName",DbType.String,khachHang.FullName);
                    db.AddInParameter(dbCommand,"Address",DbType.String,khachHang.Address);
                    db.AddInParameter(dbCommand,"Email",DbType.String,khachHang.Email);
                    db.AddInParameter(dbCommand,"Phone",DbType.String,khachHang.Phone);

                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, khachHang.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, khachHang.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, khachHang.UpdatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, khachHang.UpdatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int16, khachHang.IsDeleted);

                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelpers.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
                    if (isCheck == 1)
                    {
                        rowaffected.ErrorCode = 2;
                        rowaffected.Message = "Insert no success";
                    }
                }
            }
            catch (Exception e)
            {
                rowaffected.ErrorCode = 1;
                rowaffected.Message = e.Message;
            }
            return rowaffected;
        }

     

        public ExcutionResult Update(KhachHang employees)
        {
            throw new NotImplementedException();
        }
        public ExcutionResult Delete(KhachHang employess)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bind Data
        public static List<KhachHang> FillCollection(IDataReader _dr)
        {
            List<KhachHang> _list = new List<KhachHang>();
            try
            {
                while (_dr.Read())
                {
                    KhachHang khachHang = new KhachHang();
                    khachHang.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    khachHang.FullName = ConvertHelper.ToString(_dr["FullName"],string.Empty);
                    khachHang.Address = ConvertHelper.ToString(_dr["Address"], string.Empty);
                    khachHang.Email = ConvertHelper.ToString(_dr["Email"], string.Empty);
                    khachHang.Phone = ConvertHelper.ToString(_dr["Phone"], string.Empty);

                    khachHang.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    khachHang.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    khachHang.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    khachHang.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    khachHang.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if (_list.IndexOf(khachHang) < 0)
                    {
                        _list.Add(khachHang);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return _list;
        }
        public static KhachHang FillObject(IDataReader _dr)
        {
            List<KhachHang> _list = new List<KhachHang>();
            try
            {
                while (_dr.Read())
                {
                    KhachHang khachHang = new KhachHang();
                    khachHang.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    khachHang.FullName = ConvertHelper.ToString(_dr["FullName"], string.Empty);
                    khachHang.Address = ConvertHelper.ToString(_dr["Address"], string.Empty);
                    khachHang.Email = ConvertHelper.ToString(_dr["Email"], string.Empty);
                    khachHang.Phone = ConvertHelper.ToString(_dr["Phone"], string.Empty);

                    khachHang.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    khachHang.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    khachHang.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    khachHang.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    khachHang.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if (_list.IndexOf(khachHang) < 0)
                    {
                        _list.Add(khachHang);
                    }
                    if (_list.Count > 0)
                    {
                        KhachHang[] khachHangs = _list.ToArray();
                        if (khachHangs != null && khachHangs.Length > 0)
                        {
                            return khachHangs[0];
                        }
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return null;
        }

        
        #endregion
    }
}
