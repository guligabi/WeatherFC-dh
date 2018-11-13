using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                StreamReader sr = new StreamReader(path);
                var key = sr.ReadLine();
                return key;
            }
        }

        public static string BuildRequestUrl(string key, string city)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://api.darksky.net/forecast/");
            sb.Append(key);

            var cities = GetCities("cities.xml");
            string location = cities["city"];
            sb.Append("/");
            sb.Append(location);

            return sb.ToString();
        }

        public static Dictionary<string,string> GetCities(string path)
        {
            var xdoc = XDocument.Load(path);
            var dictionary = xdoc.Root.Elements()
                               .ToDictionary(a => (string)a.Attribute("name"),
                                             a => (string)a.Attribute("location"));
            return dictionary;
        }

        public static string RequestDataFromApi(string key, string city)
        {
            string uri = BuildRequestUrl(key, city);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static object DeserializeJson(string json)
        {
            var obj = JsonConvert.DeserializeObject(json);

            return obj;
        
        }
    }
}
