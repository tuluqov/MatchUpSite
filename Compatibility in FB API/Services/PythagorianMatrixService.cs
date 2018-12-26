using MatchUp.Models;
using MatchUp.Models.DBModels;

namespace MatchUp.Services
{
    public class PythagorianMatrixService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        PythagorianCalculator calculator = new PythagorianCalculator();

        public void CreateUserMatrix(ApplicationUser user)
        {
            var matrix = calculator.CalculateSqare(user.Birthday);

            context.PythagorianMatrices.Add(matrix);

            context.SaveChanges();

            user.MatrixId = matrix.Id;

            context.SaveChanges();
        }

        public void CreateUserMatrix(Person person)
        {
            var matrix = calculator.CalculateSqare(person.Birthday);

            context.PythagorianMatrices.Add(matrix);

            context.SaveChanges();

            person.MatrixId = matrix.Id;

            context.SaveChanges();
        }

        //public void CreateUserSecondaryAbilities(ApplicationUser user)
        //{
        //    var secondaryAbilities = calculator.CalculateSecondaryAbilities(user.Matrix);

        //    context.SecondaryAbilitieses.Add(secondaryAbilities);

        //    context.SaveChanges();

        //    user.SecondaryAbilitiesId = secondaryAbilities.Id;

        //    context.SaveChanges();
        //}

        //public void CreateUserSecondaryAbilities(Person person)
        //{
        //    var secondaryAbilities = calculator.CalculateSecondaryAbilities(person.Matrix);

        //    context.SecondaryAbilitieses.Add(secondaryAbilities);

        //    context.SaveChanges();

        //    person.SecondaryAbilitiesId = secondaryAbilities.Id;

        //    context.SaveChanges();
        //}
    }
}