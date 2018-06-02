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
    public class ThreeInOneService
    {
        SqlHelper SqlHelper = new SqlHelper("LightMasterMes");
        public List<CommonParaSet> Search(string productType, string tableName)
        {
            List<CommonParaSet> list = new List<CommonParaSet>();
            string sqlStr = @"SELECT ApplicationName, ProductType, ProductStation, 
ParameterName, ParameterValue, CurrentTemperature, Memo, OpPerson, OpDate, Id_Key 
FROM {0} WHERE ProductType=@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras = { new SqlParameter("@ProductType", SqlDbType.NVarChar, 18) };
            paras[0].Value = productType;
            DataSet ds = SqlHelper.GetTableFromDb(sqlStr, paras);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new CommonParaSet(row[0].ToString(), row[1].ToString(), row[2].ToString(),
                    row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(),
                    row[8].ToString(), Convert.ToInt32(row[9])));
            }
            return list;
        }
        public int UpdateDB(CommonParaSet commonParaSet, string tableName)
        {
            string sqlStr = @"UPDATE {0} SET ApplicationName=@ApplicationName,ProductType=@ProductType,
ProductStation=@ProductStation,ParameterName=@ParameterName,ParameterValue=@ParameterValue,
CurrentTemperature=@CurrentTemperature,Memo=@Memo,OpPerson=@OpPerson,OpDate=@OpDate 
WHERE Id_Key=@Id_Key AND ProductType=@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras =
            {
                new SqlParameter("@ApplicationName",SqlDbType.NVarChar,15),
                new SqlParameter("@ProductType",SqlDbType.NVarChar,15),
                new SqlParameter("@ProductStation",SqlDbType.NVarChar,15),
                new SqlParameter("@ParameterName",SqlDbType.NVarChar,15),
                new SqlParameter("@ParameterValue",SqlDbType.NVarChar,15),
                new SqlParameter("@CurrentTemperature",SqlDbType.NVarChar,15),
                new SqlParameter("@Memo",SqlDbType.NVarChar,15),
                new SqlParameter("@OpPerson",SqlDbType.NVarChar,15),
                //数据格式为日期，是否需要转换？
                new SqlParameter("@OpDate",SqlDbType.SmallDateTime,15),
                new SqlParameter("@Id_Key",SqlDbType.Int,15)
            };
            paras[0].Value = commonParaSet.ApplicationName;
            paras[1].Value = commonParaSet.ProductType;
            paras[2].Value = commonParaSet.ProductStation;
            paras[3].Value = commonParaSet.ParameterName;
            paras[4].Value = commonParaSet.ParameterValue;
            paras[5].Value = commonParaSet.CurrentTemperature;
            paras[6].Value = commonParaSet.Memo;
            paras[7].Value = commonParaSet.OpPerson;
            paras[8].Value = commonParaSet.OpDate;
            paras[9].Value = commonParaSet.Id_Key;
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
