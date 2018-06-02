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
using TypeManager.View.Children;
using System.Windows.Controls;

namespace TypeManager.ViewModel
{
    public class ThreeInOneParaViewModel:ViewModelBase
    {
        #region Fields
        int count = 0;
        List<string> ListDefault;
        ThreeInOneService ThreeInOneSer = new ThreeInOneService();
        string TableName = "Config_CommonParaSet";
        #endregion
        #region Properties
        private List<string> _typeList;

        public List<string> TypeList
        {
            get { return _typeList; }
            set { _typeList = value;RaisePropertyChanged(() => TypeList); }
        }
        private ObservableCollection<CommonParaSet> _displayIfo;

        public ObservableCollection<CommonParaSet> DisplayInfo
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
                List<CommonParaSet> list = ThreeInOneSer.Search(str, TableName);
                if (list.Count == 0)
                {
                    
                    MessageBox.Show("查询无结果", "系统提示");
                    DisplayInfo = null;
                }
                else
                {
                    DisplayInfo = new ObservableCollection<CommonParaSet>(list);
                }
            }
            else
                MessageBox.Show("请选择产品型号", "错误提示");
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
                if (obj is CommonParaSet model)
                {
                    int count = ThreeInOneSer.UpdateDB(model, TableName);
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
        public ThreeInOneParaViewModel()
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
            
            if (string.IsNullOrEmpty(str.Trim()))
            {
                MessageBox.Show("请输入新型号名称", "错误提示");
                return;
            }
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
            bool IsTypeExist = false;
            List<string> list = ThreeInOneSer.GetTypeList(TableName);
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

            if (DisplayInfo == null)
            {
                MessageBoxResult result = MessageBox.Show("当前列表数据为空，新型号所有数据需要手动输入，是否确认", "系统提示", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    ThreeInOne window = new ThreeInOne();
                    window.tbNewType.Text = str.Trim();
                    window.ShowDialog();
                }
                else
                {
                    return;
                }

            }
            else
            {
                foreach (var item in DisplayInfo)
                {
                    item.ProductType = str;
                }
                ThreeInOne window = new ThreeInOne();
                window.tbNewType.Text = str.Trim();
                window.lViewInfo.ItemsSource = DisplayInfo;
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
            CommonMethods.SqlBulkCopyInsert<CommonParaSet>(DisplayInfo.ToList<CommonParaSet>(), TableName);
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
