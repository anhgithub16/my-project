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
    public class Sql_CompanyDataProvider : ICompanyDataProvider
    {
        public Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=QL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }
        #region get data
        public IEnumerable<Company> GetAll()
        {
            List<Company> list = new List<Company>();
            Database db = this.GetDatabase();
            try
            {
                string storeNameProcedure = "Company_GetAll";
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

        public IEnumerable<Company> GetAllByPaging(PagingItem pagingItem)
        {
            List<Company> list = new List<Company>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Company_GetAllByPaging";
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

        public Company GetById(string id)
        {
            Company company = new Company();
            try
            {
                Database db = this.GetDatabase();
                string storeNameprocedure = "Company_GetById";
                DbCommand dbCommand = db.GetStoredProcCommand(storeNameprocedure);
                db.AddInParameter(dbCommand, "Id", DbType.String, id);
                using(IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    company = FillObject(dataReader);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return company;
        }
        public IEnumerable<Company> Search(string keyword)
        {
            List<Company> list = new List<Company>();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Company_Search";
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
        public ExcutionResult Insert(Company company)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Company_Insert";
                using(DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, company.Id);
                    db.AddInParameter(dbCommand, "Code", DbType.String, company.Code);
                    db.AddInParameter(dbCommand, "TaxCode", DbType.String, company.TaxCode);
                    db.AddInParameter(dbCommand, "Name", DbType.String, company.Name);
                    db.AddInParameter(dbCommand, "Address", DbType.String, company.Address);
                    db.AddInParameter(dbCommand, "Phone", DbType.String, company.Phone);
                    db.AddInParameter(dbCommand, "Fax", DbType.String, company.Fax);
                    db.AddInParameter(dbCommand, "Email", DbType.String, company.Email);
                    db.AddInParameter(dbCommand, "Logo", DbType.String, company.Logo);
                    db.AddInParameter(dbCommand, "Deputy", DbType.String, company.Deputy);
                    db.AddInParameter(dbCommand, "Mobile", DbType.String, company.Mobile);
                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, company.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, company.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, company.UpdatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, company.UpdatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int32, company.IsDeleted);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
                    if(isCheck == 1)
                    {
                        rowAffected.Message = "Insert no Success";
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
       
        public ExcutionResult Update(Company company)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Company_Update";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, company.Id);
                    db.AddInParameter(dbCommand, "Code", DbType.String, company.Code);
                    db.AddInParameter(dbCommand, "TaxCode", DbType.String, company.TaxCode);
                    db.AddInParameter(dbCommand, "Name", DbType.String, company.Name);
                    db.AddInParameter(dbCommand, "Address", DbType.String, company.Address);
                    db.AddInParameter(dbCommand, "Phone", DbType.String, company.Phone);
                    db.AddInParameter(dbCommand, "Fax", DbType.String, company.Fax);
                    db.AddInParameter(dbCommand, "Email", DbType.String, company.Email);
                    db.AddInParameter(dbCommand, "Logo", DbType.String, company.Logo);
                    db.AddInParameter(dbCommand, "Deputy", DbType.String, company.Deputy);
                    db.AddInParameter(dbCommand, "Mobile", DbType.String, company.Mobile);
                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, company.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, company.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, company.UpdatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, company.UpdatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int32, company.IsDeleted);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
                    if (isCheck == 1)
                    {
                        rowAffected.Message = "Update no Success";
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
        public ExcutionResult Delete(Company company)
        {
            ExcutionResult rowAffected = new ExcutionResult();
            try
            {
                Database db = this.GetDatabase();
                string storeNameProcedure = "Company_Delete";
                using(DbCommand dbCommand = db.GetStoredProcCommand(storeNameProcedure))
                {
                    db.AddInParameter(dbCommand, "Id", DbType.String, company.Id);
                    db.AddOutParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelper.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
                    if(isCheck == 1)
                    {
                        rowAffected.Message = "Delete Failed";
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
        public static List<Company> FillCollection(IDataReader _dr)
        {
            List<Company> list = new List<Company>();
            try
            {
                while (_dr.Read())
                {
                    Company company = new Company();
                    company.Id = _dr["Id"].ToString();
                    company.Code = _dr["Code"].ToString();
                    company.TaxCode = _dr["TaxCode"].ToString();
                    company.Name = _dr["Name"].ToString();
                    company.Address = _dr["Address"].ToString();
                    company.Phone = _dr["Phone"].ToString();
                    company.Fax = _dr["Fax"].ToString();
                    company.Email = _dr["Email"].ToString();
                    company.Logo = _dr["Logo"].ToString();
                    company.Deputy = _dr["Deputy"].ToString();
                    company.Mobile = _dr["Mobile"].ToString();
                    company.CreatedBy = _dr["CreatedBy"].ToString();
                    company.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    company.UpdatedBy = _dr["UpdatedBy"].ToString();
                    company.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    company.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if(list.IndexOf(company) < 0)
                    {
                        list.Add(company);
                    }
                }
            }
            catch (Exception)
            {
                _dr.Close();
            }
            return list;
        }
        public static Company FillObject(IDataReader _dr)
        {
            List<Company> list = new List<Company>();
            try
            {
                while (_dr.Read())
                {
                    Company company = new Company();
                    company.Id = _dr["Id"].ToString();
                    company.Code = _dr["Code"].ToString();
                    company.TaxCode = _dr["TaxCode"].ToString();
                    company.Name = _dr["Name"].ToString();
                    company.Address = _dr["Address"].ToString();
                    company.Phone = _dr["Phone"].ToString();
                    company.Fax = _dr["Fax"].ToString();
                    company.Email = _dr["Email"].ToString();
                    company.Logo = _dr["Logo"].ToString();
                    company.Deputy = _dr["Deputy"].ToString();
                    company.Mobile = _dr["Mobile"].ToString();
                    company.CreatedBy = _dr["CreatedBy"].ToString();
                    company.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    company.UpdatedBy = _dr["UpdatedBy"].ToString();
                    company.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    company.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if (list.IndexOf(company) < 0)
                    {
                        list.Add(company);
                    }
                    if (list.Count > 0)
                    {
                        Company[] companys = list.ToArray();
                        if(companys != null && companys.Length > 0)
                        {
                            return companys[0];
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
