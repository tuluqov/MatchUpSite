using System.Collections.Generic;
using System.Linq;
using MatchUp.Models;
using MatchUp.Models.DBModels;
using MatchUp.Shared;

namespace MatchUp.Services
{
    public class StarService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        PythagorianCalculator calculator = new PythagorianCalculator();

        public void CalcAllInTable()
        {
            var allStars = context.Stars.ToList();

            foreach (var star in allStars)
            {
                if (star.MatrixId != null)
                {
                    continue;
                }

                var matrix = calculator.CalculateSqare(star.Birthday);

                context.PythagorianMatrices.Add(matrix);

                context.SaveChanges();

                star.MatrixId = matrix.Id;

                context.SaveChanges();
            }
        }

        //public void CalcAllInTableSecond()
        //{
        //    var allStars = context.Stars.ToList();

        //    foreach (var star in allStars)
        //    {
        //        if (star.SecondaryAbilitiesId != null)
        //        {
        //            continue;
        //        }

        //        var secondaryAbilities = calculator.CalculateSecondaryAbilities(star.Matrix);

        //        context.SecondaryAbilitieses.Add(secondaryAbilities);

        //        context.SaveChanges();

        //        star.SecondaryAbilitiesId = secondaryAbilities.Id;

        //        context.SaveChanges();
        //    }
        //}

        public IEnumerable<UserViewModel> GetAll()
        {
            var stars = context.Stars.Where(x => x.Id < 100).ToList();

            List<UserViewModel> users = Mapper.ToUsers(stars);

            return users;
        }

        public IEnumerable<Star> Find(string name)
        {
            var stars = context.Stars.Where(x => x.Name.Contains(name)).Take(20);

            return stars;
        }

        private IEnumerable<Star> GetForUser(int idUserMatrix)
        {
            var userMatrix = context.PythagorianMatrices.FirstOrDefault(x => x.Id == idUserMatrix);

            var similarStars = context.Stars
                .Where(x => x.Matrix.CharacterWill == userMatrix.CharacterWill &&
                            x.Matrix.CognitiveCreative == userMatrix.CognitiveCreative &&
                            x.Matrix.Duty == userMatrix.Duty &&
                            x.Matrix.HealthBeauty == userMatrix.HealthBeauty &&
                            x.Matrix.IntellectMemory == userMatrix.IntellectMemory &&
                            x.Matrix.LaborSkill == userMatrix.LaborSkill &&
                            x.Matrix.LogicIntuition == userMatrix.LogicIntuition &&
                            x.Matrix.Luck == userMatrix.Luck &&
                            x.Matrix.VitalEnergy == userMatrix.VitalEnergy &&
                            x.Matrix.Id != userMatrix.Id)
                .OrderBy(x => x.Id)
                .Take(2)
                .ToList();

            return similarStars;
        }

        public Dictionary<int, List<Star>> GetSimilarStars(int idUserMatrix)
        {
            var userMatrix = context.PythagorianMatrices.FirstOrDefault(x => x.Id == idUserMatrix);

            var mostSame = GetForUser(idUserMatrix);

            var sameCharacter = context.Stars
                .Where(x => x.Matrix.CharacterWill == userMatrix.CharacterWill)
                .OrderBy(x=>x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameEnergy = context.Stars
                .Where(x => x.Matrix.VitalEnergy == userMatrix.VitalEnergy)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameCognitive = context.Stars
                .Where(x => x.Matrix.CognitiveCreative == userMatrix.CognitiveCreative)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameHealth = context.Stars
                .Where(x => x.Matrix.HealthBeauty == userMatrix.HealthBeauty)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameLogic = context.Stars
                .Where(x => x.Matrix.LogicIntuition == userMatrix.LogicIntuition)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameLabor = context.Stars
                .Where(x => x.Matrix.LaborSkill == userMatrix.LaborSkill)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameLuck = context.Stars
                .Where(x => x.Matrix.Luck == userMatrix.Luck)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameDuty = context.Stars
                .Where(x => x.Matrix.Duty == userMatrix.Duty)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameIntellect = context.Stars
                .Where(x => x.Matrix.IntellectMemory == userMatrix.IntellectMemory)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            Dictionary<int, List<Star>> allStars = new Dictionary<int, List<Star>>
            {
                {0, mostSame.ToList()},
                {1, sameCharacter },
                {2, sameEnergy },
                {3, sameCognitive},
                {4, sameHealth},
                {5, sameLogic},
                {6, sameLabor},
                {7, sameLuck},
                {8, sameDuty},
                {9, sameIntellect}
            };

            return allStars;
        }

        public Star GetById(int id)
        {
            var person = context.Stars.FirstOrDefault(x => x.Id == id);

            return person;
        }

        public void Add(UserViewModel model)
        {
            context.Stars.Add(Mapper.ToFamouse(model));
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private UserViewModel ToCalculateUser(Star person)
        {
            UserViewModel user = new UserViewModel
            {
                Id = person.Id.ToString(),
                Name = person.Name,
                Birthday = person.Birthday,
                Details = person.Details,
                PhotoUrl = person.PhotoUrl
            };

            user.PythagorianMatrix = calculator.CalculateSqare(user.Birthday);
            user.SquarePersent = calculator.GetPercent(user.PythagorianMatrix);
            user.MoreInfo = calculator.GetMoreInfo(user.Matrix);
            user.MoreInfoPersent = calculator.GetMoreInfoInPercent(user.MoreInfo);

            return user;
        }
    }
}