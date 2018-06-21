using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string livPath= @"\\192.168.0.237\TestParameter\LIV_database\setup";
            string SenFilePath = @"\\192.168.0.237\TestParameter\ParameterFile\Sen ParameterFile\SenParameterSet.xml";
            string path = @"\\192.168.0.237\TestParameter";
            string pwd = "eic2018";
            string userName = @"ms003\Admin";
            string connectStr = "net use " + path + " " + pwd + " /user:" + userName;
            bool state = IPCConnection.ConnectState(connectStr);
            if (state == true)
            {
                string[] fileNames;
                if (Directory.Exists(livPath))
                {
                     fileNames = Directory.GetFiles(livPath);
                    foreach (string item in fileNames)
                    {
                        Console.WriteLine(item);
                    }
                }
                if (File.Exists(SenFilePath))
                {

                    Console.WriteLine("存在Sen参数文件");
                }
                
            }
            else
            {
                Console.WriteLine("连接远程文件夹失败");
            }
            Console.ReadKey();
            IPCConnection.ConnectState("net use * /del /y");
        }
    }
}
