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
using System.Windows.Interactivity;
using System.Globalization;
using System.Threading;
using System.Xml.Linq;
using System.IO;

namespace WeatherFC.ViewModels
{
    class MainWindowVM : INotifyPropertyChanged
    {
        WeatherData actualData;
        string currentCity;
        string currentLanguage;
        Visibility errorMsg;
        ObservableCollection<ForecastData> forecast;
        ObservableCollection<string> cityList;
        string language;

        DateTime currentDate;
        string currentDay;
        string currentMonth;
        string currentYear;

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
        public WeatherData ActualData { get { return actualData; } set { actualData = value; OnPropertyChange("ActualData"); ErrorMsg = Visibility.Hidden; } }
        public ObservableCollection<ForecastData> Forecast { get { return forecast; } set { forecast = value; OnPropertyChange("Forecast"); } }
        public ObservableCollection<string> CityList { get { return cityList; } set { cityList = value; OnPropertyChange("CityList"); } }
        public Visibility ErrorMsg { get { return errorMsg; } set { errorMsg = value; OnPropertyChange("ErrorMsg"); } }
        public DateTime CurrentDate { get { return currentDate; } set { currentDate = value; OnPropertyChange("CurrentDate"); MapDateToProperties(); } }
        public string CurrentDay { get { return currentDay; } set { currentDay = value; OnPropertyChange("CurrentDay"); } }
        public string CurrentMonth { get { return currentMonth; } set { currentMonth = value; OnPropertyChange("CurrentMonth"); } }
        public string CurrentYear { get { return currentYear; } set { currentYear = value; OnPropertyChange("CurrentYear"); } }
        public string Language { get { return language; } set { language = value; OnPropertyChange("Language"); OnPropertyChange("Language"); } }

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
            CurrentDate = DateTime.Today;
            CurrentCity = "Budapest";
            currentLanguage = "en";
            UpdateCities(null);
        }

        void MapDateToProperties()
        {
            CurrentDay = CurrentDate.ToString("dd");
            CurrentMonth = CurrentDate.ToString("MMMM");
            CurrentYear = CurrentDate.ToString("yyyy");
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
                int dateOffset = 0;
                foreach (Datum3 d in actualData.daily.data)
                {
                    dateOffset++;
                    Forecast.Add(new ForecastData
                    {
                        Temperature = d.temperatureHigh,
                        Icon = d.icon,
                        ApparentTemperature = d.apparentTemperatureHigh,
                        Humidity = d.humidity,
                        Pressure = d.pressure,
                        WindSpeed = d.windSpeed,
                        UvIndex = d.uvIndex,
                        Date = DateTime.Today.AddDays(dateOffset).Day.ToString(),
                        Month = DateTime.Today.AddDays(dateOffset).ToString("MMMM")
                    });
                }
            }
            else
            { ErrorMsg = Visibility.Visible; }
        }

        void QuitApplication(object parameter)
        {
            Application.Current.Shutdown();
        }

        void ChangeLanguage(object parameter)
        {
            if (File.Exists("settings.xml"))
            {
                var doc = XDocument.Load("settings.xml");
                var lang = doc.Root.Element("Language").FirstAttribute.Value = parameter.ToString();


                doc.Save("settings.xml");

                MessageBox.Show("You need to restart the application for the changes to take effect.");
            }
        }
    }
}
