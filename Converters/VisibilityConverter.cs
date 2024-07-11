﻿using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace BudgetWatcher.Converters
{
    public class VisibilityConverter : IValueConverter
    {

        // methods
        #region methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue && boolValue ? Visibility.Visible : Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion methods


    }
}
// EOF