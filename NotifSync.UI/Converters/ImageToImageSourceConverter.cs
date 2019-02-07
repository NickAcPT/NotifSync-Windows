using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace NotifSync.UI.Converters
{
    public class ImageToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, 
            CultureInfo culture)
        {
            if (!(value is Image image)) return null;
            var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            ms.Seek(0, SeekOrigin.Begin);
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}