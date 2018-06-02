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
    public class CommonParaService
    {
        SqlHelper SqlHelper = new SqlHelper("LightMasterMes");
        public List<ProductParameter> Search(string ProductType,string tableName)
        {
            List<ProductParameter> list = new List<ProductParameter>();
            string sqlStr = @"SELECT ProductType, ProductStation, ParameterName, 
ParameterMax, ParameterMin, ParameterMemo, TempSign, Id_Key FROM {0} WHERE ProductType=@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras = { new SqlParameter("@ProductType", SqlDbType.NVarChar, 18) };
            paras[0].Value = ProductType;
            DataSet ds = SqlHelper.GetTableFromDb(sqlStr, paras);
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new ProductParameter(row[0].ToString(), row[1].ToString(), row[2].ToString(),
                    row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), Convert.ToInt32(row[7])));
            }
            return list;
        }
        public int UpdateDB(ProductParameter productParameter,string tableName)
        {
            string sqlStr = @"UPDATE {0} SET ProductType=@ProductType,ProductStation=@ProductStation,
ParameterName=@ParameterName,ParameterMax=@ParameterMax,ParameterMin=@ParameterMin,ParameterMemo=@ParameterMemo,
TempSign=@TempSign WHERE Id_Key=@Id_Key AND  ProductType=@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras =
            {
                new SqlParameter("@ProductType",SqlDbType.NVarChar,15),
                new SqlParameter("@ProductStation",SqlDbType.NVarChar,15),
                new SqlParameter("@ParameterName",SqlDbType.NVarChar,15),
                new SqlParameter("@ParameterMax",SqlDbType.NVarChar,15),
                new SqlParameter("@ParameterMin",SqlDbType.NVarChar,15),
                new SqlParameter("@ParameterMemo",SqlDbType.NVarChar,15),
                new SqlParameter("@TempSign",SqlDbType.NVarChar,15),
                new SqlParameter("@Id_Key",SqlDbType.Int,15),
            };
            paras[0].Value = productParameter.ProductType;
            paras[1].Value = productParameter.ProductStation;
            paras[2].Value = productParameter.ParameterName;
            paras[3].Value = productParameter.ParameterMax;
            paras[4].Value = productParameter.ParameterMin;
            paras[5].Value = productParameter.ParameterMemo;
            paras[6].Value = productParameter.TempSign;
            paras[7].Value = productParameter.Id_Key;
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
