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

namespace TypeManager.ViewModel
{
    public class SenParaSetViewModel : ViewModelBase
    {
        #region Fields
        readonly string Path = @"\\192.168.0.237\TestParameter\RDTest";
        readonly string Pwd = "eic2018";
        readonly string UserName = @"ms003\Admin";
        readonly string FileName = "RDTest_SEN.xml";
        IEnumerable<XElement> Elements;
        #endregion
        #region Properties
        public ProductType ProductType { get; set; }
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
        #endregion Commands
        #region CommandMethods
        private void ExecuteLoadCommand(object obj)
        {
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
                        list.Add(new SenParaBase(paraName,enable, max, min));
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
        #endregion
        #region Methods
        private void Initialize()
        {
            bool connState = false;
            string connString = "net use " + Path + " " + Pwd + " /user:" + UserName;
            try
            {
                connState = IPCConnection.ConnectState(connString);
                if (connState == true)
                {
                    string fileFullPath = Path + @"\" + FileName;
                    if (File.Exists(fileFullPath))
                    {
                        XDocument xDoc;
                        xDoc = XDocument.Load(fileFullPath);
                        Elements = xDoc.Root.DescendantsAndSelf("ProductType");
                        List<string> listType = new List<string>();
                        foreach (var item in Elements)
                        {
                            listType.Add(item.Attribute("name").Value);
                        }
                        ListProduct = listType;

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


    }
}
