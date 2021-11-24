using System.Web.Mvc;

namespace DevExtremeMvcAppTest.Controllers
{
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult Scheduler()
        {
            return View();
        }
    }
}