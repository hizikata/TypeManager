using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XuxzLib;
using TypeManager.Model;

namespace TypeManager.DAL
{
    public class Repository
    {
        #region TypePara
        SqlHelper SqlHelper = new SqlHelper("LightMasterMes");
        public List<TypeParaModel> InitialTypePara(string tableName)
        {
            List<TypeParaModel> list = new List<TypeParaModel>();

            string sqlStr = @"SELECT TOP (20) ProductType, TypeLength, TypeSign, ApdSign, TypeCatalog, SnSign, TeCaculateMethod, PackingType, 
                IsSmsrSpotTest, IsImd2SpotTest, TypeVisible, MaterialId, ProductTypeCommon, CheckSnSubLength, YearSign, 
                AlignType, IsUploadHwData, IsChangeToRedoData, HousingLaserSign, ThOneTestType, TransmitRate, Id_Key FROM {0}";
            sqlStr = string.Format(sqlStr, tableName);
            //SqlParameter[] paras = { new SqlParameter("@ProductType", System.Data.SqlDbType.NVarChar, 15) };
            DataSet ds = SqlHelper.GetTableFromDb(sqlStr);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new TypeParaModel
                (
                    row[0].ToString(),
                    Convert.ToInt32(row[1]),
                    row[2].ToString(),
                    row[3].ToString(),
                    row[4].ToString(),
                    row[5].ToString(),
                    row[6].ToString(),
                    row[7].ToString(),
                    Convert.ToInt32(row[8]),
                    Convert.ToInt32(row[9]),
                    Convert.ToInt32(row[10]),
                    row[11].ToString(),
                    row[12].ToString(),
                    Convert.ToInt32(row[13]),
                    row[14].ToString(),
                    row[15].ToString(),
                    Convert.ToInt32(row[16]),
                    Convert.ToInt32(row[17]),
                    row[18].ToString(),
                    row[19].ToString(),
                    row[20].ToString(),
                    row[21].ToString()
                ));
            }
            return list;
        }
        /// <summary>
        /// 从数据库中搜索相应型号信息
        /// </summary>
        /// <param name="tableName">数据表</param>
        /// <param name="productType">产品型号</param>
        /// <returns></returns>
        public List<TypeParaModel> ListFromDatabase(string tableName, string productType)
        {
            List<TypeParaModel> list = new List<TypeParaModel>();
            string sqlStr = @"SELECT ProductType, TypeLength, TypeSign, ApdSign, TypeCatalog, SnSign, TeCaculateMethod, PackingType, 
                IsSmsrSpotTest, IsImd2SpotTest, TypeVisible, MaterialId, ProductTypeCommon, CheckSnSubLength, YearSign, 
                AlignType, IsUploadHwData, IsChangeToRedoData, HousingLaserSign, ThOneTestType, TransmitRate, Id_Key FROM {0} WHERE ProductType=@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras = { new SqlParameter("@ProductType", System.Data.SqlDbType.NVarChar, 15) };
            paras[0].Value = productType;
            DataSet ds = SqlHelper.GetTableFromDb(sqlStr,paras);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new TypeParaModel
                (
                    row[0].ToString(),
                    Convert.ToInt32(row[1]),
                    row[2].ToString(),
                    row[3].ToString(),
                    row[4].ToString(),
                    row[5].ToString(),
                    row[6].ToString(),
                    row[7].ToString(),
                    Convert.ToInt32(row[8]),
                    Convert.ToInt32(row[9]),
                    Convert.ToInt32(row[10]),
                    row[11].ToString(),
                    row[12].ToString(),
                    Convert.ToInt32(row[13]),
                    row[14].ToString(),
                    row[15].ToString(),
                    Convert.ToInt32(row[16]),
                    Convert.ToInt32(row[17]),
                    row[18].ToString(),
                    row[19].ToString(),
                    row[20].ToString(),
                    row[21].ToString()
                ));
            }
            return list;
        }
        public List<string> GetTypeList()
        {
            string sqlStr = @"SELECT DISTINCT ProductType FROM Para_ProductType";
            List<string> list = SqlHelper.GetTypeList(sqlStr);
            return list;
        }
        public void SqlBulkCopyInsert(List<TypeParaModel> data,string tableName)
        {
            SqlHelper.SqlBulkCopyInsert<TypeParaModel>(data, tableName);
        }
        public int UpdateDB(TypeParaModel model,string tableName)
        {
            string sqlStr= @"UPDATE {0} SET ProductType=@ProductType ,TypeLength=@TypeLength,
TypeSign=@TypeSign,ApdSign=@ApdSign, TypeCatalog=@TypeCatalog, SnSign=@SnSign, 
TeCaculateMethod=@TeCaculateMethod,PackingType=@PackingType,IsSmsrSpotTest=@IsSmsrSpotTest, 
IsImd2SpotTest=IsImd2SpotTest,TypeVisible=@TypeVisible, MaterialId=@MaterialId, 
ProductTypeCommon=@ProductTypeCommon,CheckSnSubLength=@CheckSnSubLength, YearSign=@YearSign,
AlignType=@AlignType,IsUploadHwData=@IsUploadHwData, IsChangeToRedoData=@IsChangeToRedoData, 
HousingLaserSign=@HousingLaserSign, ThOneTestType=@ThOneTestType, TransmitRate=@TransmitRate
WHERE ProductType=@ProductType AND Id_Key=@IdKey AND ProductType=@ProductType";
            //todo
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras =
            {
                new SqlParameter("@ProductType",SqlDbType.NVarChar,15),
                new SqlParameter("@TypeLength",SqlDbType.Int,15),
                new SqlParameter("@TypeSign",SqlDbType.NVarChar,15),
                new SqlParameter("@ApdSign",SqlDbType.NVarChar,15),
                new SqlParameter("@TypeCatalog",SqlDbType.NVarChar,15),
                new SqlParameter("@SnSign",SqlDbType.NVarChar,15),
                new SqlParameter("@TeCaculateMethod",SqlDbType.NVarChar,15),
                new SqlParameter("@PackingType",SqlDbType.NVarChar,15),
                new SqlParameter("@IsSmsrSpotTest",SqlDbType.Int,15),
                new SqlParameter("@IsImd2SpotTest",SqlDbType.Int,15),
                new SqlParameter("@TypeVisible",SqlDbType.Int,15),
                new SqlParameter("@MaterialId",SqlDbType.NVarChar,15),
                new SqlParameter("@ProductTypeCommon",SqlDbType.NVarChar,15),
                new SqlParameter("@CheckSnSubLength",SqlDbType.Int,15),
                new SqlParameter("@YearSign",SqlDbType.NVarChar,15),
                new SqlParameter("@AlignType",SqlDbType.NVarChar,15),
                new SqlParameter("@IsUploadHwData",SqlDbType.Int,15),
                new SqlParameter("@IsChangeToRedoData",SqlDbType.Int,15),
                new SqlParameter("@HousingLaserSign",SqlDbType.NVarChar,15),
                new SqlParameter("@ThOneTestType",SqlDbType.NVarChar,15),
                new SqlParameter("@TransmitRate",SqlDbType.NVarChar,15),
                new SqlParameter("@IdKey",SqlDbType.NVarChar,15)

            };
            paras[0].Value = model.ProductTypeName;
            paras[1].Value = model.TypeLength;
            paras[2].Value = model.TypeSign;
            paras[3].Value = model.ApdSign;
            paras[4].Value = model.TypeCatalog;
            paras[5].Value = model.SnSign;
            paras[6].Value = model.TeCaculateMethod;
            paras[7].Value = model.PackingType;
            paras[8].Value = model.IsSmsrSpotTest;
            paras[9].Value = model.IsImd2SpotTest;
            paras[10].Value = model.TypeVisible;
            paras[11].Value = model.MaterialId;
            paras[12].Value = model.ProductTypeCommon;
            paras[13].Value = model.CheckSnSubLength;
            paras[14].Value = model.YearSign;
            paras[15].Value = model.AlignType;
            paras[16].Value = model.IsUploadHwData;
            paras[17].Value = model.IsChangeToRedoData;
            paras[18].Value = model.HousingLaserSign;
            paras[19].Value = model.ThOneTestType;
            paras[20].Value = model.TransmitRate;
            paras[21].Value = model.IdKey;
            return SqlHelper.ExecuteNonQuery(sqlStr, paras);
        }
        #endregion
    }
}
