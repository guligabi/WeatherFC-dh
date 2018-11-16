using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherFC.Models
{
    class ForecastData
    {
        public string Icon { get; set; }
        public double Temperature { get; set; }
        public double ApparentTemperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public double WindSpeed { get; set; }
        public int UvIndex { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
    }
}
