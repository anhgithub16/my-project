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
    public class Sql_FeedBacksDataProvider : IFeedBacksDataProvider
    {
        public Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=QL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }
        #region get data
        public IEnumerable<FeedBacks> GetAll()
        {
            List<FeedBacks> list = null;
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "FeedBack_GetAll";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
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

        public IEnumerable<FeedBacks> GetAllByPaging(PagingItem pagingItem)
        {
            if(pagingItem == null)
            {
                pagingItem = new PagingItem();
            }
            List<FeedBacks> list = new List<FeedBacks>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "FeedBacks_Get_All_By_Paging";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "orderBy", DbType.String, pagingItem.OrderBy);
                db.AddInParameter(dbCommand, "directionSort", DbType.String, pagingItem.DirectionSort);
                db.AddInParameter(dbCommand, "pageSize", DbType.Int32, pagingItem.PageSize);
                db.AddInParameter(dbCommand, "pageIndex", DbType.Int32, pagingItem.PageIndex);
                db.AddOutParameter(dbCommand, "totalRows", DbType.Int32, 0x20);
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

        public FeedBacks GetById(string id)
        {
            FeedBacks feedBacks = null;
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "FeedBack_Get_By_Id";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "id", DbType.String, id);
                using(IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    feedBacks = FillObject(dataReader);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return feedBacks;
        }
        public IEnumerable<FeedBacks> Search(string keyword)
        {
            List<FeedBacks> list = new List<FeedBacks>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "FeedBack_Search";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "Id", DbType.String, keyword);
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
        public ExcutionResult Delete(FeedBacks employess)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "FeedBack_Delete";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "id", DbType.String, employess.Id);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
                    if(isCheck == 1)
                    {
                        rowAffected.Message = "No Success";
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

        

        public ExcutionResult Insert(FeedBacks employees)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "FeedBacks_Insert";
                using(DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand,"Id",DbType.String,employees.Id);
                    db.AddInParameter(dbCommand, "EmployeeCode", DbType.String, employees.EmployeeCode);
                    db.AddInParameter(dbCommand, "Titles", DbType.String, employees.Titles);
                    db.AddInParameter(dbCommand, "FeedBack", DbType.String, employees.FeedBack);
                    db.AddInParameter(dbCommand, "Contents", DbType.String, employees.Contents);
                    db.AddInParameter(dbCommand, "FileAttached", DbType.String, employees.FileAttached);
                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, employees.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, employees.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, employees.CreatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, employees.CreatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int32, employees.IsDeleted);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
                    if(isCheck == 1)
                    {
                        rowAffected.Message = "Insert Failed";
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

      
        public ExcutionResult Update(FeedBacks employees)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "FeedBacks_Update";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, employees.Id);
                    db.AddInParameter(dbCommand, "EmployeeCode", DbType.String, employees.EmployeeCode);
                    db.AddInParameter(dbCommand, "Titles", DbType.String, employees.Titles);
                    db.AddInParameter(dbCommand, "FeedBack", DbType.String, employees.FeedBack);
                    db.AddInParameter(dbCommand, "Contents", DbType.String, employees.Contents);
                    db.AddInParameter(dbCommand, "FileAttached", DbType.String, employees.FileAttached);
                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, employees.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, employees.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, employees.CreatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, employees.CreatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int32, employees.IsDeleted);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
                    if (isCheck == 1)
                    {
                        rowAffected.Message = "Update Failed";
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
        public static List<FeedBacks> FillCollection(IDataReader _dr)
        {
            List<FeedBacks> list = new List<FeedBacks>();
            try
            {
                while (_dr.Read())
                {
                    FeedBacks feedBacks = new FeedBacks();
                    feedBacks.Id = _dr["Id"].ToString();
                    feedBacks.EmployeeCode = _dr["EmployeeCode"].ToString();
                    feedBacks.Titles = _dr["Titles"].ToString();
                    feedBacks.FeedBack = _dr["FeedBack"].ToString();
                    feedBacks.Contents = _dr["Contents"].ToString();
                    feedBacks.FileAttached = _dr["FileAttached"].ToString();
                    feedBacks.CreatedBy = _dr["CreatedBy"].ToString();
                    feedBacks.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    feedBacks.UpdatedBy = _dr["UpdatedBy"].ToString();
                    feedBacks.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    feedBacks.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if (list.IndexOf(feedBacks) < 0)
                    {
                        list.Add(feedBacks);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            
            return list;
        }

        public static FeedBacks FillObject(IDataReader _dr)
        {
            List<FeedBacks> list = new List<FeedBacks>();
            try
            {
                while (_dr.Read())
                {
                    FeedBacks feedBacks = new FeedBacks();
                    feedBacks.Id = _dr["Id"].ToString();
                    feedBacks.EmployeeCode = _dr["EmployeeCode"].ToString();
                    feedBacks.Titles = _dr["Titles"].ToString();
                    feedBacks.FeedBack = _dr["FeedBack"].ToString();
                    feedBacks.Contents = _dr["Contents"].ToString();
                    feedBacks.FileAttached = _dr["FileAttached"].ToString();
                    feedBacks.CreatedBy = _dr["CreatedBy"].ToString();
                    feedBacks.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    feedBacks.UpdatedBy = _dr["UpdatedBy"].ToString();
                    feedBacks.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    feedBacks.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if (list.IndexOf(feedBacks) < 0)
                    {
                        list.Add(feedBacks);
                    }
                    if(list.Count > 0)
                    {
                        FeedBacks[] fb = list.ToArray();
                        if(fb != null && fb.Length > 0)
                        {
                            return fb[0];
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
