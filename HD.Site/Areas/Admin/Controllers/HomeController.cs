using System.Web.Mvc;

namespace HD.Site.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}