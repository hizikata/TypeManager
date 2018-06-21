using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TypeManager.DAL
{
    class ComboBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag=(bool)value;
            switch (flag)
            {
                case true:
                    return 1;
                case false:
                    return 0;
                default:
                    return -1;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i = (int)value;
            switch (i)
            {
                case 0:
                    return false;
                case 1:
                    return false;
                case -1:
                    return null;
                default:
                    throw new Exception();
            }
        }
    }
}
