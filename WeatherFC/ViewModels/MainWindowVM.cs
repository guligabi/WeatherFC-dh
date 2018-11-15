using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeatherFC.HelperClasses;
using WeatherFC.Models;

namespace WeatherFC.ViewModels
{
    class MainWindowVM : INotifyPropertyChanged
    {
        WeatherData actualData;
        string currentCity;
        string currentLanguage;
        string errorMsg;
        ObservableCollection<ForecastData> forecast;
        ObservableCollection<string> cityList;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string CurrentCity { get { return currentCity; } set { currentCity = value; OnPropertyChange("CurrentCity"); GetWeatherData(null); } }
        public string CurrentLanguage { get { return currentLanguage; } set { currentLanguage = value; OnPropertyChange("CurrentLanguage"); } }
        public WeatherData ActualData { get { return actualData; } set { actualData = value; OnPropertyChange("ActualData"); ErrorMsg = ""; } }
        public ObservableCollection<ForecastData> Forecast { get { return forecast; } set { forecast = value; OnPropertyChange("Forecast"); } }
        public ObservableCollection<string> CityList { get { return cityList; } set { cityList = value; OnPropertyChange("CityList"); } }
        public string ErrorMsg { get { return errorMsg; } set { errorMsg = value; OnPropertyChange("ErrorMsg"); } }

        static MainWindowVM entity;
        public static MainWindowVM Get()
        {
            if (entity == null) entity = new MainWindowVM();
            return entity;
        }

        public RelayCommand QuitApplicationCommand { get; set; }
        public RelayCommand ChangeCityCommand { get; set; }
        public RelayCommand ChangeLanguageCommand { get; set; }


        public MainWindowVM()
        {
            QuitApplicationCommand = new RelayCommand(QuitApplication);
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);

            Forecast = new ObservableCollection<ForecastData>();
            CityList = new ObservableCollection<string>();
            CurrentCity = "Budapest";
            currentLanguage = "en";
            UpdateCities(null);
        }

        void UpdateCities(object parameter)
        {
            var cities = DarkSkyWrapper.GetCities("cities.xml");

            foreach (KeyValuePair<string, string> c in cities)
            {
                CityList.Add(c.Key);
            }

        }

        void GetWeatherData(object parameter)
        {
            var data = DarkSkyWrapper.RequestDataFromApi(CurrentCity, CurrentLanguage, "ca");
            if (data != null)
            {
                ActualData = data;
                Forecast.Clear();
                foreach (Datum3 d in actualData.daily.data)
                {
                    Forecast.Add(new ForecastData
                    {
                        Temperature = d.temperatureHigh,
                        Icon = d.icon,
                        ApparentTemperature = d.apparentTemperatureHigh,
                        Humidity = d.humidity,
                        Pressure = d.pressure,
                        WindSpeed = d.windSpeed,
                        UvIndex = d.uvIndex
                    });
                }
            }
            else
            { ErrorMsg = "Connection to DarkSky services failed."; }
        }

        void QuitApplication(object parameter)
        {
            Application.Current.Shutdown();
        }

        void ChangeLanguage(object parameter)
        {

        }
    }
}
