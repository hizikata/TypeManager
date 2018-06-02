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

namespace TypeManager.View.ParaModify
{
    /// <summary>
    /// TestControl.xaml 的交互逻辑
    /// </summary>
    public partial class TestControl : UserControl
    {
        List<string> List = new List<string>()
        {
            "harry","lucy","porl","neo","lili","ketty"
        };
        public TestControl()
        {
            InitializeComponent();
            this.cmbTest.ItemsSource = List;
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessageBox.Show("changed");
        }
    }
}
