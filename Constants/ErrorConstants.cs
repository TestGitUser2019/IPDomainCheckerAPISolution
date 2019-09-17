using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPDomainChecker.Constants
{
    public static class ErrorConstants
    {
        public static string InvalidRequest = "Request Specified is not valid";
        public static string InvalidIP = "IP address specified is not valid";
        public static  string InvalidDomain="Domain spcified is not valid";
        public static string InvalidServicelist = "Provided service ##Service is not supported by application";
    }
}
