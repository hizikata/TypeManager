using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XuxzLib;
using TypeManager.Model;
using System.Data.SqlClient;
using System.Data;

namespace TypeManager.DAL
{
    public class LoginService
    {
        #region Fields
        private readonly SqlHelper sqlHelper = new SqlHelper("LightMasterMes");
        #endregion
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="userId">工号</param>
        /// <param name="password">密码</param>
        public bool Validation(string userId, string password)
        {
            string sqlStr = @"SELECT UserName FROM User_Info WHERE UserID=@UserId AND Password=@Password";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@UserId",System.Data.SqlDbType.VarChar,10),
                new SqlParameter("@Password",System.Data.SqlDbType.VarChar,10)
            };
            paras[0].Value = userId;
            paras[1].Value = password;
            return sqlHelper.IsExist(sqlStr, paras);
        }
        /// <summary>
        /// 获取用户模型
        /// </summary>
        /// <param name="userId">工号</param>
        public User GetUserModel(string userId)
        {
            User user = new User();
            string sqlStr = @"SELECT UserID ,UserName,User_Info.RoleID,RoleName,User_Info.GroupID,GroupName FROM User_Info,User_Role,User_Group 
WHERE User_Info.RoleID=User_Role.RoleID AND User_Info.GroupID=User_Group.GroupID AND UserID=@UserId";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@UserId", System.Data.SqlDbType.VarChar, 10) };
            paras[0].Value = userId;
            DataTable dt= sqlHelper.GetDataReader(sqlStr, paras);
            DataRow row = dt.Rows[0];
            user.UserId = row["UserID"].ToString().Trim();
            user.UserName = row["UserName"].ToString().Trim();
            user.RoleId = row["UserID"].ToString().Trim();
            user.RoleName = row["RoleName"].ToString().Trim();
            user.GroupId = row["GroupID"].ToString().Trim();
            user.GroupName = row["GroupName"].ToString().Trim();
            return user;
        }
    }
}
