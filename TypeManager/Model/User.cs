using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace TypeManager.Model
{
    public class User
    {
        /// <summary>
        /// 用户工号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 所属角色编号
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 用户组编号
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 用户组
        /// </summary>
        public string GroupName { get; set; }

    }
}
