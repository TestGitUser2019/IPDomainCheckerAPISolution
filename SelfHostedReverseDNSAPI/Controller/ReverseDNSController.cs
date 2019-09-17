using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostedReverseDNSAPI.Controller
{
   public class ReverseDNSController:ApiController
    {
        string Path = "https://api.viewdns.info/reversedns/?ip=#IPAddress&apikey=52849ee9c669ed4335c333addfcbda112f9152f2&output=json";
        
         [HttpGet]
        public string GetReverseDNSDetails(string IPAddress = "", string Domain = "")
        {
            string responsestring = string.Empty;
            string URLPath=Path.Replace("#IPAddress", IPAddress);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(URLPath).Result;
                if (response.IsSuccessStatusCode)
                {
                    responsestring = response.Content.ReadAsStringAsync().Result.ToString();
                }
                else
                {
                    responsestring = "Error occured while communicating with Reverse DNS service";
                }
                return responsestring;
            }
        }
    }
}
