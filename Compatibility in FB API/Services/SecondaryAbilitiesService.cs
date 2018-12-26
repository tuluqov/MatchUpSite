using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MatchUp.Models;

namespace MatchUp.Services
{
    public class SecondaryAbilitiesService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        PythagorianCalculator calculator = new PythagorianCalculator();

        //public void CreateUserSecondaryAbilities(ApplicationUser user)
        //{
        //    var secondaryAbilities = calculator.CalculateSecondaryAbilities(user.Matrix);

        //    context.SecondaryAbilitieses.Add(secondaryAbilities);

        //    context.SaveChanges();

        //    user.SecondaryAbilitiesId = secondaryAbilities.Id;

        //    context.SaveChanges();
        //}
    }
}