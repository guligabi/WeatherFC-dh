using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WeatherFC.Converters
{
    class AnimationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri path = null;
            switch (value.ToString())
            {
                case "clear-day":
                    path = new Uri("pack://siteoforigin:,,,/Resources/sunny_anim.gif");
                    break;
                case "clear-night":
                    path = new Uri("pack://siteoforigin:,,,/Resources/night_anim.gif");
                    break;
                case "rain":
                    path = new Uri("pack://siteoforigin:,,,/Resources/rain_anim.gif");
                    break;
                case "snow":
                    path = new Uri("pack://siteoforigin:,,,/Resources/snow_anim.gif");
                    break;
                case "sleet":
                    path = new Uri("pack://siteoforigin:,,,/Resources/rain_anim.gif");
                    break;
                case "wind":
                    path = new Uri("pack://siteoforigin:,,,/Resources/windy_anim.gif");
                    break;
                case "fog":
                    path = new Uri("pack://siteoforigin:,,,/Resources/fog_anim.gif");
                    break;
                case "cloudy":
                    path = new Uri("pack://siteoforigin:,,,/Resources/cloudy_anim.gif");
                    break;
                case "partly-cloudy-day":
                    path = new Uri("pack://siteoforigin:,,,/Resources/lightcloud_anim.gif");
                    break;
                case "partly-cloudy-night":
                    path = new Uri("pack://siteoforigin:,,,/Resources/cloudynight_anim.gif");
                    break;
                default:
                    path = new Uri("pack://siteoforigin:,,,/Resources/sunny_anim.gif");
                    break;
            }
            BitmapImage image = new BitmapImage(path); 
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
