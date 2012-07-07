using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace CoApp.VisualStudio.Dialog.PackageManagerUI
{
    public class StringCollectionsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                string stringValue = value as string;

                if (stringValue != null)
                {
                    return stringValue;
                }

                if (value == null)
                {
                    return String.Empty;
                }

                return String.Join(", ", (IEnumerable<string>)value);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
