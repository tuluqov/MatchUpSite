using System.Linq;
using MatchUp.Models;
using MatchUp.Models.DBModels;

namespace MatchUp.Services
{
    public class PythagorianMatrixService
    {
        //ApplicationDbContext context = new ApplicationDbContext();
        PythagorianCalculator calculator = new PythagorianCalculator();

        public int CreateUserMatrix(ApplicationUser user, ApplicationDbContext context)
        {
            var matrix = calculator.CalculateSqare(user.Birthday);

            context.PythagorianMatrices.Add(matrix);

            context.SaveChanges();

            user.MatrixId = matrix.Id;

            context.SaveChanges();

            return matrix.Id;
        }

        public void CreateUserMatrix(Person person, ApplicationDbContext context)
        {
            var matrix = calculator.CalculateSqare(person.Birthday);

            context.PythagorianMatrices.Add(matrix);

            context.SaveChanges();

            person.MatrixId = matrix.Id;

            context.SaveChanges();
        }

        public void CreateUserSecondaryAbilities(ApplicationUser user, ApplicationDbContext context)
        {
            var userMatrix = context.PythagorianMatrices.FirstOrDefault(x => x.Id == user.MatrixId);
            
            var secondaryAbilities = calculator.CalculateSecondaryAbilities(userMatrix);

            context.SecondaryAbilitieses.Add(secondaryAbilities);

            context.SaveChanges();

            user.SecondaryAbilitiesId = secondaryAbilities.Id;

            context.SaveChanges();
        }

        public void CreateUserSecondaryAbilities(Person person, ApplicationDbContext context)
        {
            var secondaryAbilities = calculator.CalculateSecondaryAbilities(person.Matrix);

            context.SecondaryAbilitieses.Add(secondaryAbilities);

            context.SaveChanges();

            person.SecondaryAbilitiesId = secondaryAbilities.Id;

            context.SaveChanges();
        }
    }
}