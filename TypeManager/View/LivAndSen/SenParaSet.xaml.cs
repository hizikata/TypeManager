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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TypeManager.View.LivAndSen
{
    /// <summary>
    /// SenParaSet.xaml 的交互逻辑
    /// </summary>
    public partial class SenParaSet : UserControl
    {
        public string[] StrBoolean =new string[2] { "False", "True" };
        public SenParaSet()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.cmbBoolean.ItemsSource = StrBoolean;
        }
    }
}
