using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MyControlLib.RatingControl.RatingElement.Converters
{
    public class FillConverter : IValueConverter
    {
        /// <summary>
        /// The data binding engine calls this method when it propagates a value from the binding source to the binding target.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                if ((double)value == 1)
                    return Brushes.Red;
                else
                    return Brushes.Black;
            }
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }

    /// <summary>
    /// Converts geometry figure of RatingElement from point[] to streamgeometry (for path in xaml)
    /// </summary>
    public class PathConverter : IValueConverter
    {
        /// <summary>
        /// The data binding engine calls this method when it propagates a value from the binding source to the binding target.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Point[] points)
            {
                StreamGeometry geometry = new StreamGeometry
                {
                    FillRule = FillRule.Nonzero
                };

                // Open a StreamGeometryContext that can be used to describe this StreamGeometry 
                // object's contents.
                using (StreamGeometryContext ctx = geometry.Open())
                {
                    ctx.BeginFigure(points[0], true, true);

                    for (int i = 1; i < points.Length; i++)
                        ctx.LineTo(points[i], true, true);

                    //// Begin the triangle at the point specified. Notice that the shape is set to 
                    //// be closed so only two lines need to be specified below to make the triangle.
                    //ctx.BeginFigure(new Point(10, 100), true /* is filled */, true /* is closed */);

                    //// Draw a line to the next specified point.
                    //ctx.LineTo(new Point(100, 100), true /* is stroked */, false /* is smooth join */);

                    //// Draw another line to the next specified point.
                    //ctx.LineTo(new Point(100, 50), true /* is stroked */, false /* is smooth join */);
                }

                // Freeze the geometry (make it unmodifiable)
                // for additional performance benefits.
                geometry.Freeze();

                return geometry;
            }
            return "error";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
