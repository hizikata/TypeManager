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
    public class StationSetService
    {
        SqlHelper SqlHelper = new SqlHelper("LightMasterMes");
        public List<ProductStation> Search(string productType, string tableName)
        {
            DataSet ds = new DataSet();
            List<ProductStation> list = new List<ProductStation>();
            string sqlStr = @"SELECT StationID,StationName,StationVisible,ProductType,Id_Key
                            FROM {0} WHERE ProductType=@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras =
            {
                new SqlParameter("@ProductType",System.Data.SqlDbType.NVarChar,18)
            };
            paras[0].Value = productType;

            ds = SqlHelper.GetTableFromDb(sqlStr, paras);
            //是否有必要添加行内内容是否为null的判定
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new ProductStation(row[0].ToString(), row[1].ToString(), Convert.ToInt32(row[2]),
                    row[3].ToString(), Convert.ToInt32(row[4])));
            }
            return list;
        }
        public int UpdateDB(ProductStation productStation, string tableName)
        {
            string sqlStr = @"UPDATE {0} SET ProductType=@ProductType,StationID=@StationID,
StationName=@StationName,StationVisible=@StationVisible WHERE Id_Key=@Id_Key AND ProductType=@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras =
            {
                new SqlParameter("@ProductType",SqlDbType.NVarChar,15),
                new SqlParameter("@StationID",SqlDbType.NVarChar,15),
                new SqlParameter("@StationName",SqlDbType.NVarChar,15),
                new SqlParameter("@StationVisible",SqlDbType.NVarChar,15),
                new SqlParameter ("@Id_Key",SqlDbType.Int,15)
            };
            paras[0].Value = productStation.ProductType;
            paras[1].Value = productStation.StationID;
            paras[2].Value = productStation.StationName;
            paras[3].Value = productStation.StationVisible;
            paras[4].Value = productStation.Id_Key;
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
