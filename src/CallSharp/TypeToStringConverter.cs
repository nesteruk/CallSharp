using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace CallSharp
{
  public class TypeToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return ((Type) value).GetFriendlyName();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}