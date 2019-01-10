using System;
using System.Linq;
using System.Web.Mvc;
using MatchUp.Models;
using MatchUp.Services;
using MatchUp.Shared;
using Microsoft.AspNet.Identity;

namespace MatchUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly PersonService personService = new PersonService();
        private readonly StarService starService = new StarService();
        private readonly PythagorianCalculator calculator = new PythagorianCalculator();
        private readonly UserService userService = new UserService();
        private readonly DescriptionService descriptionService = new DescriptionService();

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("UserPage");
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult UserPage(int id = 0, bool isFamouse = false)
        {
            if (id == 0)
            {
                var user = userService.GetCurrentUser(User);
                UserViewModel model = Mapper.ToUser(user);
                
                model.SameStars = Mapper.ToSameUsers(starService.GetSimilarStars(user.MatrixId.Value));

                model.SquarePersent = calculator.GetPercent(model.PythagorianMatrix);
                model.Descriptions = descriptionService.GetDescriptions(model.PythagorianMatrix);

                return View(model);
            }
            else
            {
                if (!isFamouse)
                {
                    var person = personService.GetById(id, User.Identity.GetUserId());

                    if (person == null)
                    {
                        return HttpNotFound();
                    }

                    var personModel = Mapper.ToUser(person);

                    personModel.SameStars = Mapper.ToSameUsers(starService.GetSimilarStars(person.MatrixId.Value));

                    personModel.SquarePersent = calculator.GetPercent(person.Matrix);
                    personModel.Descriptions = descriptionService.GetDescriptions(personModel.PythagorianMatrix);

                    return View(personModel);
                }

                var personFamouse = Mapper.ToUser(starService.GetById(id));

                if (personFamouse == null)
                {
                    return HttpNotFound();
                }

                personFamouse.SquarePersent = calculator.GetPercent(personFamouse.PythagorianMatrix);
                personFamouse.Descriptions = descriptionService.GetDescriptions(personFamouse.PythagorianMatrix);

                return View(personFamouse);
            }

        }

        [Authorize]
        [HttpGet]
        public ActionResult Comtibility(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }

            CompatibilityViewModel model = new CompatibilityViewModel
            {
                User1 = Mapper.ToUser(userService.GetCurrentUser(User)),
                User2 = Mapper.ToUser(personService.GetById(id, User.Identity.GetUserId()))
            };

            calculator.CalculateAll(model.User2);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Comtibility(int year, int month, int day)
        {
            DateTime birthday = new DateTime(year, month, day);

            CompatibilityViewModel model = new CompatibilityViewModel
            {
                User1 = Mapper.ToUser(userService.GetCurrentUser(User)),
                User2 = new UserViewModel
                {
                    Birthday = birthday,
                    Name = "Вторя половинка",
                }
            };

            calculator.CalculateAll(model.User2);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CompatibilityInPerson(int id, bool isFamouse = false)
        {
            if (!isFamouse)
            {
                CompatibilityViewModel model = new CompatibilityViewModel
                {
                    User1 = Mapper.ToUser(userService.GetCurrentUser(User)),
                    User2 = Mapper.ToUser(personService.GetById(id, User.Identity.GetUserId()))
                };
                
                return View("Comtibility", model);
            }
            else
            {
                CompatibilityViewModel model = new CompatibilityViewModel
                {
                    User1 = Mapper.ToUser(userService.GetCurrentUser(User)),
                    User2 = Mapper.ToUser(starService.GetById(id))
                };
                
                return View("Comtibility", model);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Comapare(int id, bool isFamouse = false)
        {
            if (isFamouse)
            {
                CompatibilityViewModel model = new CompatibilityViewModel
                {
                    User1 = Mapper.ToUser(userService.GetCurrentUser(User)),
                    User2 = Mapper.ToUser(starService.GetById(id))
                };

                model.User1.SquarePersent = calculator.GetPercent(model.User1.PythagorianMatrix);
                model.User2.SquarePersent = calculator.GetPercent(model.User2.PythagorianMatrix);

                return View(model);
            }
            else
            {
                CompatibilityViewModel model = new CompatibilityViewModel
                {
                    User1 = Mapper.ToUser(userService.GetCurrentUser(User)),
                    User2 = Mapper.ToUser(personService.GetById(id, User.Identity.GetUserId()))
                };

                model.User1.SquarePersent = calculator.GetPercent(model.User1.PythagorianMatrix);
                model.User2.SquarePersent = calculator.GetPercent(model.User2.PythagorianMatrix);

                return View(model);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Search(string name)
        {
            var currentUser = userService.GetCurrentUser(User);

            var result = Mapper.ToUsers(starService.Find(name).ToList());

            foreach (var model in result)
            {
                model.SimilarPercent = 
                    calculator.GetComparePercentMatrix(currentUser.Matrix, model.PythagorianMatrix);
            }

            return View(result);
        }
    }
}