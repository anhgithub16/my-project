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
    public class Sql_TripDataProvider : ITripDataProvider
    {
        public Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=TRAVEL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }

        #region Get Data
        public IEnumerable<Trip> GetAll()
        {
            IEnumerable<Trip> list = null;
            Database db = this.GetDatabase();
            try
            {
                string storeName = "Trip_GetAll";
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

        public IEnumerable<Trip> GetAllByPaging(PagingItem pagingItem)
        {
            throw new NotImplementedException();
        }

        public Trip GetById(string id)
        {
            Trip trip = new Trip();
            try
            {
                Database db = this.GetDatabase();
                string storeName = "Trip_GetById";
                DbCommand dbCommand = db.GetStoredProcCommand(storeName);
                db.AddInParameter(dbCommand, "Id", DbType.Guid, Guid.Parse(id));
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    trip = FillObject(dataReader);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return trip;
        }


        public IEnumerable<Trip> Search(string keyword)
        {
            List<Trip> list = new List<Trip>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Trip_Search";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "Name", DbType.String, keyword);
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


        #region Modify data
        public ExcutionResult Insert(Trip employees)
        {
            throw new NotImplementedException();
        }
        public ExcutionResult Update(Trip employees)
        {
            throw new NotImplementedException();
        }
        public ExcutionResult Delete(Trip employess)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bind Data
        public static List<Trip> FillCollection(IDataReader _dr)
        {
            List<Trip> _list = new List<Trip>();
            try
            {
                while (_dr.Read())
                {
                    Trip trip = new Trip();
                    trip.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    trip.Name = ConvertHelper.ToString(_dr["Name"], string.Empty);
                    trip.Title = ConvertHelper.ToString(_dr["Title"], string.Empty);
                    trip.NoiDung = ConvertHelper.ToString(_dr["NoiDung"], string.Empty);
                    trip.GiaTrip = ConvertHelper.ToInt64(_dr["GiaTrip"], 0);
                    trip.DiemKhoiHanh = ConvertHelper.ToString(_dr["DiemKhoiHanh"], string.Empty);
                    trip.TimeKhoiHanh = ConvertHelper.ToDateTime(_dr["TimeKhoiHanh"], DateTime.Now);
                    trip.SoNgay = ConvertHelper.ToInt32(_dr["SoNgay"], 0);
                    trip.SoDem = ConvertHelper.ToInt32(_dr["SoDem"], 0);
                    trip.HinhAnh = ConvertHelper.ToString(_dr["HinhAnh"], string.Empty);
                    trip.Sale = ConvertHelper.ToInt32(_dr["Sale"], 0);

                    trip.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    trip.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    trip.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    trip.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    trip.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if (_list.IndexOf(trip) < 0)
                    {
                        _list.Add(trip);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return _list;
        }
        public static Trip FillObject(IDataReader _dr)
        {
            List<Trip> _list = new List<Trip>();
            try
            {
                while (_dr.Read())
                {
                    Trip trip = new Trip();
                    trip.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    trip.Name = ConvertHelper.ToString(_dr["Name"], string.Empty);
                    trip.Title = ConvertHelper.ToString(_dr["Title"], string.Empty);
                    trip.NoiDung = ConvertHelper.ToString(_dr["NoiDung"], string.Empty);
                    trip.GiaTrip = ConvertHelper.ToInt64(_dr["GiaTrip"], 0);
                    trip.DiemKhoiHanh = ConvertHelper.ToString(_dr["DiemKhoiHanh"], string.Empty);
                    trip.TimeKhoiHanh = ConvertHelper.ToDateTime(_dr["TimeKhoiHanh"], DateTime.Now);
                    trip.SoNgay = ConvertHelper.ToInt32(_dr["SoNgay"], 0);
                    trip.SoDem = ConvertHelper.ToInt32(_dr["SoDem"], 0);
                    trip.HinhAnh = ConvertHelper.ToString(_dr["HinhAnh"], string.Empty);
                    trip.Sale = ConvertHelper.ToInt32(_dr["Sale"], 0);

                    trip.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    trip.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    trip.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    trip.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    trip.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if (_list.IndexOf(trip) < 0)
                    {
                        _list.Add(trip);
                    }
                    if (_list.Count > 0)
                    {
                        Trip[] trips = _list.ToArray();
                        if (trips != null && trips.Length > 0)
                        {
                            return trips[0];
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
