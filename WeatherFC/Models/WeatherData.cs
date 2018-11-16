using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WeatherFC.Models
{
    public class WeatherData : INotifyPropertyChanged
    {
        double _latitude;
        double _longitude;
        string _timezone;
        Currently _currently;
        Minutely _minutely;
        Hourly _hourly;
        Daily _daily;
        Flags _flags;
        int _offset;

        public double latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                OnPropertyChange("latitude");

            }
        }
        public double longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                OnPropertyChange("longitude");
            }
        }
        public string timezone
        {
            get
            {
                return _timezone;
            }
            set
            {
                _timezone = value;
                OnPropertyChange("timezone");
            }
        }
        public Currently currently
        {
            get
            {
                return _currently;
            }
            set
            {
                _currently = value;
                OnPropertyChange("currently");
            }
        }
        public Minutely minutely
        {
            get
            {
                return _minutely;
            }
            set
            {
                _minutely = value;
                OnPropertyChange("minutely");
            }
        }
        public Hourly hourly
        {
            get
            {
                return _hourly;
            }
            set
            {
                _hourly = value;
                OnPropertyChange("hourly");
            }
        }
        public Daily daily
        {
            get
            {
                return _daily;
            }
            set
            {
                _daily = value;
                OnPropertyChange("daily");
            }
        }
        public Flags flags
        {
            get
            {
                return _flags;
            }
            set
            {
                _flags = value;
                OnPropertyChange("flags");
            }
        }
        public int offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
                OnPropertyChange("offset");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class Currently : INotifyPropertyChanged
    {
        double _apparentTemperature;

        public int time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public double nearestStormDistance { get; set; }
        public double precipIntensity { get; set; }
        public double precipProbability { get; set; }
        public double temperature { get; set; }
        public double apparentTemperature { get { return _apparentTemperature; } set { _apparentTemperature = value; OnPropertyChange("apparentTemperature"); } }
        public double dewPoint { get; set; }
        public double humidity { get; set; }
        public double pressure { get; set; }
        public double windSpeed { get; set; }
        public double windGust { get; set; }
        public double windBearing { get; set; }
        public double cloudCover { get; set; }
        public double uvIndex { get; set; }
        public double visibility { get; set; }
        public double ozone { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class Datum
    {
        public int time { get; set; }
        public int precipIntensity { get; set; }
        public int precipProbability { get; set; }
    }

    public class Minutely
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Datum2
    {
        public int time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public double precipIntensity { get; set; }
        public double precipProbability { get; set; }
        public string precipType { get; set; }
        public double temperature { get; set; }
        public double apparentTemperature { get; set; }
        public double dewPoint { get; set; }
        public double humidity { get; set; }
        public double pressure { get; set; }
        public double windSpeed { get; set; }
        public double windGust { get; set; }
        public int windBearing { get; set; }
        public double cloudCover { get; set; }
        public int uvIndex { get; set; }
        public double visibility { get; set; }
        public double ozone { get; set; }
        public int? precipAccumulation { get; set; }
    }

    public class Hourly
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum2> data { get; set; }
    }

    public class Datum3
    {
        public int time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public int sunriseTime { get; set; }
        public int sunsetTime { get; set; }
        public double moonPhase { get; set; }
        public double precipIntensity { get; set; }
        public double precipIntensityMax { get; set; }
        public int precipIntensityMaxTime { get; set; }
        public double precipProbability { get; set; }
        public string precipType { get; set; }
        public double temperatureHigh { get; set; }
        public int temperatureHighTime { get; set; }
        public double temperatureLow { get; set; }
        public int temperatureLowTime { get; set; }
        public double apparentTemperatureHigh { get; set; }
        public int apparentTemperatureHighTime { get; set; }
        public double apparentTemperatureLow { get; set; }
        public int apparentTemperatureLowTime { get; set; }
        public double dewPoint { get; set; }
        public double humidity { get; set; }
        public double pressure { get; set; }
        public double windSpeed { get; set; }
        public double windGust { get; set; }
        public int windGustTime { get; set; }
        public int windBearing { get; set; }
        public double cloudCover { get; set; }
        public int uvIndex { get; set; }
        public int uvIndexTime { get; set; }
        public double visibility { get; set; }
        public double ozone { get; set; }
        public double temperatureMin { get; set; }
        public int temperatureMinTime { get; set; }
        public double temperatureMax { get; set; }
        public int temperatureMaxTime { get; set; }
        public double apparentTemperatureMin { get; set; }
        public int apparentTemperatureMinTime { get; set; }
        public double apparentTemperatureMax { get; set; }
        public int apparentTemperatureMaxTime { get; set; }
        public double? precipAccumulation { get; set; }
    }

    public class Daily
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum3> data { get; set; }
    }

    public class Flags
    {
        public List<string> sources { get; set; }
        public string units { get; set; }
    }
}

