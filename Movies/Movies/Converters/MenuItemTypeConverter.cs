using Movies.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Movies.Converters
{
    public class MenuItemTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menuItemType = (MenuItemType)value;

            switch (menuItemType)
            {
                case MenuItemType.Discover:
                    if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                        return "movies_discover";
                    else
                        return string.Empty;
                case MenuItemType.Upcoming:
                    
                    if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                        return "movies_upcoming";
                    else
                        return string.Empty;
                case MenuItemType.Action:
                    if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                        return "movies_movie";
                    else
                        return string.Empty;
                case MenuItemType.Animation:
                    if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                        return "movies_movie";
                    else
                        return string.Empty;
                case MenuItemType.Adventure:
                    if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                        return "movies_movie";
                    else
                        return string.Empty;
                case MenuItemType.Comedy:
                    if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                        return "movies_movie";
                    else
                        return string.Empty;
                case MenuItemType.Horror:
                    if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                        return "movies_movie";
                    else
                        return string.Empty;
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}