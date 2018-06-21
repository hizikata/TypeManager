using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace XuxzLib
{
    public class SqlHelper
    {
        public SqlHelper(string DatabaseName)
        {
            switch (DatabaseName)
            {
                case "LocalLightMasterMes":
                    connString = "Data Source=127.0.0.1; Initial Catalog=LightMasterMes ;User ID=sa;Password=sa2017";
                    break;
                case "LocalNBOSA":
                    connString = "Data Source=127.0.0.1; Initial Catalog=NBOSA ;User ID=sa;Password=sa2017";
                    break;
                case "LightMasterMes":
                    connString = "Data Source=192.168.0.237; Initial Catalog=LightMasterMes ;User ID=sa;Password=lm2011";
                    break;
                case "NBOSA":
                    connString = "Data Source=192.168.0.237; Initial Catalog=NBOSA;User ID=sa;Password=lm2011";
                    break;
                default:throw new Exception("数据库选择不正确");
            }
            
        }
        //本地数据库 127.0.0.1
        private string connString=null;
        //远程数据库 192.168.0.237
        /// <summary>
        /// 从数据库中获取数据表格
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="sqlParameters">sql参数数组</param>
        /// <returns></returns>
        public DataSet GetTableFromDb(string sqlStr,SqlParameter[] sqlParameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 1000;
                        if(sqlParameters!=null)
                        {
                            foreach (SqlParameter para in sqlParameters)
                                cmd.Parameters.Add(para);                           
                        }
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(ds);                          
                        }
                    }
                }
                return ds;
            }           
            catch (Exception)
            {
                throw;
            }
            
        }
        public void UpdataFromDataTable(string sqlStr, SqlParameter[] paras, DataSet ds,string tableName)
        {
            using(SqlConnection conn=new SqlConnection(connString))
            {
                using(SqlCommand cmd=new SqlCommand(sqlStr, conn))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    if (paras != null)
                    {
                        foreach (var item in paras)
                            cmd.Parameters.Add(item);
                    }
                    using(SqlDataAdapter ada=new SqlDataAdapter(cmd))
                    {
                        ada.Update(ds,tableName);
                    }
                }
            }
        }
        /// <summary>
        /// 从数据库中返回表格数据
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <returns></returns>
        public DataSet GetTableFromDb(string sqlStr)       
        {
            return this.GetTableFromDb(sqlStr, null);
            
        }
        /// <summary>
        /// 判断在数据库中是否已存在对应数据
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="parameters">sql参数</param>
        /// <returns></returns>
        public bool IsExist(string sqlStr,SqlParameter[]parameters)
        {
            using(SqlConnection conn=new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {
                    if (parameters != null)
                    {
                        foreach (SqlParameter para in parameters)
                            cmd.Parameters.Add(para);
                    }
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return true;
                        else
                            return false;
                    }

                }
                
            }          
        }
        /// <summary>
        /// 向数据库插入数据
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sqlStr, SqlParameter[] paras)
        {
            int count = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (paras != null)
                        {
                            foreach (var para in paras)
                            {
                                cmd.Parameters.Add(para);
                            }
                        }
                        count = cmd.ExecuteNonQuery();
                    }
                }
                return count;
            }
            catch (Exception )
            {

                throw;
            }
             
            
        }
        public int ExecuteNonQuery(string sqlStr)
        {
            int count = 0;
            count = this.ExecuteNonQuery(sqlStr, null);
            return count;

        }
        public bool IsPropertyNumExist(string sqlStr, SqlParameter[] paras)
        {
            try
            {
                using(SqlConnection conn=new SqlConnection(connString)){
                    using (SqlCommand cmd = new SqlCommand(sqlStr,conn))
                    {                     
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        foreach(var para in paras)
                        {
                            cmd.Parameters.Add(para);
                        }                            
                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (reader.Read())
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #region GetTypeList

        /// <summary>
        /// 获取List列表
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public List<string> GetTypeList(string sqlStr, SqlParameter[] paras)
        {
            List<string> listStr = new List<string>();
            try
            {
                using(SqlConnection conn=new SqlConnection(connString))
                {
                    using(SqlCommand cmd=new SqlCommand(sqlStr, conn))
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                        if (paras != null)
                        {
                            foreach (var item in paras)
                                cmd.Parameters.Add(item);
                        }                           
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                            listStr.Add(reader["ProductType"].ToString());
                    }
                }
                return listStr;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取对应列List<string>
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="paras"></param>
        /// <paramref name="ColumnName">对应列名</paramref>
        /// <returns></returns>
        public List<string> GetList(string sqlStr, SqlParameter[] paras,string ColumnName)
        {
            List<string> listStr = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                        if (paras != null)
                        {
                            foreach (var item in paras)
                                cmd.Parameters.Add(item);
                        }
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                            listStr.Add(reader[ColumnName].ToString());
                    }
                }
                return listStr;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<string>GetTypeList(string sqlStr)
        {
            return GetTypeList(sqlStr, null);
        }
        #endregion
        #region 使用SqlBulkCopy将DataTable中的数据批量插入数据库中  
        /// <summary>  
        /// 注意：DataTable中的列需要与数据库表中的列完全一致。
        /// 已自测可用。
        /// </summary>  
        /// <param name="conStr">数据库连接串</param>
        /// <param name="strTableName">数据库中对应的表名</param>  
        /// <param name="dtData">数据集</param>  
        public void SqlBulkCopyInsert(string tableName, DataTable dt)
        {
            try
            {
                using (SqlBulkCopy sqlRevdBulkCopy = new SqlBulkCopy(connString))//引用SqlBulkCopy  
                {
                    sqlRevdBulkCopy.DestinationTableName = tableName;//数据库中对应的表名  
                    sqlRevdBulkCopy.NotifyAfter = dt.Rows.Count;//有几行数据  
                    sqlRevdBulkCopy.WriteToServer(dt);//数据导入数据库  
                    sqlRevdBulkCopy.Close();//关闭连接  
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// List<T>数据写入数据库 可代替Insert操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        public void SqlBulkCopyInsert<T>(List<T> data,string tableName)
        {
            List<PropertyInfo> pList = new List<PropertyInfo>();
            DataTable dt = new DataTable();
            Array.ForEach<PropertyInfo>(typeof(T).GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach(var item in data)
            {
                DataRow row = dt.NewRow();
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                dt.Rows.Add(row);
            }
            SqlBulkCopyInsert(tableName, dt);
        }
        /// <summary>
        /// 返回SqlDataReader
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetDataReader(string sqlStr, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using(SqlConnection conn =new SqlConnection(connString))
            {
                using(SqlCommand cmd=new SqlCommand(sqlStr, conn))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    if (parameters != null)
                    {
                        foreach (var para in parameters)
                        {
                            cmd.Parameters.Add(para);
                        }
                    }
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                    return dt;
                }
            }
        }
        #endregion
    }
}
