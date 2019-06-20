using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace App1.Converters
{
    class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(string.IsNullOrEmpty(value.ToString())))
            {
                byte[] Base64Stream = System.Convert.FromBase64String(value.ToString());

                return ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            }
            return null;
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
