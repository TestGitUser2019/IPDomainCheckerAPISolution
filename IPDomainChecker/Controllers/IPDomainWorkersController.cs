using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using IPDomainChecker.DataModels;

namespace IPDomainChecker.Controllers
{
    [Produces("application/json")]
    [Route("api/IPDomainWorkers")]
    public class IPDomainWorkersController : Controller
    {
        //[HttpGet]
        //[ActionName("Test")]
        //public string Get()
        //{
        //    return "Test";
        //}

        [HttpGet]
        public ActionResult GetIPDomainDetails(Request request)
        {
            try
            {
                if (request is null)
                {
                    return BadRequest(IPDomainChecker.Constants.ErrorConstants.InvalidRequest);
                }
                else
                {
                    IPDomainChecker.BusinesssLayer.IPDomainImplementation iPDomainImplementation = new IPDomainChecker.BusinesssLayer.IPDomainImplementation();

                    return Ok(iPDomainImplementation.GetIPDomainDetails(request));
                }
            }
            catch (Exception)
            {
                return NoContent();
                // throw;
            }

        }
    }
}