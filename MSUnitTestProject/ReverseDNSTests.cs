using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSUnitTestProject
{
    [TestClass]
    class ReverseDNSTests
    {
        [TestMethod]
        public void ReverseDNS_WhenCalled_ReturnsRespose()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig["APIPath"] = "";
            ReverseDNSAPI.Controllers.ReverseDNSController reverseDNSController = new ReverseDNSAPI.Controllers.ReverseDNSController(mockConfig);
            string IpAddress = "121.244.55.130";
            reverseDNSController.GetReverseDNSDetails(IpAddress);
        }
    }
}
