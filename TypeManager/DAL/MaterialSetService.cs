using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XuxzLib;
using System.Data.SqlClient;
using TypeManager.Model;
using System.Data;

namespace TypeManager.DAL
{
    public class MaterialSetService
    {
        SqlHelper SqlHelper = new SqlHelper("NBOSA");
        public List<ProductMatchSpecify>Search(string productType,string tableName)
        {
            List<ProductMatchSpecify> list = new List<ProductMatchSpecify>();
            string sqlStr = @"SELECT ProductType, LD_Spec, APD_Spec, PD_Spec, Isolator_Spec,
ZeroFilter_Spec, FortyFiveFilter_Spec,Id_Key FROM {0} WHERE ProductType=@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras = { new SqlParameter("@ProductType", SqlDbType.NVarChar, 18) };
            paras[0].Value = productType;
            DataSet ds = SqlHelper.GetTableFromDb(sqlStr, paras);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new ProductMatchSpecify(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(),
                    row[4].ToString(), row[5].ToString(), row[6].ToString(), Convert.ToInt32(row[7])));
            }
            return list;
        }
        public int UpdateDB(ProductMatchSpecify productMatchSpecify,string tableName)
        {
            string sqlStr = @"UPDATE {0} SET ProductType =@ProductType,LD_Spec =@LD_Spec,
APD_Spec =@APD_Spec,PD_Spec =@PD_Spec,Isolator_Spec =@Isolator_Spec,ZeroFilter_Spec =@ZeroFilter_Spec,
FortyFiveFilter_Spec =@FortyFiveFilter_Spec WHERE Id_Key=@Id_Key AND ProductType =@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras =
            {
                new SqlParameter("@ProductType",SqlDbType.NVarChar,100),
                new SqlParameter("@LD_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@APD_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@PD_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@Isolator_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@ZeroFilter_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@FortyFiveFilter_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@Id_Key",SqlDbType.Int,15)
            };
            paras[0].Value = productMatchSpecify.ProductType;
            paras[1].Value = string.IsNullOrEmpty(productMatchSpecify.LD_Spec) ? DBNull.Value : (object)productMatchSpecify.LD_Spec;
            paras[2].Value = string.IsNullOrEmpty(productMatchSpecify.APD_Spec) ? DBNull.Value : (object)productMatchSpecify.APD_Spec;
            paras[3].Value = string.IsNullOrEmpty(productMatchSpecify.PD_Spec) ? DBNull.Value : (object)productMatchSpecify.PD_Spec;
            paras[4].Value = string.IsNullOrEmpty(productMatchSpecify.Isolator_Spec) ? DBNull.Value : (object)productMatchSpecify.Isolator_Spec;
            paras[5].Value = string.IsNullOrEmpty(productMatchSpecify.ZeroFilter_Spec) ? DBNull.Value : (object)productMatchSpecify.ZeroFilter_Spec;
            paras[6].Value = string.IsNullOrEmpty(productMatchSpecify.FortyFiveFilter_Spec) ? DBNull.Value : (object)productMatchSpecify.FortyFiveFilter_Spec;
            paras[7].Value = productMatchSpecify.Id_Key;
            return SqlHelper.ExecuteNonQuery(sqlStr, paras);
        }
        public int InsertDB(ProductMatchSpecify productMatchSpecify,string tableName)
        {
            string sqlStr = @"Insert INTO {0}(ProductType,LD_Spec,APD_Spec,PD_Spec,Isolator_Spec,ZeroFilter_Spec,FortyFiveFilter_Spec) 
VALUES(@ProductType,@LD_Spec,@APD_Spec,@PD_Spec,@Isolator_Spec,@ZeroFilter_Spec,@FortyFiveFilter_Spec)";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras = {
                new SqlParameter("@ProductType",SqlDbType.NVarChar,100),
                new SqlParameter("@LD_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@APD_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@PD_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@Isolator_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@ZeroFilter_Spec",SqlDbType.NVarChar,100),
                new SqlParameter("@FortyFiveFilter_Spec",SqlDbType.NVarChar,100),
            };
            paras[0].Value = productMatchSpecify.ProductType;
            paras[1].Value = string.IsNullOrEmpty(productMatchSpecify.LD_Spec) ? DBNull.Value : (object)productMatchSpecify.LD_Spec;
            paras[2].Value = string.IsNullOrEmpty(productMatchSpecify.APD_Spec) ? DBNull.Value : (object)productMatchSpecify.APD_Spec;
            paras[3].Value = string.IsNullOrEmpty(productMatchSpecify.PD_Spec) ? DBNull.Value : (object)productMatchSpecify.PD_Spec;
            paras[4].Value = string.IsNullOrEmpty(productMatchSpecify.Isolator_Spec) ? DBNull.Value : (object)productMatchSpecify.Isolator_Spec;
            paras[5].Value = string.IsNullOrEmpty(productMatchSpecify.ZeroFilter_Spec) ? DBNull.Value : (object)productMatchSpecify.ZeroFilter_Spec;
            paras[6].Value = string.IsNullOrEmpty(productMatchSpecify.FortyFiveFilter_Spec) ? DBNull.Value : (object)productMatchSpecify.FortyFiveFilter_Spec;
            return SqlHelper.ExecuteNonQuery(sqlStr, paras);

        }
        public List<string> GetTypeList(string tableName)
        {
            string sqlStr = @"SELECT DISTINCT ProductType FROM {0}";
            sqlStr = string.Format(sqlStr, tableName);
            return SqlHelper.GetTypeList(sqlStr);
        }
    }
}
