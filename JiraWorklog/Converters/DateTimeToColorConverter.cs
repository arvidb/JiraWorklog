using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JiraWorklog.Converters
{
    public class DateTimeToColorConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = Brushes.Crimson;
            if (value != null)
            {
                var date = (DateTime)value;
                var offset = DateTime.Now - date;

                if (offset.Days < 2)
                {
                    color = Brushes.White;
                }
                else if (offset.Days > 4)
                {
                    color = Brushes.Gold;
                }
                else if (offset.Days > 7)
                {
                    color = Brushes.Crimson;
                }
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
