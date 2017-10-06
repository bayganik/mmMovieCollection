using System;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media.Imaging;

namespace mmMovieCollection
{
    public class UriToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BitmapImage image = new BitmapImage();
            if (value != null)
            {
                try
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.UriSource = new Uri((string)value, UriKind.Absolute);
                    image.EndInit();
                }
                catch
                {
                    image = null;
                }
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public class BoolInverterConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

    }

    public class YesNoToBoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString().ToLower() == "yes")
                return true;
            if (value.ToString().ToLower() == "no")
                return false;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value)
                    return "Yes";
                else
                    return "No";
            }
                
            return "";
        }

    }
    public class ConvertVisibilityBool : IValueConverter
    {
        /// <summary>
        /// True Visible 
        /// False – Collapse
        /// False –  Hidden 
        /// </summary>
        public ConvertVisibilityBool() { }

        public bool Collapse { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bValue = (bool)value;
            if (bValue)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return true;
            else
                return false;
        }

    }
}
