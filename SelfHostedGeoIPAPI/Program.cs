using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SelfHostedGeoIPAPI
{
    class Program
    {
        static int Main(string[] args)
        {
            return (int)HostFactory.Run(x =>
            {
                x.Service<GeoIPOwinService>(s =>
                {
                    s.ConstructUsing(() => new GeoIPOwinService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
            });
        }
    }
}
