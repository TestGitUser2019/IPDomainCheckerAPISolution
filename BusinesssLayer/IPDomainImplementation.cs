
using IPDomainChecker.DataModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static IPDomainChecker.Constants.Constants;

namespace IPDomainChecker.BusinesssLayer
{
    public class IPDomainImplementation
    {
     
        string GeoIPURL = "http://localhost:9000/api/GeoIP?IpAddress=";
        string ReverseDNSURL = "http://localhost:8200/api/ReverseDNS?IpAddress=";

        public Dictionary<string, string> GetIPDomainDetails(Request request)
        {
            Dictionary<string, string> response = null;
            if (request != null)
            {
                if (!String.IsNullOrEmpty(request.IpAddress) && IsValidIP(request.IpAddress))
                {
                    if (!String.IsNullOrEmpty(request.Domain) && IsValidDomainName(request.Domain))
                    {
                        return CallServices(request);
                    }
                    else
                    {
                        response = new Dictionary<string, string>();
                        response["Error"] = IPDomainChecker.Constants.ErrorConstants.InvalidDomain.ToString();
                        return response;
                    }
                }
                else
                {
                    response = new Dictionary<string, string>();
                    response["Error"] = IPDomainChecker.Constants.ErrorConstants.InvalidIP.ToString();
                    return response;
                }
            }
            response = new Dictionary<string, string>();
            response["Error"] = IPDomainChecker.Constants.ErrorConstants.InvalidRequest.ToString();
            return response;
        }

        private Dictionary<string, string> CallServices(Request request)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            //foreach (var service in request.ServiceList)
            Parallel.ForEach(request.ServiceList, (service) =>
            {
                string endpoint = GetServiceEndpoint(request, service);
                if (string.IsNullOrEmpty(endpoint))
                {
                    string Error = IPDomainChecker.Constants.ErrorConstants.InvalidServicelist;
                    response[service] = Error.Replace("##Service", service);
                }
                else
                {
                    Infrastructure.IRestClient RestClient = new Infrastructure.RestClient(endpoint);
                    response[service] = RestClient.GetAsync<string>(endpoint).Result.ToString();
                }
            });
            return response;
        }

        private string GetServiceEndpoint(Request request, string service)
        {
            string endpoint = string.Empty;
            SupportedServices supportedservice;
            if (Enum.TryParse<SupportedServices>(Convert.ToString(service), out supportedservice))
            {
                switch (supportedservice)
                {
                    case SupportedServices.GeoIP:
                        endpoint = GeoIPURL+ request.IpAddress;
                        break;
                    case SupportedServices.ReverseDNS:
                        endpoint = ReverseDNSURL + request.IpAddress;
                         break;
                }
            }
            return endpoint;
        }

        private bool IsValidDomainName(string name)
        {
            return Uri.CheckHostName(name) != UriHostNameType.Unknown;
        }

        private bool IsValidIP(string ipaddress)
        {
            IPAddress ip = null;
            return IPAddress.TryParse(ipaddress, out ip);
        }

    }
}
