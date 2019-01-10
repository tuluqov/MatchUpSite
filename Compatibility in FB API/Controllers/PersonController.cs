using System;
using System.Web.Mvc;
using MatchUp.Models;
using MatchUp.Models.DBModels;
using MatchUp.Services;
using MatchUp.Shared;
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

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            personService.DeleteById(id.Value, User.Identity.GetUserId());

            return Redirect(Url.Action("AddPerson"));
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.ToPersonEditView(personService.GetById(id.Value, User.Identity.GetUserId()));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                personService.Edit(Mapper.ToPerson(model));

                return Redirect(Url.Action("AddPerson"));
            }

            return View(model);
        }
    }
}