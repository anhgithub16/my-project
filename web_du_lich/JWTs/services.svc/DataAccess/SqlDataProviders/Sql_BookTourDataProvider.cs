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
    public class Sql_BookTourDataProvider : IBookTourDataProvider
    {
        public Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=TRAVEL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }

        #region Get Data

        public IEnumerable<BookTour> GetAllTrip()
        {
            IEnumerable<BookTour> list = null;
            Database db = this.GetDatabase();
            try
            {
                string storeName = "BookTour_GetAllTrip";
                DbCommand dbCommand = db.GetStoredProcCommand(storeName);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    list = FillCollectionTrip(dataReader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }

        public IEnumerable<BookTour> GetAllHotel()
        {
            IEnumerable<BookTour> list = null;
            Database db = this.GetDatabase();
            try
            {
                string storeName = "BookTour_GetAllHotel";
                DbCommand dbCommand = db.GetStoredProcCommand(storeName);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    list = FillCollectionHotel(dataReader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
        public IEnumerable<BookTour> GetAll()
        {
            IEnumerable<BookTour> list = null;
            Database db = this.GetDatabase();
            try
            {
                string storeName = "BookTour_GetAll";
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

        public IEnumerable<BookTour> GetAllByPaging(PagingItem pagingItem)
        {
            throw new NotImplementedException();
        }

        public BookTour GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookTour> Search(string keyword)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Modify data
        public ExcutionResult Insert(BookTour bookTour)
        {
            ExcutionResult rowaffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeName = "BookTour_Insert";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeName))
                {
                    bookTour.Id = Guid.NewGuid();
                    db.AddInParameter(dbCommand, "Id", DbType.Guid, bookTour.Id);
                    db.AddInParameter(dbCommand, "KhachHangId", DbType.Guid, bookTour.KhachHangId);
                    db.AddInParameter(dbCommand, "TourDetailId", DbType.Guid, bookTour.TourDetailId);
                    db.AddInParameter(dbCommand, "NguoiLon", DbType.Int32, bookTour.NguoiLon);
                    db.AddInParameter(dbCommand, "TreEm", DbType.Int32, bookTour.TreEm);
                    db.AddInParameter(dbCommand, "ThongDiep", DbType.String, bookTour.ThongDiep);

                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, bookTour.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, bookTour.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, bookTour.UpdatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, bookTour.UpdatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int16, bookTour.IsDeleted);
                    db.AddInParameter(dbCommand, "TripId", DbType.Guid, bookTour.TripId);
                    db.AddInParameter(dbCommand, "TongTien", DbType.Int64, bookTour.TongTien);

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

        public ExcutionResult Update(BookTour employees)
        {
            throw new NotImplementedException();
        }
        public ExcutionResult Delete(BookTour employess)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bind Data
        public static List<BookTour> FillCollection(IDataReader _dr)
        {
            List<BookTour> _list = new List<BookTour>();
            try
            {
                while (_dr.Read())
                {
                    BookTour bookTour = new BookTour();
                    bookTour.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    bookTour.KhachHangId = ConvertHelper.ToGuid(_dr["KhachHangId"], Guid.Empty);
                    bookTour.TourDetailId = ConvertHelper.ToGuid(_dr["TourDetailId"], Guid.Empty);
                    bookTour.NguoiLon = ConvertHelper.ToInt32(_dr["NguoiLon"], 0);
                    bookTour.TreEm = ConvertHelper.ToInt32(_dr["TreEm"], 0);
                    bookTour.ThongDiep = ConvertHelper.ToString(_dr["ThongDiep"], string.Empty);
                    //bookTour.TenKhachHang = ConvertHelper.ToString(_dr["FullName"], string.Empty);
                    //bookTour.TenTrip = ConvertHelper.ToString(_dr["Name"], string.Empty);
                    //bookTour.TenHotel = ConvertHelper.ToString(_dr["TenTour"], string.Empty);


                    bookTour.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    bookTour.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    bookTour.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    bookTour.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    bookTour.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    bookTour.TripId = ConvertHelper.ToGuid(_dr["TripId"], Guid.Empty);
                    bookTour.TongTien = ConvertHelper.ToInt64(_dr["TongTien"], 0);

                    if (_list.IndexOf(bookTour) < 0)
                    {
                        _list.Add(bookTour);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return _list;
        }
        public static List<BookTour> FillCollectionTrip(IDataReader _dr)
        {
            List<BookTour> _list = new List<BookTour>();
            try
            {
                while (_dr.Read())
                {
                    BookTour bookTour = new BookTour();
                    bookTour.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    bookTour.KhachHangId = ConvertHelper.ToGuid(_dr["KhachHangId"], Guid.Empty);
                    bookTour.TourDetailId = ConvertHelper.ToGuid(_dr["TourDetailId"], Guid.Empty);

                    bookTour.NguoiLon = ConvertHelper.ToInt32(_dr["NguoiLon"], 0);
                    bookTour.TreEm = ConvertHelper.ToInt32(_dr["TreEm"], 0);
                    bookTour.ThongDiep = ConvertHelper.ToString(_dr["ThongDiep"], string.Empty);
                    bookTour.TenKhachHang = ConvertHelper.ToString(_dr["FullName"], string.Empty);
                    bookTour.TenTrip = ConvertHelper.ToString(_dr["Name"], string.Empty);


                    bookTour.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    bookTour.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    bookTour.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    bookTour.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    bookTour.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    bookTour.TripId = ConvertHelper.ToGuid(_dr["TripId"], Guid.Empty);
                    bookTour.TongTien = ConvertHelper.ToInt64(_dr["TongTien"], 0);

                    if (_list.IndexOf(bookTour) < 0)
                    {
                        _list.Add(bookTour);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return _list;
        }
        public static List<BookTour> FillCollectionHotel(IDataReader _dr)
        {
            List<BookTour> _list = new List<BookTour>();
            try
            {
                while (_dr.Read())
                {
                    BookTour bookTour = new BookTour();
                    bookTour.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                

                    bookTour.NguoiLon = ConvertHelper.ToInt32(_dr["NguoiLon"], 0);
                    bookTour.TreEm = ConvertHelper.ToInt32(_dr["TreEm"], 0);
                    bookTour.ThongDiep = ConvertHelper.ToString(_dr["ThongDiep"], string.Empty);
                    bookTour.TenKhachHang = ConvertHelper.ToString(_dr["FullName"], string.Empty);
                    bookTour.TenHotel = ConvertHelper.ToString(_dr["TenTour"], string.Empty);


                  
                    bookTour.TongTien = ConvertHelper.ToInt64(_dr["TongTien"], 0);

                    if (_list.IndexOf(bookTour) < 0)
                    {
                        _list.Add(bookTour);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return _list;
        }
        public static BookTour FillObject(IDataReader _dr)
        {
            List<BookTour> _list = new List<BookTour>();
            try
            {
                while (_dr.Read())
                {
                    BookTour bookTour = new BookTour();
                    bookTour.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    bookTour.KhachHangId = ConvertHelper.ToGuid(_dr["KhachHangId"], Guid.Empty);
                    bookTour.TourDetailId = ConvertHelper.ToGuid(_dr["TourDetailId"], Guid.Empty);
                    bookTour.NguoiLon = ConvertHelper.ToInt32(_dr["NguoiLon"], 0);
                    bookTour.TreEm = ConvertHelper.ToInt32(_dr["TreEm"], 0);
                    bookTour.ThongDiep = ConvertHelper.ToString(_dr["ThongDiep"], string.Empty);
                    //bookTour.TenKhachHang = ConvertHelper.ToString(_dr["FullName"], string.Empty);
                    //bookTour.TenTrip = ConvertHelper.ToString(_dr["Name"], string.Empty);
                    //bookTour.TenHotel = ConvertHelper.ToString(_dr["TenTour"], string.Empty);


                    bookTour.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    bookTour.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    bookTour.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    bookTour.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    bookTour.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    bookTour.TripId = ConvertHelper.ToGuid(_dr["TripId"], Guid.Empty);
                    bookTour.TongTien = ConvertHelper.ToInt64(_dr["TongTien"], 0);

                    if (_list.IndexOf(bookTour) < 0)
                    {
                        _list.Add(bookTour);
                    }
                    if (_list.Count > 0)
                    {
                        BookTour[] bookTours = _list.ToArray();
                      

                        if (bookTours != null && bookTours.Length > 0)
                        {
                            return bookTours[0];
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
