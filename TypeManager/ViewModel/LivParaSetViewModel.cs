using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TypeManager.Model;
using TypeManager.CommonClass;
using System.Windows;
using System.IO;

namespace TypeManager.ViewModel
{
    public class LivParaSetViewModel : ViewModelBase
    {
        #region Fields
        readonly string Path = @"\\192.168.0.237\TestParameter\RDTest";
        readonly string Pwd = "eic2018";
        readonly string UserName = @"ms003\Admin";
        string FileName;
        #endregion
        #region Properties
        public string[] FileNames { get; set; }
        public List<LivParaModel> ListDisplayInfo { get; set; }
        public Dictionary<string, string> DictProductType { get; set; }
        private ObservableCollection<LivParaModel> _displayInfo;

        public ObservableCollection<LivParaModel> DisplayInfo
        {
            get { return _displayInfo; }
            set { _displayInfo = value; RaisePropertyChanged(() => DisplayInfo); }
        }
        public Dictionary<string, string> DictParaInfo { get; set; }


        #endregion

        #region Constructors
        public LivParaSetViewModel()
        {
            Initialize();
        }
        #endregion
        #region Methods
        private void Initialize()
        {
            try
            {
                bool connState = false;
                //连接远程文件夹
                string connectStr = "net use " + Path + " " + Pwd + " /user:" + UserName;
                connState = IPCConnection.ConnectState(connectStr);
                if (connState)
                {
                    //共享文件夹的目录
                    DirectoryInfo theFolder = new DirectoryInfo(Path);
                    string filename = theFolder.ToString();
                    //获取目录下的全部文件名
                    FileNames = Directory.GetFiles(Path);
                }
                else
                {
                    MessageBox.Show("连接远程文件夹失败！", "系统提示");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示");
            }
        }
        #endregion
        #region Commands
        public RelayCommand<object> QueryCommand
        {
            get
            {
                return new RelayCommand<object>(obj => ExecuteQueryCommand(obj));
            }
        }
        public RelayCommand<object> UpdateCommand
        {
            get
            {
                return new RelayCommand<object>(obj => ExecuteUpdateCommand(obj));
            }
        }
        #endregion
        #region CommandMethods
        private void ExecuteQueryCommand(object obj)
        {
            try
            {
                DictParaInfo = new Dictionary<string, string>();
                ListDisplayInfo = new List<LivParaModel>();
                FileName = obj.ToString();
                FileInfo fileInfo = new FileInfo(FileName);
                using (StreamReader sr = fileInfo.OpenText())
                {
                    string[] strArray = null;
                    string output = null;
                    while ((output = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(output.Trim()))
                        {
                            strArray = output.Split('=');
                            DictParaInfo.Add(strArray[0], strArray[1]);
                            if (strArray[0].StartsWith("Judge"))
                            {
                                ListDisplayInfo.Add(new LivParaModel(strArray[0], strArray[1]));
                            }
                        }
                    }
                }

                DisplayInfo = new ObservableCollection<LivParaModel>(ListDisplayInfo);
                if (ListDisplayInfo.Count == 0)
                    MessageBox.Show("文件内无相关数据");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统设置");
            }
        }
        private void ExecuteUpdateCommand(object obj)
        {
            if (obj is LivParaModel livPara)
            {
                DictParaInfo[livPara.ParaName] = livPara.ParaValue;

                using (StreamWriter sw = File.CreateText(FileName))
                {
                    foreach (var item in DictParaInfo)
                    {
                        sw.WriteLine(item.Key + " = "+item.Value);
                    }
                }
                MessageBox.Show("文件数据更新成功", "系统提示");


            }
            else
            {
                MessageBox.Show("输入的参数值格式不正确", "系统提示");
            }
        }
        #endregion
    }
}
