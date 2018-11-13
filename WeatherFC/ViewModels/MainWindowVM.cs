using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherFC.ViewModels
{
    class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        static MainWindowVM entity;
        public static MainWindowVM Get()
        {
            if (entity == null) entity = new MainWindowVM();
            return entity;
        }
    }
}
