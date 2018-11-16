using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Linq;

namespace WeatherFC.ViewModels
{
    class SettingsWindowVM : INotifyPropertyChanged
    {
        string key;
        bool engBtnChecked;
        bool huBtnChecked;

        static SettingsWindowVM entity;

        public static SettingsWindowVM Get()
        {
            if (entity == null) entity = new SettingsWindowVM();
            return entity;
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand QuitApplicationCommand { get; set; }
        public bool EngBtnChecked { get { return engBtnChecked; } set { engBtnChecked = value; OnPropertyChange("EngBtnChecked"); } }
        public bool HuBtnChecked { get { return huBtnChecked; } set { huBtnChecked = value; OnPropertyChange("HuBtnChecked"); } }
        public string Key { get { return key; } set { key = value; OnPropertyChange("Key"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public SettingsWindowVM()
        {
            SaveCommand = new RelayCommand(Save);
            QuitApplicationCommand = new RelayCommand(QuitApplication);
        }

        void Save(object parameter)
        {
            if (!EngBtnChecked && !HuBtnChecked)
            {
                MessageBox.Show("Please select a language!", "Language required", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Key == null)
            {
                MessageBox.Show("Please enter your key.", "Key required", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string language = "";
            if(EngBtnChecked)
            { language = "en-GB"; }
            if(HuBtnChecked)
            { language = "hu-HU"; }

            try
            {
                var doc = new XDocument(
                    new XElement("Settings",
                        new XElement("Key", new XAttribute("UserKey", Key)),
                        new XElement("Language", new XAttribute("Lang", language))));
                doc.Save("settings.xml");
            }
            catch(UnauthorizedAccessException)
            {
                MessageBox.Show("Settings file could not be saved. The target path is protected.");
            }
            CloseWindow(parameter);
        }

        void CloseWindow(object parameter)
        {
            var currentWindow = (Window)parameter;
            currentWindow.Close();
        }

        void QuitApplication(object parameter)
        {
            Environment.Exit(0);
        }
    }
}
