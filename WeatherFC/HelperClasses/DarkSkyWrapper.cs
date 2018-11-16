using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using WeatherFC.Models;

namespace WeatherFC.HelperClasses
{
    public static class DarkSkyWrapper
    {
        public static string GetUserKey(string path)
        {
            if (!File.Exists(path))
            { throw new FileNotFoundException("The file could not be found at the specified location! User key retrieval failed!"); }
            else
            {
                var xdoc = XDocument.Load(path);
                var keyElement = xdoc.Root.Elements("Key").First();
                string key = keyElement.FirstAttribute.Value.ToString();
                return key;
            }
        }

        public static string BuildRequestUrl(string key, string location, string language, string units)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://api.darksky.net/forecast/");
            sb.Append(key);
            sb.Append("/");
            sb.Append(location);

            if (language != null)
            {
                sb.Append("?lang=" + language);
            }
            if (units != null)
            {
                if (language == null)
                { sb.Append("?units=" + units); }
                else
                { sb.Append("&units=" + units); }
            }

            return sb.ToString();
        }

        public static Dictionary<string, string> GetCities()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var xdoc = XDocument.Load(assembly.GetManifestResourceStream("WeatherFC.Resources.Cities.xml"));
                var dictionary = xdoc.Root.Elements()
                                   .ToDictionary(a => (string)a.Attribute("name"),
                                                 a => (string)a.Attribute("location"));
                return dictionary;

        }

        public static WeatherData RequestDataFromApi(string city, string language, string units)
        {
            string uri = "";
            try
            {
                var key = GetUserKey("settings.xml");
                var cities = GetCities();
                string location = cities[city];
                uri = BuildRequestUrl(key, location, language, units);
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            string json;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                MessageBox.Show("The request has timed out. Check your connection or try again later.\nException details: " + we.Message);
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show("An unexpected error has occured.\nException details: " + e.Message);
                return null;
            }

            var data = JsonConvert.DeserializeObject<WeatherData>(json);
            return data;
        }

        public static object DeserializeJson(string json)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<WeatherData>(json);
                return obj;
            }
            catch (JsonSerializationException e)
            {
                MessageBox.Show("Data received from server appears to be corrupt. Exception details: " + e.Message);
                return null;
            }


        }
    }
}
