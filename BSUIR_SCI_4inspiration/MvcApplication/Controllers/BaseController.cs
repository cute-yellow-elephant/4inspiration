using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppCore;

namespace MvcApplication.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        protected CoreHolder core = new CoreHolder();


    }
}
