using System.Collections.Generic;
using System.Linq;
using MatchUp.Models;
using MatchUp.Models.DBModels;
using MatchUp.Shared;

namespace MatchUp.Services
{
    public class PersonService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        PythagorianMatrixService pythagorianService = new PythagorianMatrixService();

        public void Add(Person model)
        {
            context.Persons.Add(model);
            context.SaveChanges();

            pythagorianService.CreateUserMatrix(model);
            pythagorianService.CreateUserSecondaryAbilities(model);
        }

        public IEnumerable<UserViewModel> GetMyPerson(string userId)
        {
            var persons = context.Persons.Where(x => x.IdUser == userId).ToList();

            List<UserViewModel> personsModels = Mapper.ToUsers(persons);
            
            return personsModels;
        }

        public Person GetById(int id)
        {
            var person = context.Persons.FirstOrDefault(x => x.Id == id);

            person.Matrix = context.PythagorianMatrices.FirstOrDefault(x => x.Id == person.MatrixId);

            return person;
        }
    }
}