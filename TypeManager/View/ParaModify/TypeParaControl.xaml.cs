using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using TypeManager.DAL;
using TypeManager.ViewModel;
using TypeManager.Model;


namespace TypeManager.View.ParaModify
{
    /// <summary>
    /// TypeParaControl.xaml 的交互逻辑
    /// </summary>
    public partial class TypeParaControl : UserControl
    {
        TypeParaViewModel TypeParaVM = new TypeParaViewModel();
        public TypeParaControl()
        {
            InitializeComponent();           
            DataContext = TypeParaVM;
        }

        private void cmbProductType_TextChanged(object sender, TextChangedEventArgs e)
        {
            TypeParaVM.ProductTypeSource = CommonMethods.ListFilter(TypeParaVM.ListDefault, cmbProductType.Text.Trim());
            cmbProductType.IsDropDownOpen = true;
        }

        private void cmbProductType_GotFocus(object sender, RoutedEventArgs e)
        {
            cmbProductType.IsDropDownOpen = true;
        }
    }
}
