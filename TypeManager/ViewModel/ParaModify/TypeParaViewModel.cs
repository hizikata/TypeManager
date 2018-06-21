using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using TypeManager.DAL;
using XuxzLib;
using TypeManager.Model;
using GalaSoft.MvvmLight.Command;
using System.IO;
using System.Windows;
using TypeManager.View.Children;
using System.Windows.Controls;
using TypeManager.Assist;
using TypeManager.View;

namespace TypeManager.ViewModel
{

    public class TypeParaViewModel : ViewModelBase
    {
        public List<string> ListDefault;
        Repository Repository = new Repository();
        public string TableName = "Para_ProductType";
        string FilePath = Environment.CurrentDirectory + "\\ProductTypeList.txt";
        public TypeParaViewModel()
        {
            //InitialTypeParaList(Repository);
            CurrentUser = FrmMain.CurrentUser;
            CurrentPrivilege = GetPrivilege(CurrentUser);
            InitialProductTypeSource();
        }
        #region Command
        public RelayCommand<string> Search
        {
            get
            {
                return new RelayCommand<string>((str) => ExecuteSearch(str));
            }
        }
        public void ExecuteSearch(string str)
        {
            if (!string.IsNullOrEmpty(str))
                TypeParaList = new ObservableCollection<TypeParaModel>(Repository.ListFromDatabase(TableName, str));
        }
        private List<string> _productTypeSource;

        public List<string> ProductTypeSource
        {
            get { return _productTypeSource; }
            set
            {
                _productTypeSource = value;
                RaisePropertyChanged(() => ProductTypeSource);
            }
        }
        public RelayCommand<object> UpdateDB
        {
            get
            {
                return new RelayCommand<object>((obj) => ExecuteUpdateDB(obj));
            }
        }
        private void ExecuteUpdateDB(object obj)
        {
            if (obj != null)
            {
                if (obj is TypeParaModel model)
                {
                    int count = Repository.UpdateDB(model, TableName);
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
            //Repository.SqlBulkCopyInsert(typeParaList.ToList<TypeParaModel>(), TableName);
            //System.Windows.MessageBox.Show("更新数据库成功", "成功提示",System.Windows.MessageBoxButton.OK);
        }
        #endregion

        private ObservableCollection<TypeParaModel> typeParaList;
        public ObservableCollection<TypeParaModel> TypeParaList
        {
            get
            {
                return typeParaList;
            }
            set
            {
                typeParaList = value;
                RaisePropertyChanged(() => TypeParaList);
            }
        }
        /// <summary>
        /// 打开型号参数界面是自动搜索前20项
        /// </summary>
        /// <param name="repository"></param>
        public void InitialTypeParaList(Repository repository)
        {
            List<TypeParaModel> list = repository.InitialTypePara(TableName);
            TypeParaList = new ObservableCollection<TypeParaModel>(list);
        }
        /// <summary>
        /// 初始化cmbbox下拉框
        /// </summary>
        public void InitialProductTypeSource()
        {
            //if (!File.Exists(FilePath))
            //    throw new Exception("型号列表文件不存在");

            WriteToText(FilePath);
            ListDefault = ReadFromText(FilePath);
            ProductTypeSource = ListDefault;
        }
        public void WriteToText(string filePath)
        {
            List<string> list = Repository.GetTypeList();
            using (StreamWriter sw = File.CreateText(filePath))
            {
                foreach (string item in list)
                {
                    sw.WriteLine(item);
                }
            }
        }
        public List<string> ReadFromText(string filePath)
        {
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
        #region InsertDB
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
            bool IsTypeExist = false;
            foreach (string item in ProductTypeSource)
            {
                if (item == str.Trim())
                    IsTypeExist = true;
            }
            if (IsTypeExist == true)
            {
                MessageBoxResult result = MessageBox.Show("数据库已存在该型号数据，是否重复添加？", "系统提示", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                    return;
            }
            if (typeParaList == null)
            {
                MessageBoxResult result = MessageBox.Show("当前列表数据为空，新型号所有数据需要手动输入，是否确认", "系统提示", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    TypePara typePara = new TypePara();
                    typePara.lbNewType.Content = str;
                    typePara.ShowDialog();
                }
                else
                {
                    return;
                }

            }
            else
            {
                foreach (TypeParaModel item in typeParaList)
                {
                    item.ProductTypeName = str;
                }
                TypePara typePara = new TypePara();
                typePara.dgInfo.ItemsSource = typeParaList;
                typePara.lbNewType.Content = str;
                typePara.ShowDialog();
                //窗口关闭后刷新combobox下拉菜单
                this.WriteToText(FilePath);
                ProductTypeSource = this.ReadFromText(FilePath);
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
            CommonMethods.SqlBulkCopyInsert<TypeParaModel>(TypeParaList.ToList<TypeParaModel>(), TableName);
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
                if (comboBox.IsMouseCaptured)
                    comboBox.IsDropDownOpen = true;

                ProductTypeSource = CommonMethods.ListFilter(ListDefault, comboBox.Text.Trim());
                comboBox.IsDropDownOpen = true;
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
    }
}
