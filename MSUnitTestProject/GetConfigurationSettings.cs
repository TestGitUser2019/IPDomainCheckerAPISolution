using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSUnitTestProject
{
    class GetConfigurationSettings
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets("e3dfcccf-0cb3-423a-b302-e3e92e95c128")
                .AddEnvironmentVariables()
                .Build();
        }

        public static string GetApplicationConfiguration(string outputPath)
        {
            
            var iConfig = GetIConfigurationRoot(outputPath);
            return iConfig["APIPath"];
                
        }
    }
}
