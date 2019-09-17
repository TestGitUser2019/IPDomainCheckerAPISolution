using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSUnitTestProject
{
    [TestClass]
    class GeoIPUnitTests
    {

        [TestMethod]
        public void GeoIP_WhenCalled_ReturnsRespose()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig["APIPath"] = "";
            GeoIPAPI.Controllers.GeoIPController geoIPController = new GeoIPAPI.Controllers.GeoIPController();
            string IpAddress = "121.244.55.130";
            geoIPController.GetGeoIPDetails(IpAddress);
        }
    }
}
