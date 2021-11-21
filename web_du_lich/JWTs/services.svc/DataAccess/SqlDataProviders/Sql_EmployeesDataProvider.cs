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
    public class Sql_EmployeesDataProvider:IEmployeeDataProvider
    {
        #region get data
        public Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=QL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }
        public Employees GetById(string id)
        {
            Employees employees = null;
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Employee_get_byId";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "Id", DbType.String, id);
                using(IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    employees = FillObject(dataReader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return employees;
        }

        public IEnumerable<Employees> GetAll()
        {
            IEnumerable<Employees> list = null;
            try
            {
                Database db = this.GetDatabase();
                string storeName = "Employee_get_all";
                DbCommand dbCommand = db.GetStoredProcCommand(storeName);
                using(IDataReader dataReader = db.ExecuteReader(dbCommand))
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

        public IEnumerable<Employees> GetAllByPaging(PagingItem pagingItem)
        {
            if (pagingItem == null)
            {
                pagingItem = new PagingItem();
            }
            IEnumerable<Employees> list = null;
            try
            {
                Database db = this.GetDatabase();
                string storeName = "Employee_Get_All_By_Paging";
                DbCommand dbCommand = db.GetStoredProcCommand(storeName);
                db.AddInParameter(dbCommand, "orderBy", DbType.String, pagingItem.OrderBy);
                db.AddInParameter(dbCommand, "directionSort", DbType.String, pagingItem.DirectionSort);
                db.AddInParameter(dbCommand, "pageSize", DbType.Int32, pagingItem.PageSize);
                db.AddInParameter(dbCommand, "pageIndex", DbType.Int32, pagingItem.PageIndex);
                db.AddOutParameter(dbCommand, "totalRows", DbType.Int32, 0x20);
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
        public IEnumerable<Employees> Search(string keyword)
        {
            List<Employees> list = new List<Employees>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Employee_search";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure);
                db.AddInParameter(dbCommand, "LastName", DbType.String, keyword);
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
        #region modify data

        public ExcutionResult Insert(Employees employees)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Employee_Insert";
                using(DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, employees.id);
                    db.AddInParameter(dbCommand, "LastName", DbType.String, employees.LastName);
                    db.AddInParameter(dbCommand, "FullName", DbType.String, employees.FullName);
                    db.AddInParameter(dbCommand, "Email", DbType.String, employees.Email);
                    db.AddInParameter(dbCommand, "Address", DbType.String, employees.Address);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, employees.CreatedAt);
                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, employees.CreatedBy);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int16, employees.IsDeleted);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, employees.UpdatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, employees.UpdatedBy);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = int.Parse((db.GetParameterValue(dbCommand, "IsCheck")).ToString());
                    if(isCheck == 1)
                    {
                        rowAffected.Message = "No success";
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

        public ExcutionResult Update(Employees employees)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Employee_Update";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, employees.id);
                    db.AddInParameter(dbCommand, "LastName", DbType.String, employees.LastName);
                    db.AddInParameter(dbCommand, "FullName", DbType.String, employees.FullName);
                    db.AddInParameter(dbCommand, "Email", DbType.String, employees.Email);
                    db.AddInParameter(dbCommand, "Address", DbType.String, employees.Address);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, employees.CreatedAt);
                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, employees.CreatedBy);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int16, employees.IsDeleted);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, employees.UpdatedAt);
                    db.AddInParameter(dbCommand, "UpdateBy", DbType.String, employees.UpdatedBy);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = int.Parse((db.GetParameterValue(dbCommand, "IsCheck")).ToString());
                    if (isCheck == 1)
                    {
                        rowAffected.Message = "No success";
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
        public ExcutionResult Delete(Employees employees)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Employee_Delete";
                using(DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, employees.id);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = int.Parse(db.GetParameterValue(dbCommand, "IsCheck").ToString());
                    if(isCheck == 1)
                    {
                        rowAffected.Message = "No success";
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
        public static List<Employees> FillCollection(IDataReader _dr)
        {
            List<Employees> list = new List<Employees>();
            try
            {
                while (_dr.Read())
                {
                    Employees emp = new Employees();
                    emp.id = _dr["Id"].ToString();
                    emp.LastName = _dr["LastName"].ToString();
                    emp.FullName = _dr["FullName"].ToString();
                    emp.Email = _dr["Email"].ToString();
                    emp.Address = _dr["Address"].ToString();
                    emp.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    emp.CreatedBy = _dr["CreateBy"].ToString();
                    emp.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    emp.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    emp.UpdatedBy = _dr["UpdatedBy"].ToString();
                    if (list.IndexOf(emp)<0)
                    {
                        list.Add(emp);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return list;
        }
        public static Employees FillObject(IDataReader _dr)
        {
            List<Employees> list = new List<Employees>();
            try
            {
                while (_dr.Read())
                {
                    Employees emp = new Employees();
                    emp.id = _dr["Id"].ToString();
                    emp.LastName = _dr["LastName"].ToString();
                    emp.FullName = _dr["FullName"].ToString();
                    emp.Email = _dr["Email"].ToString();
                    emp.Address = _dr["Address"].ToString();
                    emp.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    emp.CreatedBy = _dr["CreateBy"].ToString();
                    emp.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    emp.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    emp.UpdatedBy = _dr["UpdatedBy"].ToString();
                    if (list.IndexOf(emp) < 0)
                    {
                        list.Add(emp);
                    }
                    if(list.Count > 0)
                    {
                        Employees[] emps = list.ToArray();
                        if(emps != null && emps.Length > 0)
                        {
                            return emps[0];
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
