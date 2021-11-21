using JWT.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Data
{
    public class Sql_User_Provider : IUserDataProvider
    {
      

        public Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=TRAVEL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }
        public List<Users> GetById(Users user)
        {
            List<Users> list = new List<Users>();
            Database db = this.GetDatabase();
            string storeName = "USER_GetById";
            DbCommand dbCommand = db.GetStoredProcCommand(storeName);
            db.AddInParameter(dbCommand, "Id", DbType.Guid, user.Id);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    Users users = new Users();
                    users.Id = ConvertHelpers.ToGuid(dr["Id"], Guid.Empty);
                    users.UserName = ConvertHelpers.ToString(dr["UserName"], string.Empty);
                    users.Password = ConvertHelpers.ToString(dr["Password"], string.Empty);
                    users.FirstName = ConvertHelpers.ToString(dr["FirstName"], string.Empty);
                    users.LastName = ConvertHelpers.ToString(dr["LastName"], string.Empty);
                    users.FullName = ConvertHelpers.ToString(dr["FullName"], string.Empty);
                    users.Gender = ConvertHelpers.ToInt16(dr["Gender"], 0);
                    users.DateOfBirth = ConvertHelpers.ToDateTime(dr["DateOfBirth"], DateTime.Now);
                    users.BirthPlace = ConvertHelpers.ToString(dr["BirthPlace"], string.Empty);
                    users.Email = ConvertHelpers.ToString(dr["Email"], string.Empty);
                    users.Phone = ConvertHelpers.ToString(dr["Phone"], string.Empty);
                    users.CreatedBy = ConvertHelpers.ToString(dr["CreatedBy"], string.Empty);
                    users.CreatedAt = ConvertHelpers.ToDateTime(dr["CreatedAt"], DateTime.Now);
                    users.UpdatedBy = ConvertHelpers.ToString(dr["UpdatedBy"], string.Empty);
                    users.UpdatedAt = ConvertHelpers.ToDateTime(dr["UpdatedAt"], DateTime.Now);
                    users.IsDeleted = ConvertHelpers.ToInt16(dr["IsDeleted"], 0);

                    list.Add(users);
                }
                return list;
            }
        }
        public List<User> getEmpCode(string empCode)
        {
            List<User> list = new List<User>();
            Database db = this.GetDatabase();
            string storeName = "User_GetByEmpCode";
            DbCommand dbCommand = db.GetStoredProcCommand(storeName);
            db.AddInParameter(dbCommand, "EmployeeCode", DbType.String, empCode);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    User user = new User();
                    user.userName = dr["UserName"].ToString();
                    user.passWord = dr["PassWord"].ToString();
                    user.EmployeeCode = dr["EmployeeCode"].ToString();
                    user.AppCode = dr["AppCode"].ToString();
                    list.Add(user);
                }
            }
            return list;
        }

        public List<Users> getUser(string userName)
        {
            List<Users> list = new List<Users>();
            Database db = this.GetDatabase();
            string storeName = "User_Get_ByUser";
            DbCommand dbCommand = db.GetStoredProcCommand(storeName);
            db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
            using (IDataReader  dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    Users users = new Users();
                    users.Id = ConvertHelpers.ToGuid(dr["Id"], Guid.Empty);
                    users.UserName = ConvertHelpers.ToString(dr["UserName"],string.Empty);
                    users.Password = ConvertHelpers.ToString(dr["Password"], string.Empty);
                    users.FirstName = ConvertHelpers.ToString(dr["FirstName"], string.Empty);
                    users.LastName = ConvertHelpers.ToString(dr["LastName"], string.Empty);
                    users.FullName = ConvertHelpers.ToString(dr["FullName"], string.Empty);
                    users.Gender = ConvertHelpers.ToInt16(dr["Gender"], 0);
                    users.DateOfBirth = ConvertHelpers.ToDateTime(dr["DateOfBirth"], DateTime.Now);
                    users.BirthPlace = ConvertHelpers.ToString(dr["BirthPlace"], string.Empty);
                    users.Email = ConvertHelpers.ToString(dr["Email"], string.Empty);
                    users.Phone = ConvertHelpers.ToString(dr["Phone"], string.Empty);
                    users.CreatedBy = ConvertHelpers.ToString(dr["CreatedBy"], string.Empty);
                    users.CreatedAt = ConvertHelpers.ToDateTime(dr["CreatedAt"], DateTime.Now);
                    users.UpdatedBy = ConvertHelpers.ToString(dr["UpdatedBy"], string.Empty);
                    users.UpdatedAt = ConvertHelpers.ToDateTime(dr["UpdatedAt"], DateTime.Now);
                    users.IsDeleted = ConvertHelpers.ToInt16(dr["IsDeleted"], 0);

                    list.Add(users);
                }
            }
            return list;
        }

        public ExcutionResultAuth Insert(Users users)
        {
            ExcutionResultAuth result = new ExcutionResultAuth();
            try
            {
                Database db = this.GetDatabase();
                string storeName = "USER_Insert";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeName))
                {
                    
                    users.Id = Guid.NewGuid();
                    db.AddInParameter(dbCommand, "Id", DbType.Guid, users.Id);
                    db.AddInParameter(dbCommand, "UserName", DbType.String, users.UserName);
                    db.AddInParameter(dbCommand, "Password", DbType.String, users.Password);
                    db.AddInParameter(dbCommand, "FirstName", DbType.String, users.FirstName);
                    db.AddInParameter(dbCommand, "LastName", DbType.String, users.LastName);
                    db.AddInParameter(dbCommand, "FullName", DbType.String, users.FullName);
                    db.AddInParameter(dbCommand, "Gender", DbType.Int16, users.Gender);
                    db.AddInParameter(dbCommand, "DateOfBirth", DbType.DateTime, users.DateOfBirth);
                    db.AddInParameter(dbCommand, "BirthPlace", DbType.String, users.BirthPlace);
                    db.AddInParameter(dbCommand, "Email", DbType.String, users.Email);
                    db.AddInParameter(dbCommand, "Phone", DbType.String, users.Phone);

                    db.AddInParameter(dbCommand, "CreatedBy", DbType.String, users.CreatedBy);
                    db.AddInParameter(dbCommand, "CreatedAt", DbType.DateTime, users.CreatedAt);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, users.UpdatedBy);
                    db.AddInParameter(dbCommand, "UpdatedAt", DbType.DateTime, users.UpdatedAt);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Int16, users.IsDeleted);
                    db.AddInParameter(dbCommand, "IsCheck", DbType.Int32, 0x20);
                    db.ExecuteNonQuery(dbCommand);
                    var isCheck = ConvertHelpers.ToInt32(db.GetParameterValue(dbCommand, "IsCheck"), 0);
                    if(isCheck == 1)
                    {
                        result.ErrorCode = 1;
                    }
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                
            }
            return result;
           
        }

        public ExcutionResultAuth UpdatePassword(User user)
        {
            ExcutionResultAuth rowAffected = new ExcutionResultAuth();

            try
            {
                Database db = this.GetDatabase();
                string storeName = "User_Change_Password";
                using (DbCommand dbCommand = db.GetStoredProcCommand(storeName))
                {
                    db.AddInParameter(dbCommand, "userName", DbType.String, user.userName);
                    db.AddInParameter(dbCommand, "newPassword", DbType.String, user.passWord);
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception)
            {
                rowAffected.Message = "Change password failed";
            }
            return rowAffected;
        }
    }
}
