using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather
{
    class Program
    {
        static string uri = "http://datapoint.metoffice.gov.uk/public/data/";
        static string service = "val/wxfcs/all/json/sitelist";
        static string key = "?key=de564a61-349d-4238-8c4d-023663e2adde";
        static string url = uri + service + key;

        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseBody);
            var result = JsonConvert.DeserializeObject<WeatherObj>(responseBody);
            foreach (var item in result.Locations.Location)
            {
                Console.WriteLine(item.name+" "+item.elevation);
            }
            Console.ReadLine();
        }

        public class WeatherObj
        {
            public WeatherSiteObj Locations { get; set; }

        }

        public class WeatherSiteObj
        {
            public Stuff[] Location { get; set; }
        }

        public class Stuff
        {
            public int id { get; set; }
            public string name { get; set; }
            public double elevation { get; set; }
            public double longitutde { get; set; }
            public double latitude { get; set; }
            public string region { get; set; }
            public string unitaryAuthArea { get; set; }
        }

    }
}
