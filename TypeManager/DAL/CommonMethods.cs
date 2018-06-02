using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XuxzLib;

namespace TypeManager.DAL
{
    public class CommonMethods
    {
        public static SqlHelper SqlHelper;
        static CommonMethods()
        {
            SqlHelper = new SqlHelper("LightMasterMes");
        }
        public static List<string> ReadFromFile()
        {
            string filePath = Environment.CurrentDirectory + "\\ProductTypeList.txt";
            string typeName;
            List<string> list = new List<string>();
            using (StreamReader sr = File.OpenText(filePath))
            {
                while ((typeName = sr.ReadLine()) != null)
                {
                    if (typeName.Trim() != string.Empty)
                    {
                        typeName = typeName.Trim();
                        list.Add(typeName);
                    }
                }
            }
            return list;
        }

        public static void SqlBulkCopyInsert<T>(List<T> list,string tableName)
        {
            SqlHelper.SqlBulkCopyInsert<T>(list, tableName);
        }
        public static List<string> ListFilter(List<string> list,string str)
        {
            List<string> listNew = new List<string>();
            foreach (string item in list)
            {
                if (item.Contains(str))
                    listNew.Add(item);
            }
            return listNew;
        }
    }
}
