using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtremeMvcAppTest.Models;
using System.Net.Http;
using System.Web.Http;

namespace DevExtremeMvcAppTest.Controllers
{
    public class SampleDataController : ApiController {

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            return Request.CreateResponse(DataSourceLoader.Load(SampleData.Orders, loadOptions));
        }

    }
}