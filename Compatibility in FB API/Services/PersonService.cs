using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MatchUp.Models;
using MatchUp.Models.DBModels;
using MatchUp.Shared;

namespace MatchUp.Services
{
    public class PersonService
    {
        ApplicationDbContext context = ApplicationDbContext.Create();
        PythagorianMatrixService pythagorianService = new PythagorianMatrixService();

        public void Add(Person model)
        {
            context.Persons.Add(model);
            context.SaveChanges();

            pythagorianService.CreateUserMatrix(model, context);

            model = context.Persons.FirstOrDefault(x => x.Id == model.Id);

            pythagorianService.CreateUserSecondaryAbilities(model, context);
        }

        public IEnumerable<UserViewModel> GetMyPerson(string userId)
        {
            var persons = context.Persons
                .Where(x => x.IdUser == userId)
                .OrderByDescending(x => x.Id)
                .ToList();

            List<UserViewModel> personsModels = Mapper.ToUsers(persons);

            return personsModels;
        }

        public Person GetById(int id, string userId)
        {
            var person = context.Persons.FirstOrDefault(x => x.Id == id && x.IdUser == userId);

            return person;
        }

        public void DeleteById(int id, string userId)
        {
            var person = context.Persons.FirstOrDefault(x => x.Id == id && x.IdUser == userId);

            if (person != null)
            {
                context.Persons.Remove(person);
                context.SaveChanges();
            }
        }

        public void Edit(Person person)
        {
            var editPerson = context.Persons.FirstOrDefault(x => x.Id == person.Id);

            if (editPerson != null)
            {
                editPerson.Birthday = person.Birthday;
                editPerson.Name = person.Name;

                context.SaveChanges();
            }
        }
    }
}