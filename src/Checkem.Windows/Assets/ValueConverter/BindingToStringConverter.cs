﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Checkem.Assets.ValueConverter
{
    public class BindingToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}