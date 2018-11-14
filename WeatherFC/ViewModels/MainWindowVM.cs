using System;
using System.Collections.Generic;
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


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string CurrentCity { get { return currentCity; } set { currentCity = value; OnPropertyChange("CurrentCity"); } }
        public string CurrentLanguage { get { return currentLanguage; } set { currentLanguage = value; OnPropertyChange("CurrentLanguage"); } }
        public WeatherData ActualData { get { return actualData; } set { actualData = value; OnPropertyChange("ActualData"); } }

        static MainWindowVM entity;
        public static MainWindowVM Get()
        {
            if (entity == null) entity = new MainWindowVM();
            return entity;
        }

        public RelayCommand QuitApplicationCommand { get; set; }
        public RelayCommand ChangeCityCommand { get; set; }

        public MainWindowVM()
        {
            QuitApplicationCommand = new RelayCommand(QuitApplication);
            ChangeCityCommand = new RelayCommand(ChangeCity);

            CurrentCity = "Placeholder";
            currentLanguage = "en";
        }

        void GetWeatherData(object parameter)
        {
            var data = DarkSkyWrapper.RequestDataFromApi(currentCity,)
        }

        void QuitApplication(object parameter)
        {
            Application.Current.Shutdown();
        }

        void ChangeCity (object parameter)
        {
            CurrentCity = "Munich";
        }
    }
}
