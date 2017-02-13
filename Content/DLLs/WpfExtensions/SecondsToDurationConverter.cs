using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace Songhay.ValueConverters
{
    /// <summary>
    /// Converts <see cref="System.Double"/> seconds
    /// to <see cref="System.Windows.Duration"/> data.
    /// </summary>
    /// <remarks>
    ///     This Converter is based on the one from Kenny Young,
    ///     Expression Blend Architect
    /// </remarks>
    public sealed class SecondsToDurationConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Seconds to duration converter: convert.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Duration(TimeSpan.FromSeconds((double)value));
        }

        /// <summary>
        /// Seconds to duration converter: convert back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}