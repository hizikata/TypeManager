using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using TypeManager.View.ParaModify;
using Xceed.Wpf.AvalonDock.Layout;
using System.Collections.ObjectModel;
using TypeManager.View.LivAndSen;
using TypeManager.CommonClass;

namespace TypeManager.View
{
    /// <summary>
    /// FrmMain.xaml 的交互逻辑
    /// </summary>
    public partial class FrmMain : Window
    {
        public FrmMain()
        {
            InitializeComponent();
            RegisterMethods();
        }
        public void RegisterMethods()
        {
            #region TypeParaModify  参数修改
            Messenger.Default.Register<object>(this, "TypePara", (obj) => ShowUserControl("型号参数", new TypeParaControl()));
            Messenger.Default.Register<object>(this, "StationSet", (obj) => ShowUserControl("站别设置", new StationSetControl()));
            Messenger.Default.Register<object>(this, "CommonPara", (obj) => ShowUserControl("通用参数", new CommonParaControl()));
            Messenger.Default.Register<object>(this, "ThreeInOnePara", (obj) => ShowUserControl("三合一参数", new ThreeInOneParaControl()));
            Messenger.Default.Register<object>(this, "LabelSet", (obj) => ShowUserControl("标签设置", new LabelSetControl()));
            Messenger.Default.Register<object>(this, "ReportExport", (obj) => ShowUserControl("测报导出", new ReportExport()));
            Messenger.Default.Register<object>(this, "MaterialSet", (obj) => ShowUserControl("物料设置", new MaterialSetControl()));
            Messenger.Default.Register<object>(this, "MaterialRegister", (obj) => ShowUserControl("物料注册", new MaterialRegister()));
            #endregion
            #region Liv&Sen 参数修改
            Messenger.Default.Register<object>(this, "LivParaSet", (obj) => ShowUserControl("Liv参数设置", new LivParaSet()));
            Messenger.Default.Register<object>(this, "SenParaSet", obj => ShowUserControl("Sen参数设置", new SenParaSet()));
            #endregion

        }
        public void ShowUserControl(string title, UserControl userControl)
        {
            LayoutDocumentPane DocumentPane = (LayoutDocumentPane)dockManager.Layout.Descendents().OfType<ILayoutDocumentPane>().FirstOrDefault();
            if (DocumentPane != null)
            {
                ObservableCollection<LayoutContent> documents = DocumentPane.Children;
                if (documents.Count > 0)
                {           
                    foreach (var item in documents)
                    {
                        if (item.Title == title)
                        {
                            ((LayoutDocument)item).IsSelected = true;
                            return;
                        }
                    }
                    LayoutDocument document = new LayoutDocument();
                    document.Content = userControl;
                    document.Title = title;
                    DocumentPane.Children.Add(document);
                    document.IsSelected = true;
                }
                else
                {
                    LayoutDocument document = new LayoutDocument();
                    document.Content = userControl;
                    document.Title = title;
                    DocumentPane.Children.Add(document);
                    document.IsSelected = true;
                }
                
            }
            else
            {
                MessageBox.Show("不存在DocumentPane","系统提示");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            IPCConnection.ConnectState("net use * /del /y");
        }
    }
}
