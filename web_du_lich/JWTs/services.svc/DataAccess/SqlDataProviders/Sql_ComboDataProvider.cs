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
    public class Sql_ComboDataProvider : IComboDataProvider
    {
        public Database GetDatabase()
        {
            string connectionString = "Data Source=ANH;Initial Catalog=TRAVEL;Integrated Security=True";
            return new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
        }
        #region Get Data
        public IEnumerable<Combo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Combo> GetAllByPaging(PagingItem pagingItem)
        {
            throw new NotImplementedException();
        }

        public Combo GetById(string id)
        {
            throw new NotImplementedException();
        }
        public Combo GetByTourDetailId(string tourDetailid)
        {
            Combo combo = new Combo();
            try
            {
                Database db = this.GetDatabase();
                string storeName = "Combo_GetByTourDetailId";
                DbCommand dbCommand = db.GetStoredProcCommand(storeName);
                db.AddInParameter(dbCommand, "TourDetailId", DbType.Guid, Guid.Parse(tourDetailid));
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    combo = FillObject(dataReader);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return combo;
        }
        public IEnumerable<Combo> Search(string keyword)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Modify data
        public ExcutionResult Insert(Combo employees)
        {
            throw new NotImplementedException();
        }

        public ExcutionResult Update(Combo employees)
        {
            throw new NotImplementedException();
        }
        public ExcutionResult Delete(Combo employess)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region bind data
        public static List<Combo> FillCollection(IDataReader _dr)
        {
            List<Combo> _list = new List<Combo>();
            try
            {
                Combo combo = new Combo();
                combo.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                combo.TourDetailId = ConvertHelper.ToGuid(_dr["TourDetailId"], Guid.Empty);
                combo.TenCombo = ConvertHelper.ToString(_dr["TenCombo"], string.Empty);
                combo.NoiDung = ConvertHelper.ToString(_dr["NoiDung"], string.Empty);
                combo.Special = ConvertHelper.ToString(_dr["Special"], string.Empty);
                combo.DieuKien = ConvertHelper.ToString(_dr["DieuKien"], string.Empty);

                combo.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                combo.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                combo.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                combo.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                combo.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                while (_dr.Read())
                {
                    
                    if (_list.IndexOf(combo) < 0)
                    {
                        _list.Add(combo);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return _list;
        }
        public static Combo FillObject(IDataReader _dr)
        {
            List<Combo> _list = new List<Combo>();
            try
            {
                while (_dr.Read())
                {
                    Combo combo = new Combo();
                    combo.Id = ConvertHelper.ToGuid(_dr["Id"], Guid.Empty);
                    combo.TourDetailId = ConvertHelper.ToGuid(_dr["TourDetailId"], Guid.Empty);
                    combo.TenCombo = ConvertHelper.ToString(_dr["TenCombo"], string.Empty);
                    combo.NoiDung = ConvertHelper.ToString(_dr["NoiDung"], string.Empty);
                    combo.Special = ConvertHelper.ToString(_dr["Special"], string.Empty);
                    combo.DieuKien = ConvertHelper.ToString(_dr["DieuKien"], string.Empty);

                    combo.CreatedBy = ConvertHelper.ToString(_dr["CreatedBy"], string.Empty);
                    combo.CreatedAt = ConvertHelper.ToDateTime(_dr["CreatedAt"], DateTime.Now);
                    combo.UpdatedBy = ConvertHelper.ToString(_dr["UpdatedBy"], string.Empty);
                    combo.UpdatedAt = ConvertHelper.ToDateTime(_dr["UpdatedAt"], DateTime.Now);
                    combo.IsDeleted = ConvertHelper.ToInt32(_dr["IsDeleted"], 0);
                    if (_list.IndexOf(combo) < 0)
                    {
                        _list.Add(combo);
                    }
                    if (_list.Count > 0)
                    {
                        Combo[] combos = _list.ToArray();
                        if (combos != null && combos.Length > 0)
                        {
                            return combos[0];
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
