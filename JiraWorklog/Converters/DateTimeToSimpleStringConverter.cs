using JiraWorklog.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JiraWorklog.Converters
{
    /// <summary>
    /// Converts a Date and Time into a string for display.
    /// </summary>
    public class DateTimeToSimpleStringConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "Never";
            }

            var dtValue = (DateTime)value;

            return dtValue.ToTimeSinceString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
