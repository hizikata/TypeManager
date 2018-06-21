using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TypeManager.CommonClass;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using TypeManager.Model.SenPara;
using TypeManager.Assist;
using TypeManager.Model;
using TypeManager.View;
using System.Windows.Controls;
using TypeManager.DAL;

namespace TypeManager.ViewModel
{
    public class SenParaSetViewModel : ViewModelBase,IGetPrivilege
    {
        #region Fields
        int count = 0;
        List<string> ListDefault;
        readonly string SenFilePath = @"\\192.168.0.237\TestParameter\ParameterFile\Sen ParameterFile\SenParameterSet.xml";
        IEnumerable<XElement> Elements;
        XDocument xDoc;
        #endregion
        #region Properties
        private string _currentType;

        public string CurrentType
        {
            get { return _currentType; }
            set { _currentType = value; RaisePropertyChanged(() => CurrentType); }
        }

        public List<string> _listProduct;
        public List<string> ListProduct
        {
            get { return _listProduct; }
            set
            {
                _listProduct = value;
                RaisePropertyChanged(() => ListProduct);
            }
        }
        private List<SenParaBase> _listSenPara;

        public List<SenParaBase> ListSenPara
        {
            get { return _listSenPara; }
            set { _listSenPara = value; RaisePropertyChanged(() => ListSenPara); }
        }

        #endregion
        #region Constructors
        public SenParaSetViewModel()
        {
            CurrentUser = FrmMain.CurrentUser;
            CurrentPrivilege = GetPrivilege(CurrentUser);
            Initialize();
            _listSenPara = new List<SenParaBase>();
        }

        #endregion Constructors
        #region Commands  
        public RelayCommand<object> LoadCommand
        {
            get
            {
                return new RelayCommand<object>(obj => ExecuteLoadCommand(obj));
            }
        }
        public RelayCommand<object> UpdateCommand
        {
            get
            {
                return new RelayCommand<object>(obj => ExecuteUpdateCommand(obj));
            }
        }
        #endregion Commands
        #region CommandMethods
        private void ExecuteLoadCommand(object obj)
        {
            CurrentType = obj.ToString().Trim();
            try
            {
                XElement RDTest = (from elem in Elements
                                   where elem.Attribute("name").Value == obj.ToString().Trim()
                                   select elem).FirstOrDefault();
                if (RDTest != null)
                {
                    List<SenParaBase> list = new List<SenParaBase>();
                    IEnumerable<XElement> elements = from ele in RDTest.Elements() select ele;
                    foreach (var item in elements)
                    {
                        string paraName = item.Name.ToString().Trim();
                        bool enable = Convert.ToBoolean(item.Attribute("EnableTest").Value.Trim());
                        float max = Convert.ToSingle(item.Attribute("Max").Value.Trim());
                        float min = Convert.ToSingle(item.Attribute("Min").Value.Trim());
                        list.Add(new SenParaBase(paraName, enable, max, min));
                    }
                    ListSenPara = list;
                }
                else
                {
                    MessageBox.Show("无法找到该型号", "系统提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统设置");
            }
        }
        void ExecuteUpdateCommand(object obj)
        {
            try
            {
                if (obj is SenParaBase senPara)
                {
                    string type = this.CurrentType;
                    XElement xElement = (from ele in Elements
                                         where ele.Attribute("name").Value == CurrentType
                                         select ele).FirstOrDefault();
                    //获取对应属性所在的子元素
                    XElement element = xElement.Element(senPara.ParaName);
                    element.Attribute("EnableTest").SetValue(senPara.EnableTest);
                    element.Attribute("Max").SetValue(senPara.Max);
                    element.Attribute("Min").SetValue(senPara.Min);
                    xDoc.Save(SenFilePath);
                    MessageBox.Show("参数更新成功", "系统提示");

                }
                else
                {
                    throw new Exception("无法转换为SenParaBase");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Methods
        private void Initialize()
        {
            try
            {
                if (File.Exists(SenFilePath))
                {
                    if (File.Exists(SenFilePath))
                    {
                        
                        xDoc = XDocument.Load(SenFilePath);
                        Elements = xDoc.Root.DescendantsAndSelf("ProductType");
                        List<string> listType = new List<string>();
                        foreach (var item in Elements)
                        {
                            listType.Add(item.Attribute("name").Value);
                        }
                        ListDefault = listType;
                        ListProduct = ListDefault;

                    }
                    else
                    {
                        MessageBox.Show("文件不存在", "系统设置");
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "系统设置");
            }
        }
        #endregion Methods

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
                    ListProduct = CommonMethods.ListFilter(ListDefault, comboBox.Text.Trim());
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
