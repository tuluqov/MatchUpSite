using System.Security.Principal;
using MatchUp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MatchUp.Services
{
    public class UserService
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ApplicationUser GetCurrentUser(IPrincipal userContext)
        {
            var store = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(store);
            
            ApplicationUser user = userManager.FindByNameAsync(userContext.Identity.Name).Result;
            
            return user;
        }
    }
}