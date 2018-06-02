using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace TypeManager.Model
{
    public class User
    {
        public int Id_Key { get; set; }
        public string LoginId { get; set; }
        public string LoginPwd { get; set; }
        public string AdminName { get; set; }
        public string WorkStation { get; set; }
        public string Privilege { get; set; }
        
    }
}
