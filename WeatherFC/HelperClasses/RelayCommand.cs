using System;
using System.Windows.Input;

namespace WeatherFC
{
    class RelayCommand : ICommand
    {
        readonly Action<object> execute;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public RelayCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
