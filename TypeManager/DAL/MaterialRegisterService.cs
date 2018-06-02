using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TypeManager.Model;
using XuxzLib;

namespace TypeManager.DAL
{
    public class MaterialRegisterService
    {
        SqlHelper SqlHelper = new SqlHelper("NBOSA");
        public List<MaterialOrderParameter>Search(string materialName,string tableName)
        {
            List<MaterialOrderParameter> list = new List<MaterialOrderParameter>();
            string sqlStr = @"SELECT MaterialName, MaterialSpecify, MaterialSupplier, 
MaterialMakePlace, Id_Key FROM {0} WHERE MaterialName=@MaterialName";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras = { new SqlParameter("@MaterialName", SqlDbType.NVarChar, 18) };
            paras[0].Value = materialName;
            DataSet ds = SqlHelper.GetTableFromDb(sqlStr, paras);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new MaterialOrderParameter(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(),
                    Convert.ToInt32(row[4])));
            }
            return list;
        }
        public int UpdateDB(MaterialOrderParameter materialOrderParameter,string tableName)
        {
            string sqlStr = @"UPDATE {0} SET MaterialName=@MaterialName, MaterialSpecify =@MaterialSpecify,
MaterialSupplier =@MaterialSupplier,MaterialMakePlace =@MaterialMakePlace WHERE Id_Key =@Id_Key and MaterialName=MaterialName";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras =
            {
                new SqlParameter("@MaterialName",SqlDbType.NVarChar,25),
                new SqlParameter("@MaterialSpecify",SqlDbType.NVarChar,100),
                new SqlParameter("@MaterialSupplier",SqlDbType.NVarChar,15),
                new SqlParameter("@MaterialMakePlace",SqlDbType.NVarChar,25),
                new SqlParameter("@Id_Key",SqlDbType.Int,15)
            };
            paras[0].Value = materialOrderParameter.MaterialName;
            paras[1].Value = materialOrderParameter.MaterialSpecify;
            paras[2].Value = materialOrderParameter.MaterialSupplier;
            paras[3].Value = materialOrderParameter.MaterialMakePlace;
            paras[4].Value = materialOrderParameter.Id_Key;
            return SqlHelper.ExecuteNonQuery(sqlStr, paras);
        }
        public int InsertDB(MaterialOrderParameter materialOrderParameter,string tableName)
        {
            string sqlStr = @"INSERT INTO {0} (MaterialName,MaterialSpecify,MaterialSupplier,MaterialMakePlace) 
VALUES(@MaterialName,@MaterialSpecify,@MaterialSupplier,@MaterialMakePlace)";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras =
            {
                new SqlParameter("@MaterialName",SqlDbType.NVarChar,25),
                new SqlParameter("@MaterialSpecify",SqlDbType.NVarChar,100),
                new SqlParameter("@MaterialSupplier",SqlDbType.NVarChar,15),
                new SqlParameter("@MaterialMakePlace",SqlDbType.NVarChar,25),
            };
            paras[0].Value = materialOrderParameter.MaterialName;
            paras[1].Value = materialOrderParameter.MaterialSpecify;
            paras[2].Value = materialOrderParameter.MaterialSupplier;
            paras[3].Value = materialOrderParameter.MaterialMakePlace;
            return SqlHelper.ExecuteNonQuery(sqlStr, paras);
        }
    }
}
