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
    public class Sql_TourDetailDataProvider : ITourDetailDataProvider
    {
        public Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=TRAVEL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }

        #region Get Data
        public IEnumerable<TourDetail> GetAll()
        {
            IEnumerable<TourDetail> list = null;
            try
            {
                Database db = this.GetDatabase();
                string storeName = "TourDetail_GetAll";
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

        public IEnumerable<TourDetail> GetAllByPaging(PagingItem pagingItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TourDetail> GetByCityId(int CityId)
        {
            IEnumerable<TourDetail> list = null;
            try
            {
                Database db = this.GetDatabase();
                string storeName = "TourDetail_GetByCityId";
                DbCommand dbCommand = db.GetStoredProcCommand(storeName);
                db.AddInParameter(dbCommand, "CityId", DbType.Int32, CityId);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    list = FillCollection(dataReader);
                }
            }
            catch(Exception)
            {
                throw;
            }
            return list;
        }

        public TourDetail GetById(string id)
        {
            TourDetail tourDetail = new TourDetail();
            try
            {
                Database db = this.GetDatabase();
                string storeName = "TourDetail_GetById";
                DbCommand dbCommand = db.GetStoredProcCommand(storeName);
                db.AddInParameter(dbCommand,"Id",DbType.Guid,Guid.Parse(id));
                using(IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    tourDetail = FillObject(dataReader);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tourDetail;
        }


        public IEnumerable<TourDetail> Search(string keyword)
        {
            List<TourDetail> list = new List<TourDetail>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "TourDetail_Search";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "TenTour", DbType.String, keyword);
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
        #endregion

        #region Modify Data
        public ExcutionResult Insert(TourDetail employees)
        {
            throw new NotImplementedException();
        }
        public ExcutionResult Update(TourDetail employees)
        {
            throw new NotImplementedException();
        }
        public ExcutionResult Delete(TourDetail employess)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bind Data
        public static List<TourDetail> FillCollection(IDataReader _dr)
        {
            List<TourDetail> _list = new List<TourDetail>();
            try
            {
                while (_dr.Read())
                {
                    TourDetail tourDetail = new TourDetail();
                    tourDetail.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    tourDetail.TypeTourId = ConvertHelper.ToInt32(_dr["TypeTourId"],0);
                    tourDetail.TenTour = ConvertHelper.ToString(_dr["TenTour"], string.Empty);
                    tourDetail.GioiThieu = ConvertHelper.ToString(_dr["GioiThieu"], string.Empty);
                    tourDetail.NoiDung = ConvertHelper.ToString(_dr["NoiDung"], string.Empty);
                    tourDetail.GiaTour = ConvertHelper.ToInt64(_dr["GiaTour"], 0);
                    tourDetail.DiemKhoiHanh = ConvertHelper.ToString(_dr["DiemKhoiHanh"], string.Empty);
                    tourDetail.TimeKhoiHanh = ConvertHelper.ToDateTime(_dr["TimeKhoiHanh"], DateTime.Now);
                    tourDetail.SoNgay = ConvertHelper.ToInt32(_dr["SoNgay"], 0);
                    tourDetail.SoDem = ConvertHelper.ToInt32(_dr["SoDem"], 0);
                    tourDetail.HinhAnh = ConvertHelper.ToString(_dr["HinhAnh"], string.Empty);
                    tourDetail.Sale = ConvertHelper.ToInt32(_dr["Sale"], 0);
                    tourDetail.CityId = ConvertHelper.ToInt32(_dr["CityId"], 0);

                    tourDetail.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    tourDetail.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    tourDetail.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    tourDetail.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    tourDetail.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if(_list.IndexOf(tourDetail) < 0)
                    {
                        _list.Add(tourDetail);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return _list;
        }
        public static TourDetail FillObject(IDataReader _dr)
        {
            List<TourDetail> _list = new List<TourDetail>();
            try
            {
                while (_dr.Read())
                {
                    TourDetail tourDetail = new TourDetail();
                    tourDetail.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    tourDetail.TypeTourId = ConvertHelper.ToInt32(_dr["TypeTourId"], 0);
                    tourDetail.TenTour = ConvertHelper.ToString(_dr["TenTour"], string.Empty);
                    tourDetail.GioiThieu = ConvertHelper.ToString(_dr["GioiThieu"], string.Empty);
                    tourDetail.NoiDung = ConvertHelper.ToString(_dr["NoiDung"], string.Empty);
                    tourDetail.GiaTour = ConvertHelper.ToInt64(_dr["GiaTour"], 0);
                    tourDetail.DiemKhoiHanh = ConvertHelper.ToString(_dr["DiemKhoiHanh"], string.Empty);
                    tourDetail.TimeKhoiHanh = ConvertHelper.ToDateTime(_dr["TimeKhoiHanh"], DateTime.Now);
                    tourDetail.SoNgay = ConvertHelper.ToInt32(_dr["SoNgay"], 0);
                    tourDetail.SoDem = ConvertHelper.ToInt32(_dr["SoDem"], 0);
                    tourDetail.HinhAnh = ConvertHelper.ToString(_dr["HinhAnh"], string.Empty);
                    tourDetail.Sale = ConvertHelper.ToInt32(_dr["Sale"], 0);

                    tourDetail.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    tourDetail.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    tourDetail.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    tourDetail.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    tourDetail.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if (_list.IndexOf(tourDetail) < 0)
                    {
                        _list.Add(tourDetail);
                    }
                    if(_list.Count > 0)
                    {
                        TourDetail[] tourDetails = _list.ToArray();
                        if(tourDetails != null && tourDetails.Length > 0)
                        {
                            return tourDetails[0];
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
