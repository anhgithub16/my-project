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
    public class Sql_AppParamDataProvider : IAppParamDataProvider
    {
        public  Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=QL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }
        #region get data
        public IEnumerable<AppParam> GetAll()
        {
            List<AppParam> list = new List<AppParam>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "AppParam_GetAll";
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

        public IEnumerable<AppParam> GetAllByPaging(PagingItem pagingItem)
        {
            List<AppParam> list = new List<AppParam>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "AppParam_Get_All_By_Paging";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "orderBy", DbType.String, pagingItem.OrderBy);
                db.AddInParameter(dbCommand, "directionSort", DbType.String, pagingItem.DirectionSort);
                db.AddInParameter(dbCommand, "pageIndex", DbType.Int32, pagingItem.PageIndex);
                db.AddInParameter(dbCommand, "pageSize", DbType.Int32, pagingItem.PageSize);
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

        public AppParam GetById(string id)
        {
            AppParam appParam = new AppParam();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "AppParam_Get_By_Id";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "Id", DbType.String, id);
                using(IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    appParam = FillObject(dataReader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return appParam;
        }
        public IEnumerable<AppParam> Search(string keyword)
        {
            List<AppParam> list = new List<AppParam>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "AppParam_Search";
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
        public ExcutionResult Insert(AppParam appParam)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "AppParam_Insert";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, appParam.Id);
                    db.AddInParameter(dbCommand, "Type", DbType.String, appParam.Type);
                    db.AddInParameter(dbCommand, "Code", DbType.String, appParam.Code);
                    db.AddInParameter(dbCommand, "Name", DbType.String, appParam.Name);
                    db.AddInParameter(dbCommand, "Value", DbType.String, appParam.Value);
                    db.AddInParameter(dbCommand, "OrderNo", DbType.Int32, appParam.OrderNo);
                    db.AddInParameter(dbCommand, "Status", DbType.Int32, appParam.Status);
                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, appParam.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, appParam.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, appParam.UpdatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, appParam.UpdatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int32, appParam.IsDeleted);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
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

        public ExcutionResult Update(AppParam appParam)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "AppParam_Update";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, appParam.Id);
                    db.AddInParameter(dbCommand, "Type", DbType.String, appParam.Type);
                    db.AddInParameter(dbCommand, "Code", DbType.String, appParam.Code);
                    db.AddInParameter(dbCommand, "Name", DbType.String, appParam.Name);
                    db.AddInParameter(dbCommand, "Value", DbType.String, appParam.Value);
                    db.AddInParameter(dbCommand, "OrderNo", DbType.Int32, appParam.OrderNo);
                    db.AddInParameter(dbCommand, "Status", DbType.Int32, appParam.Status);
                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, appParam.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, appParam.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, appParam.UpdatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, appParam.UpdatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int32, appParam.IsDeleted);
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
        public ExcutionResult Delete(AppParam appParam)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "AppParam_Delete";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, appParam.Id);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
                    if(isCheck == 1)
                    {
                        rowAffected.Message = "Delete failed";
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
        public static List<AppParam> FillCollection(IDataReader _dr)
        {
            List<AppParam> list = new List<AppParam>();
            try
            {
                while (_dr.Read())
                {
                    AppParam appParam = new AppParam();
                    appParam.Id = _dr["Id"].ToString();
                    appParam.Type = _dr["Type"].ToString();
                    appParam.Code = _dr["Code"].ToString();
                    appParam.Name = _dr["Name"].ToString();
                    appParam.Value = _dr["Value"].ToString();
                    appParam.OrderNo = ConvertHelper.ToInt32(_dr["OrderNo"], 0);
                    appParam.Status = ConvertHelper.ToInt32(_dr["Status"], 0);
                    appParam.CreatedBy = _dr["CreatedBy"].ToString();
                    appParam.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    appParam.UpdatedBy = _dr["UpdatedBy"].ToString();
                    appParam.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    appParam.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if(list.IndexOf(appParam) < 0)
                    {
                        list.Add(appParam);
                    }
                }
            }
            catch (Exception)
            {
                _dr.Close();
            }
            return list;
        }
        public static AppParam FillObject(IDataReader _dr)
        {
            List<AppParam> list = new List<AppParam>();
            try
            {
                while (_dr.Read())
                {
                    AppParam appParam = new AppParam();
                    appParam.Id = _dr["Id"].ToString();
                    appParam.Type = _dr["Type"].ToString();
                    appParam.Code = _dr["Code"].ToString();
                    appParam.Name = _dr["Name"].ToString();
                    appParam.Value = _dr["Value"].ToString();
                    appParam.OrderNo = ConvertHelper.ToInt32(_dr["OrderNo"], 0);
                    appParam.Status = ConvertHelper.ToInt32(_dr["Status"], 0);
                    appParam.CreatedBy = _dr["CreatedBy"].ToString();
                    appParam.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    appParam.UpdatedBy = _dr["UpdatedBy"].ToString();
                    appParam.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    appParam.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if(list.IndexOf(appParam) < 0)
                    {
                        list.Add(appParam);
                    }
                    if(list.Count > 0)
                    {
                        AppParam[] appParams = list.ToArray();
                        if(appParams != null && appParams.Length > 0)
                        {
                            return appParams[0];
                        }
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
