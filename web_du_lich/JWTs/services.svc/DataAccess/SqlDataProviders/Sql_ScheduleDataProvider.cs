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
    public class Sql_ScheduleDataProvider : IScheduleDataProvider
    {
        public Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=TRAVEL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }

        #region Get Data
        public IEnumerable<Schedule> GetAll()
        {
            IEnumerable<Schedule> list = null;
            Database db = this.GetDatabase();
            try
            {
                string storeName = "Schedule_GetAll";
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
        public IEnumerable<Schedule> GetByDay(string tripId,int day)
        {
            IEnumerable<Schedule> schedule = null;
            try
            {
                Database db = this.GetDatabase();
                string storeName = "Schedule_GetByDay";
                DbCommand dbCommand = db.GetStoredProcCommand(storeName);
                db.AddInParameter(dbCommand, "TripId", DbType.Guid, Guid.Parse(tripId));
                db.AddInParameter(dbCommand, "Day", DbType.Int32, day);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    schedule = FillCollection(dataReader);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return schedule;
        }

        public IEnumerable<Schedule> GetAllByPaging(PagingItem pagingItem)
        {
            throw new NotImplementedException();
        }

        public Schedule GetById(string id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Schedule> Search(string keyword)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Modify Data
        public ExcutionResult Insert(Schedule employees)
        {
            throw new NotImplementedException();
        }
        public ExcutionResult Update(Schedule employees)
        {
            throw new NotImplementedException();
        }
        public ExcutionResult Delete(Schedule employess)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region BindData
        public static List<Schedule> FillCollection(IDataReader _dr)
        {
            List<Schedule> _list = new List<Schedule>();
            try
            {
                while (_dr.Read())
                {
                    Schedule schedule = new Schedule();
                    schedule.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    schedule.TripId = ConvertHelper.ToGuid(_dr["TripId"], Guid.Empty);
                    schedule.Day = ConvertHelper.ToInt32(_dr["Day"], 0);
                    schedule.Hour = ConvertHelper.ToString(_dr["Hour"], string.Empty);
                    schedule.NoiDung = ConvertHelper.ToString(_dr["NoiDung"], string.Empty);
                    schedule.MoTaDay = ConvertHelper.ToString(_dr["MoTaDay"], string.Empty);
                    schedule.Stt = ConvertHelper.ToInt32(_dr["Stt"], 0);

                    schedule.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    schedule.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    schedule.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    schedule.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    schedule.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);

                    if (_list.IndexOf(schedule) < 0)
                    {
                        _list.Add(schedule);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return _list;
        }
        public static Schedule FillObject(IDataReader _dr)
        {
            List<Schedule> _list = new List<Schedule>();
            try
            {
                while (_dr.Read())
                {
                    Schedule schedule = new Schedule();
                    schedule.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    schedule.TripId = ConvertHelper.ToGuid(_dr["TripId"], Guid.Empty);
                    schedule.Day = ConvertHelper.ToInt32(_dr["Day"], 0);
                    schedule.Hour = ConvertHelper.ToString(_dr["Hour"], string.Empty);
                    schedule.NoiDung = ConvertHelper.ToString(_dr["NoiDung"], string.Empty);
                    schedule.MoTaDay = ConvertHelper.ToString(_dr["MoTaDay"], string.Empty);
                    schedule.Stt = ConvertHelper.ToInt32(_dr["Stt"], 0);

                    schedule.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    schedule.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    schedule.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    schedule.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    schedule.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);

                    if (_list.IndexOf(schedule) < 0)
                    {
                        _list.Add(schedule);
                    }
                    if (_list.Count > 0)
                    {
                        Schedule[] schedules = _list.ToArray();
                        if (schedules != null && schedules.Length > 0)
                        {
                            return schedules[0];
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
