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
    public class Sql_HolidayDataProvider : IHolidayDataProvider
    {

        public  Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=QL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }
        #region get data
        public IEnumerable<Holiday> GetAll()
        {
            List<Holiday> list = new List<Holiday>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Holiday_GetAll";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
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

        public IEnumerable<Holiday> GetAllByPaging(PagingItem pagingItem)
        {
            if(pagingItem == null)
            {
                pagingItem = new PagingItem();
            }
            List<Holiday> list = new List<Holiday>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Holiday_Get_All_By_Paging";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "orderBy", DbType.String, pagingItem.OrderBy);
                db.AddInParameter(dbCommand, "directionSort", DbType.String, pagingItem.DirectionSort);
                db.AddInParameter(dbCommand, "pageIndex", DbType.Int32, pagingItem.PageIndex);
                db.AddInParameter(dbCommand, "pageSize", DbType.Int32, pagingItem.PageSize);
                db.AddOutParameter(dbCommand, "totalRows",DbType.Int32,0x20);
                using(IDataReader dataReader = db.ExecuteReader(dbCommand))
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

        public Holiday GetById(string id)
        {
            Holiday holiday = new Holiday();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Holiday_Get_By_Id";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "Id", DbType.String, id);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    holiday = FillObject(dataReader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return holiday;
        }

        public IEnumerable<Holiday> Search(string keyword)
        {
            List<Holiday> list = new List<Holiday>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Holiday_Search";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "Name", DbType.String, keyword);
                using(IDataReader dataReader = db.ExecuteReader(dbCommand))
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

        #region modify data
        public ExcutionResult Insert(Holiday holiday)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Holiday_Insert";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, holiday.Id);
                    db.AddInParameter(dbCommand, "Code", DbType.String, holiday.Code);
                    db.AddInParameter(dbCommand, "Name", DbType.String, holiday.Name);
                    db.AddInParameter(dbCommand, "DayOff", DbType.DateTime, holiday.DayOff);
                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, holiday.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, holiday.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, holiday.UpdatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, holiday.UpdatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int32, holiday.IsDeleted);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand,"IsCheck"),0);
                    if(isCheck == 1)
                    {
                        rowAffected.Message = "Insert no success";
                    }
                }

            }
            catch (Exception e)
            {
                rowAffected.ErrorCode = 1;
                rowAffected.Message = e.Message;
            }
            return rowAffected;
        }
        public ExcutionResult Update(Holiday holiday)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Holiday_Update";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, holiday.Id);
                    db.AddInParameter(dbCommand, "Code", DbType.String, holiday.Code);
                    db.AddInParameter(dbCommand, "Name", DbType.String, holiday.Name);
                    db.AddInParameter(dbCommand, "DayOff", DbType.DateTime, holiday.DayOff);
                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, holiday.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, holiday.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, holiday.UpdatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, holiday.UpdatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int32, holiday.IsDeleted);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
                    if (isCheck == 1)
                    {
                        rowAffected.Message = "Update no success";
                    }
                }

            }
            catch (Exception e)
            {
                rowAffected.ErrorCode = 1;
                rowAffected.Message = e.Message;
            }
            return rowAffected;
        }
        public ExcutionResult Delete(Holiday holiday)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Holiday_Delete";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, holiday.Id);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand,"IsCheck"),0);
                    if(isCheck == 1)
                    {
                        rowAffected.Message = "Delete no success";
                    }
                }

            }
            catch (Exception e)
            {
                rowAffected.ErrorCode = 1;
                rowAffected.Message = e.Message;
            }   
            return rowAffected;
        }
        #endregion

        #region bind data
        public static List<Holiday> FillCollection(IDataReader _dr)
        {
            List<Holiday> list = new List<Holiday>();
            try
            {
                while (_dr.Read())
                {
                    Holiday holiday = new Holiday();
                    holiday.Id = _dr["Id"].ToString();
                    holiday.Code = _dr["Code"].ToString();
                    holiday.Name = _dr["Name"].ToString();
                    holiday.DayOff = ConvertHelper.ToDateTime(_dr["DayOff"], DateTime.Now);
                    holiday.CreatedBy = _dr["CreatedBy"].ToString();
                    holiday.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    holiday.UpdatedBy = _dr["UpdatedBy"].ToString();
                    holiday.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"],DateTime.Now);
                    holiday.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if(list.IndexOf(holiday) < 0)
                    {
                        list.Add(holiday);
                    }
                }
            }
            catch (Exception)
            {
                _dr.Close();
            }
            return list;
        }
        public static Holiday FillObject(IDataReader _dr)
        {
            List<Holiday> list = new List<Holiday>();
            try
            {
                while (_dr.Read())
                {
                    Holiday holiday = new Holiday();
                    holiday.Id = _dr["Id"].ToString();
                    holiday.Code = _dr["Code"].ToString();
                    holiday.Name = _dr["Name"].ToString();
                    holiday.DayOff = ConvertHelper.ToDateTime(_dr["DayOff"], DateTime.Now);
                    holiday.CreatedBy = _dr["CreatedBy"].ToString();
                    holiday.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    holiday.UpdatedBy = _dr["UpdatedBy"].ToString();
                    holiday.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    holiday.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if(list.IndexOf(holiday) < 0)
                    {
                        list.Add(holiday);
                    }
                    if (list.Count > 0)
                    {
                        Holiday[] holidays = list.ToArray();
                        if (holidays != null && holidays.Length > 0)
                            return holidays[0];
                    }
                }
            }
            catch (Exception)
            {
                _dr.Close();
            }
            return null;
        }
        #endregion
    }
}
