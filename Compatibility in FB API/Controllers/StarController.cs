using System.Linq;
using System.Web.Mvc;
using MatchUp.Models;
using MatchUp.Services;
using MatchUp.Shared;
using Microsoft.AspNet.Identity;

namespace MatchUp.Controllers
{
    public class StarController : Controller
    {
        private readonly StarService starService = new StarService();
        private readonly UserService userService = new UserService();

        [HttpGet]
        public ActionResult Stars()
        {
            var user = userService.GetCurrentUser(User);

            var stars = starService.GetSimilarStarsInPercent(user.MatrixId.Value);

            ViewBag.Ok = stars;
            ViewBag.Compatibility = Mapper.ToUsers(starService
                .GetCompatibilityStars(user.MatrixId.Value, user.SecondaryAbilitiesId.Value)
                .ToList());

            CompareWithFamouseViewModel model = new CompareWithFamouseViewModel
            {
                Person = Mapper.ToUser(user),
                FamousePersons = stars
            };

            return View(model);
        }
    }
}