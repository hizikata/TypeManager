using System;
using System.Windows;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using TypeManager.Assist;

namespace TypeManager.DAL
{
    public class PrivilegeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UserPrivilege user)
            {
                if (user == UserPrivilege.admin)
                    return Visibility.Visible;
                else
                {
                    return Visibility.Hidden;
                }
            }
            else
            {
                throw new Exception("转换错误");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values[0].ToString().Trim()).Contains("RD-TEST"))
            {
                return Visibility.Visible;
            }
            else
            {
                if (values[1] is UserPrivilege user)
                {
                    if (user == UserPrivilege.admin)
                        return Visibility.Visible;
                    else
                        return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MaterialRegisterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Hidden;
            else if (value is UserPrivilege user)
            {
                if (user == UserPrivilege.admin)
                    return Visibility.Visible;
                else
                {
                    return Visibility.Hidden;
                }
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
