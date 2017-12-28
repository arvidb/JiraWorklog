using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JiraWorklog.Converters
{
    public class HoursToSecondsConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (ulong)value / 3600.0f;

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dtValue = (string)value;
            if (double.TryParse(dtValue, out var val))
            {
                return (ulong)(val * 3600.0f);
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
