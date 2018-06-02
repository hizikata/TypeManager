using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media;

namespace TypeManager.CommonClass
{
    public class BackgroundConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListViewItem listViewItem = (ListViewItem)value;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(listViewItem) as ListView;
            if (listView != null)
            {
                int index = listView.ItemContainerGenerator.IndexFromContainer(listViewItem);
                if (index % 2 == 0)
                    return Brushes.LightSkyBlue;
                else
                    return Brushes.White;
            }
            else
            {
                //System.Windows.MessageBox.Show("listView背景转换失败");
                return Brushes.White;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
