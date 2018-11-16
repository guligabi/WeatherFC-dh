using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using WeatherFC.Views;

namespace WeatherFC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeSettingsFile();
                SetLanguage();

                InitializeComponent();
                this.DataContext = ViewModels.MainWindowVM.Get();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                Application.Current.Shutdown();
            }      
        }

        void SetLanguage()
        {
            var xdoc = XDocument.Load("settings.xml");
            var langElement = xdoc.Root.Elements("Language").First();

            string language = langElement.FirstAttribute.Value.ToString();

            CultureInfo ci = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        void InitializeSettingsFile()
        {
            if(!File.Exists("settings.xml"))
            {
                var sw = new SettingsWindow();
                sw.ShowDialog();
            }
        }
    }
}
