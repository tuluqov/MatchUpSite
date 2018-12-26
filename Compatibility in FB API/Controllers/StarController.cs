using System.Web.Mvc;
using MatchUp.Models;
using MatchUp.Services;
using MatchUp.Shared;

namespace MatchUp.Controllers
{
    public class StarController : Controller
    {
        private readonly StarService starService = new StarService();
        private readonly UserService userService = new UserService();

        [HttpGet]
        public ActionResult Stars()
        {
            var stars = starService.GetAll();
            ViewBag.Ok = stars;

            CompareWithFamouseViewModel model = new CompareWithFamouseViewModel
            {
                Person = Mapper.ToUser(userService.GetCurrentUser(User)),
                FamousePersons = stars
            };

            return View(model);
        }
    }
}