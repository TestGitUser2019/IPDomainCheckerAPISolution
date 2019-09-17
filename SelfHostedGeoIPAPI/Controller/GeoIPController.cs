using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using System.Web.Http;

namespace SelfHostedGeoIPAPI
{
   
    public class GeoIPController : ApiController
    {
        string Path = "http://api.ipstack.com/#IPAddress?access_key=04d5137a6a330512341a47b355995cf6";

        [HttpGet]
        public string GetGeoIPDetails(string IPAddress = "", string Domain = "")
        {
            string responsestring = string.Empty;
            Path= Path.Replace("#IPAddress", IPAddress);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(Path).Result;
                if (response.IsSuccessStatusCode)
                {
                    responsestring = response.Content.ReadAsStringAsync().Result.ToString();
                }
                else
                {
                    responsestring = "Error occured while communicating with GeoIP service";
                }
                return responsestring;
            }
        }
    }
}