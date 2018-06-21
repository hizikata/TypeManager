using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TypeManager.DAL;
using TypeManager.Model;
using System.Collections.ObjectModel;
using System.Windows;
using TypeManager.Assist;
using TypeManager.View;

namespace TypeManager.ViewModel
{
    public class MaterialRegisterViewModel : ViewModelBase,IGetPrivilege
    {
        #region Fields
        MaterialRegisterService MaterialReSer = new MaterialRegisterService();
        string TableName = "Material_OrderParameter";
        #endregion
        #region Properties
        private List<string> _typeList;

        public List<string> TypeList
        {
            get { return _typeList; }
            set { _typeList = value; }
        }
        private ObservableCollection<MaterialOrderParameter> _displayIfo;

        public ObservableCollection<MaterialOrderParameter> DisplayInfo
        {
            get { return _displayIfo; }
            set { _displayIfo = value; RaisePropertyChanged(() => DisplayInfo); }
        }



        #endregion
        #region Commands
        public RelayCommand<string> Search
        {
            get
            {
                return new RelayCommand<string>((str) => ExecuteSearch(str));
            }
        }
        void ExecuteSearch(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                List<MaterialOrderParameter> list = MaterialReSer.Search(str, TableName);
                if (list.Count == 0)
                {

                    MessageBox.Show("查询结果为空", "错误提示");
                    DisplayInfo = null;
                }
                else
                {
                    DisplayInfo = new ObservableCollection<MaterialOrderParameter>(list);
                }

            }
        }
        public RelayCommand<object> UpdateDB
        {
            get
            {
                return new RelayCommand<object>((obj) => ExecuteUpdateDB(obj));
            }
        }
        void ExecuteUpdateDB(object obj)
        {
            if (obj != null)
            {
                if (obj is MaterialOrderParameter model)
                {
                    int count = MaterialReSer.UpdateDB(model, TableName);
                    if (count == 1)
                        System.Windows.MessageBox.Show("更新数据库成功", "更新提示", System.Windows.MessageBoxButton.OK);
                    else
                        System.Windows.MessageBox.Show("更新失败,更新数量:" + count, "错误提示", System.Windows.MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("转换SelectedItem发生错误，请通知系统管理员", "错误提示");
                //todo
            }
            else
                MessageBox.Show("请选择一项", "提示");
        }
        public RelayCommand<object> InsertDB
        {
            get { return new RelayCommand<object>((obj) => ExecuteInsertDB(obj)); }
        }
        public void ExecuteInsertDB(object obj)
        {
            if (obj != null)
            {
                if(obj is MaterialOrderParameter model)
                {
                    int count = MaterialReSer.InsertDB(model, TableName);
                    if (count == 1)
                    {
                        MessageBox.Show("插入数据库成功", "系统提示");
                    }
                    else
                    {
                        MessageBox.Show("插入数据库失败，数量:" + count, "系统提示");
                    }
                }
                else
                {
                    MessageBox.Show("SelectedItem转换失败，请通知系统管理员", "系统提示");
                }
            }
            else
            {
                MessageBox.Show("请选择一项", "系统提示");
            }
        }
    
        #endregion
        #region Constructor
        public MaterialRegisterViewModel()
        {
            CurrentUser = FrmMain.CurrentUser;
            CurrentPrivilege = GetPrivilege(CurrentUser);
            TypeList = new List<string>()
            {
                "0度滤片",
                "45度滤片",
                "APD",
                "Isolator",
                "LD",
                "PD"
            };
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
    }
}
