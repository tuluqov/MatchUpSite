using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatchUp.Controllers
{
    public class ErrorsController : Controller
    {
        [HttpGet]
        public ActionResult Error404()
        {
            Response.StatusCode = 404;

            return View();
        }

        [HttpGet]
        public ActionResult Error500()
        {
            Response.StatusCode = 500;

            return View();
        }
    }
}