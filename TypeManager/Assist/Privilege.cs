using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeManager.Model;

namespace TypeManager.Assist
{
    public enum UserPrivilege
    {
        admin,
        developer,
        others
    }
    public interface IGetPrivilege
    {
        /// <summary>
        /// 获取当前用户权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserPrivilege GetPrivilege(User user);
        bool IsEditable { get; set; }
    }
}
