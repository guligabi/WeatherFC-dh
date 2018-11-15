using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherFC.ViewModels
{
    class SettingsWindowVM : INotifyPropertyChanged
    {
        string key;

        static SettingsWindowVM entity;

        public string Key { get { return key; }set { key = value; OnPropertyChange("Key"); } }

        public static SettingsWindowVM Get()
        {
            if (entity == null) entity = new SettingsWindowVM();
            return entity;
        }



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

        }
    }
}
