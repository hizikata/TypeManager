using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TypeManager.Model;
using System.Collections.ObjectModel;
using TypeManager.DAL;
using System.Windows;
using System.IO;
using TypeManager.View.Children;
using System.Windows.Controls;

namespace TypeManager.ViewModel
{
    public class StationSetViewModel : ViewModelBase
    {
        #region Fields 
        int count = 0;
        List<string> ListDefault;
        string TableName = "Para_ProductStation";
        StationSetService StationSetSer = new StationSetService();
        #endregion
        #region Properties
        private ObservableCollection<ProductStation> _displayInfo;

        public ObservableCollection<ProductStation> DisplayInfo
        {
            get { return _displayInfo; }
            set
            {
                _displayInfo = value;
                RaisePropertyChanged(() => DisplayInfo);
            }
        }
        private List<string> _typeList;

        public List<string> TypeList
        {
            get { return _typeList; }
            set { _typeList = value;RaisePropertyChanged(() => TypeList); }
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
        public void ExecuteSearch(string productType)
        {
            if (!string.IsNullOrEmpty(productType))
            {
                List<ProductStation> list = StationSetSer.Search(productType, TableName);
                DisplayInfo = new ObservableCollection<ProductStation>(list);
            }
            else
                System.Windows.MessageBox.Show("产品型号不能为空", "错误提示");
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
                if (obj is ProductStation model)
                {
                    int count = StationSetSer.UpdateDB(model, TableName);
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
        #endregion
        #region Constructor
        public StationSetViewModel()
        {
            ListDefault = CommonMethods.ReadFromFile();
            TypeList = ListDefault;
        }
        #endregion
        #region InsertDB Area
        public RelayCommand<string> ShowInsertDBWindow
        {
            get
            {
                return new RelayCommand<string>((str) => ExecuteShowInsertDBWindow(str));
            }
        }
        /// <summary>
        /// 弹出“插入新型号”窗口
        /// </summary>
        /// <param name="str"></param>
        public void ExecuteShowInsertDBWindow(string str)
        {
            //验证新型号填写是否为空
            if (string.IsNullOrEmpty(str.Trim()))
            {
                MessageBox.Show("请输入新型号名称", "错误提示");
                return;
            }
            //验证已在“型号参数”选项卡中添加对应新型号
            bool flag = false;
            foreach (string item in ListDefault)
            {
                if (item == str.Trim())
                    flag = true;
            }
            if (flag == false)
            {
                MessageBox.Show("请先在“型号参数”选项卡添加对应型号！", "系统提示");
                return;
            }
            //验证数据库中是否已存在新型号数据
            bool IsTypeExist = false;
            List<string> list = StationSetSer.GetTypeList(TableName);
            foreach (string item in list)
            {
                if (item == str.Trim())
                    IsTypeExist = true;
            }
            if (IsTypeExist == true)
            {
                MessageBoxResult result = MessageBox.Show("数据库已存在该型号数据，是否重复写入？", "系统提示",MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                    return;
            }
            //
            if (DisplayInfo == null)
            {
                MessageBoxResult result = MessageBox.Show("当前列表数据为空，新型号所有数据需要手动输入，是否确认", "系统提示", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    StationSet window = new StationSet();
                    window.tbNewType.Text = str;
                    window.ShowDialog();
                }
                else
                {
                    return;
                }

            }
            else
            {
                foreach (ProductStation item in DisplayInfo)
                {
                    item.ProductType = str;
                }
                StationSet window = new StationSet();
                window.tbNewType.Text = str;
                window.lviewInfo.ItemsSource = DisplayInfo;
                window.ShowDialog();
            }

        }
        /// <summary>
        /// 插入数据库命令执行的方法
        /// </summary>
        public RelayCommand InsertDB
        {
            get { return new RelayCommand(() => ExecuteInsertDB()); }
        }
        public void ExecuteInsertDB()
        {
            List<ProductStation> list = DisplayInfo.ToList<ProductStation>();
            CommonMethods.SqlBulkCopyInsert<ProductStation>(list, TableName);
            MessageBox.Show("数据插入成功", "系统提示");
        }
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
                    TypeList = CommonMethods.ListFilter(ListDefault, comboBox.Text.Trim());
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
