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
    public class ReportExportService
    {
        SqlHelper SqlHelper = new SqlHelper("LightMasterMes");
        public List<PackingExportPara>Search(string productType,string tableName)
        {
            List<PackingExportPara> list = new List<PackingExportPara>();
            string sqlStr = @"SELECT ProductType, StartRowIndex, EndColIndex, ExportSqlText,
SetRowType, IsNeedPrintPackingLabel,IsExportAboutHW, HwSnColNames, HwVopColNames, HwVbrColNames, 
FillDataContent, Id_Key FROM {0} WHERE ProductType=@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras = { new SqlParameter("@ProductType", System.Data.SqlDbType.NVarChar, 18) };
            paras[0].Value = productType;
            DataSet ds = SqlHelper.GetTableFromDb(sqlStr,paras);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new PackingExportPara(row[0].ToString(), Convert.ToInt32(row[1]), Convert.ToInt32(row[2]),
                    row[3].ToString(), row[4].ToString(), Convert.ToInt32(row[5]), Convert.ToInt32(row[6]), row[7].ToString(),
                    row[8].ToString(), row[9].ToString(), row[10].ToString(), Convert.ToInt32(row[11])));
            }
            return list;
        }
        public int UpdateDB(PackingExportPara packingExportPara,string tableName)
        {
            string sqlStr = @"UPDATE {0} SET ProductType=@ProductType,StartRowIndex =@StartRowIndex,
EndColIndex =@EndColIndex,ExportSqlText =@ExportSqlText,SetRowType =@SetRowType,
IsNeedPrintPackingLabel =@IsNeedPrintPackingLabel,IsExportAboutHW =@IsExportAboutHW,
HwSnColNames =@HwSnColNames,HwVopColNames =@HwVopColNames,HwVbrColNames =@HwVbrColNames,
FillDataContent =@FillDataContent WHERE Id_Key=@Id_Key AND ProductType=@ProductType";
            sqlStr = string.Format(sqlStr, tableName);
            SqlParameter[] paras =
            {
                new SqlParameter("@ProductType",SqlDbType.NVarChar,15),
                new SqlParameter("@StartRowIndex",SqlDbType.Int,15),
                new SqlParameter("@EndColIndex",SqlDbType.Int,15),
                new SqlParameter("@ExportSqlText",SqlDbType.Text),
                new SqlParameter("@SetRowType",SqlDbType.NVarChar,15),
                new SqlParameter("@IsNeedPrintPackingLabel",SqlDbType.Int,15),
                new SqlParameter("@IsExportAboutHW",SqlDbType.Int,15),
                new SqlParameter("@HwSnColNames",SqlDbType.NVarChar,15),
                new SqlParameter("@HwVopColNames",SqlDbType.NVarChar,15),
                new SqlParameter("@HwVbrColNames",SqlDbType.NVarChar,15),
                new SqlParameter("@FillDataContent",SqlDbType.NVarChar,15),
                new SqlParameter("@Id_Key",SqlDbType.Int,15)
            };
            paras[0].Value = packingExportPara.ProductType;
            paras[1].Value = packingExportPara.StartRowIndex;
            paras[2].Value = packingExportPara.EndColIndex;
            paras[3].Value = packingExportPara.ExportText;
            paras[4].Value = packingExportPara.SetRowType;
            paras[5].Value = packingExportPara.IsNeedPrintPackingLabel;
            paras[6].Value = packingExportPara.IsExportAboutHW;
            paras[7].Value = packingExportPara.HwSnColNames;
            paras[8].Value = packingExportPara.HwVopColNames;
            paras[9].Value = packingExportPara.HwVbrColNames;
            paras[10].Value = packingExportPara.FillDataContent;
            paras[11].Value = packingExportPara.Id_Key;
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
