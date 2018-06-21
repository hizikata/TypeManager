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
using TypeManager.Assist;
using TypeManager.View;
using System.Windows.Controls;
using TypeManager.DAL;

namespace TypeManager.ViewModel
{
    public class LivParaSetViewModel : ViewModelBase
    {
        #region Fields
        int count = 0;
        string CurrentFileName;
        List<string> ListDefault;
        readonly string LivPath = @"\\192.168.0.237\TestParameter\LIV_database\setup";

        #endregion
        #region Properties
        List<string> _fileNames;
        /// <summary>
        /// Liv文件参数文件集
        /// </summary>
        public List<string> FileNames
        {
            get { return _fileNames; }
            set { _fileNames = value; RaisePropertyChanged(() => FileNames); }
        }
        public List<LivParaModel> ListDisplayInfo { get; set; }
        public Dictionary<string, string> DictProductType { get; set; }
        private ObservableCollection<LivParaModel> _displayInfo;

        public ObservableCollection<LivParaModel> DisplayInfo
        {
            get { return _displayInfo; }
            set { _displayInfo = value; RaisePropertyChanged(() => DisplayInfo); }
        }
        public Dictionary<string, string> DictParaInfo { get; set; }
        string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; RaisePropertyChanged(() => FileName); }
        }

        #endregion

        #region Constructors
        public LivParaSetViewModel()
        {
            CurrentUser = FrmMain.CurrentUser;
            CurrentPrivilege = GetPrivilege(CurrentUser);
            FileNames = new List<string>();
            Initialize();
        }
        #endregion
        #region Methods
        private void Initialize()
        {
            try
            {
                if (Directory.Exists(LivPath))
                {
                    //共享文件夹的目录
                    //获取目录下的全部文件名
                    ListDefault = new List<string>();
                    string[] fileNames = Directory.GetFiles(LivPath);
                    foreach (var item in fileNames)
                    {
                        if (item.EndsWith(".scg"))
                        {
                            ListDefault.Add(item);
                        }
                    }
                    FileNames = ListDefault;
                }
                else
                {
                    MessageBox.Show("连接Liv远程文件夹失败！", "系统提示");
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
        public RelayCommand<object> ParaSearchCommand
        {
            get
            {
                return new RelayCommand<object>(obj => ExecuteParaSearchCommand(obj));
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
                CurrentFileName = obj.ToString();
                int location = CurrentFileName.LastIndexOf('\\');
                FileName = CurrentFileName.Substring(location + 1);
                FileInfo fileInfo = new FileInfo(CurrentFileName);
                using (StreamReader sr = fileInfo.OpenText())
                {
                    string[] strArray = null;
                    string output = null;
                    while ((output = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(output.Trim()))
                        {
                            if (output.Contains("="))
                            {
                                strArray = output.Split('=');
                                DictParaInfo.Add(strArray[0].Trim(), strArray[1].Trim());
                                //显示所有参数 180612
                                //if (strArray[0].StartsWith("Judge"))
                                //{
                                ListDisplayInfo.Add(new LivParaModel(strArray[0], strArray[1]));
                                //}
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
                DictParaInfo[livPara.ParaName.Trim()] = livPara.ParaValue.Trim() ;

                using (StreamWriter sw = File.CreateText(CurrentFileName))
                {
                    foreach (var item in DictParaInfo)
                    {
                        sw.WriteLine(item.Key + " = " + item.Value);
                    }
                }
                MessageBox.Show("文件数据更新成功", "系统提示");


            }
            else
            {
                MessageBox.Show("输入的参数值格式不正确", "系统提示");
            }
        }
        private void ExecuteParaSearchCommand(object obj)
        {
            string searchStr = obj.ToString().Trim();
            if (string.IsNullOrEmpty(searchStr))
            {
                DisplayInfo = new ObservableCollection<LivParaModel>(ListDisplayInfo);
            }
            else
            {
                List<LivParaModel> listNew = new List<LivParaModel>();
                foreach (var item in ListDisplayInfo)
                {
                    if (item.ParaName.Contains(searchStr))
                    {
                        listNew.Add(item);
                    }
                }
                DisplayInfo = new ObservableCollection<LivParaModel>(listNew);
            }
            
        }
        #endregion
        #region Interface Related
        /// <summary>
        /// 获取当前用户权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserPrivilege GetPrivilege(User user)
        {
            if (user.RoleName == "admin")
                return UserPrivilege.admin;
            else if (user.GroupName == "开发部")
                return UserPrivilege.developer;
            else
                return UserPrivilege.others;
        }
        UserPrivilege _currentPrivilege;
        public UserPrivilege CurrentPrivilege
        {
            get { return _currentPrivilege; }
            set { _currentPrivilege = value; }
        }
        readonly User CurrentUser;
        #endregion

        #region ComboBox Command
        public RelayCommand<object> CmbDropDown
        {
            get { return new RelayCommand<object>((obj) => ExecuteCmbDropDown(obj)); }
        }
        public void ExecuteCmbDropDown(object obj)
        {
            if (obj is ComboBox comboBox)
            {
                int countNew = comboBox.Text.Length;
                if (countNew != count)
                {
                    FileNames = CommonMethods.ListFilter(ListDefault, comboBox.Text.Trim());
                    comboBox.IsDropDownOpen = true;
                    count = countNew;
                }
                else
                    return;
            }
        }
        public RelayCommand<object> GotFocusCommand
        {
            get { return new RelayCommand<object>((obj) => ExecuteGotFocusCommand(obj)); }
        }
        public void ExecuteGotFocusCommand(object obj)
        {
            if (obj is ComboBox comboBox)
            {
                comboBox.IsDropDownOpen = true;
            }
        }
        #endregion
    }
}
