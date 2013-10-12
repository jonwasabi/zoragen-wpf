﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace Zyrenth.OracleHack.Wpf
{
    [ContentProperty("Converter")]
    public class MultiValueConverterAdapter : IMultiValueConverter
    {
        public IValueConverter Converter { get; set; }

        #region IMultiValueConverter Members

        private object lastParameter;
        private IValueConverter lastConverter;

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            lastConverter = Converter;
            if (values.Length > 1) lastParameter = values[1];
            if (values.Length > 2) lastConverter = (IValueConverter)values[2];
            if (Converter == null) return values[0];
            return Converter.Convert(values[0], targetType, lastParameter, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
			if (lastConverter == null) return new object[] { value }; // Required for VS design-time
            return new object[] { lastConverter.ConvertBack(value, targetTypes[0], lastParameter, culture) };
        }

        #endregion

    }
}
