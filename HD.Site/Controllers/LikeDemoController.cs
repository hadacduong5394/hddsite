using HD.Core;
using HD.Service.Interface;
using System.Web.Mvc;

namespace HD.Site.Controllers
{
    public class LikeDemoController : BaseController
    {
        // GET: LikeDemo
        public ActionResult Index()
        {
            var pSrv = IoC.Resolve<IProductService>();

            return View(pSrv.GetAll());
        }
    }
}