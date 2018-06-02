using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using System.Xml;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace TypeManager.ViewModel
{
    public class TestViewModel : ViewModelBase
    {
        public RelayCommand<object> TreeViewItemSelectedCommand
        {
            get
            {
                return new RelayCommand<object>((obj) => ExecuteTreeViewItemSelectedCommand(obj));
            }
        }
        public void ExecuteTreeViewItemSelectedCommand(object obj)
        {
            if (obj is XmlElement element)
            {
                if (element.Name == "MenuItem")
                    Messenger.Default.Send<object>("", element.Attributes["tag"].Value);
                else
                    return;

            }
            else
                MessageBox.Show("发生错误", "系统提示");
        }
    }
}
