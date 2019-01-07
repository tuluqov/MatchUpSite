using System;
using System.Web.Mvc;
using MatchUp.Models.DBModels;
using MatchUp.Services;
using Microsoft.AspNet.Identity;

namespace MatchUp.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly PersonService personService = new PersonService();
       
        [HttpGet]
        public ActionResult AddPerson()
        {
            ViewBag.Persons = personService.GetMyPerson(User.Identity.GetUserId());
            
            return View();
        }

        [HttpPost]
        public ActionResult AddPerson(Person model)
        {
            model.IdUser = User.Identity.GetUserId();

            personService.Add(model);

            return RedirectToAction("AddPerson");
        }
    }
}