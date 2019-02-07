using System;
using System.Windows.Data;
using System.Windows.Media;

namespace NotifSync.UI.Converters
{
    public class ColorToWpfColorConverter : IValueConverter
    {
        public static Color DefaultColor { get; set; } = Color.FromRgb(115, 115, 115);

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            switch (value)
            {
                case null:
                    return null;
                // For a more sophisticated converter, check also the targetType and react accordingly..
                case Color color:
                    return color;
                case System.Drawing.Color color:
                    if (targetType == typeof(Color))
                        return ToColorFromDrawing(color);
                    else
                        return new SolidColorBrush(ToColorFromDrawing(color));
                default:
                {
                    // You can support here more source types if you wish
                    // For the example I throw an exception

                    Type type = value.GetType();
                    throw new InvalidOperationException("Unsupported type ["+type.Name+"]");
                }
            }
        }

        private static Color ToColorFromDrawing(System.Drawing.Color color)
        {
            if (color == System.Drawing.Color.Transparent || color.IsEmpty || color.A == 0)
                return Color.FromArgb(DefaultColor.A, DefaultColor.R, DefaultColor.G, DefaultColor.B);
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

}