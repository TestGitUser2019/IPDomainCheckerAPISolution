using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SelfHostedReverseDNSAPI
{
    class Program
    {
        static int Main(string[] args)
        {
            return (int)HostFactory.Run(x =>
            {
                x.Service<ReverseDNSOwinService>(s =>
                {
                    s.ConstructUsing(() => new ReverseDNSOwinService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
            });
        }
    }
}
