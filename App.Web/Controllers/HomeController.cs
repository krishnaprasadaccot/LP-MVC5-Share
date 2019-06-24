using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, null);
            return View();
        }
    }
}