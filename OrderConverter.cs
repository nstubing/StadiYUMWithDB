using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DocumentDBTodo
{
    public class OrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime thisTime = DateTime.Parse(value.ToString());
            DateTime DefaultTime = new DateTime();
            if (value == null)
            {
                return "Received";
            }
            else if (thisTime == DefaultTime)
            {
                return "Received";
            }
            else if (DateTime.Now > thisTime.AddMinutes(5))
            {
                return "Completed";
            }
            else
            {
                return "On its way!";
            }
            

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
