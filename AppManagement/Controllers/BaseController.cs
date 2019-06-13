
using System;
using System.Web;
using System.Web.Mvc;

namespace AppManagement.Controllers
{
   // [Authorize]
    public class BaseController : Controller
    {
        public BaseController()
        {

        }
        protected T GetSession<T>(string key)
        {
            return Session[key] != null ? (T)Session[key] : default;
        }
        protected void SetSession(string key,object value)
        {
           Session[key]  = value;
        }
    }
}