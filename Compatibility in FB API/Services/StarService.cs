using System;
using System.Collections.Generic;
using System.Linq;
using MatchUp.Models;
using MatchUp.Models.DBModels;
using MatchUp.Shared;

namespace MatchUp.Services
{
    public class StarService
    {
        ApplicationDbContext context = ApplicationDbContext.Create();
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

        public void CalcAllInTableSecond()
        {
            var allStars = context.Stars.ToList();

            foreach (var star in allStars)
            {
                if (star.SecondaryAbilitiesId != null)
                {
                    continue;
                }

                var secondaryAbilities = calculator.CalculateSecondaryAbilities(star.Matrix);

                context.SecondaryAbilitieses.Add(secondaryAbilities);

                context.SaveChanges();

                star.SecondaryAbilitiesId = secondaryAbilities.Id;

                context.SaveChanges();
            }
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            var stars = context.Stars.Where(x => x.Id < 100).ToList();

            List<UserViewModel> users = Mapper.ToUsers(stars);

            return users;
        }

        public IEnumerable<Star> Find(string name)
        {
            var stars = context.Stars.Where(x => x.Name.Contains(name)).Take(30);

            return stars;
        }

        private IEnumerable<Star> GetForUser(int idUserMatrix, int peopleNumbers)
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
                            x.Matrix.Id != idUserMatrix)
                .OrderBy(x => x.Id)
                .Take(peopleNumbers)
                .ToList();

            return similarStars;
        }

        public Dictionary<int, List<Star>> GetSimilarStars(int idUserMatrix)
        {
            var userMatrix = context.PythagorianMatrices.FirstOrDefault(x => x.Id == idUserMatrix);

            var mostSame = GetForUser(idUserMatrix, 2);

            var sameCharacter = context.Stars
                .Where(x => x.Matrix.CharacterWill == userMatrix.CharacterWill && x.MatrixId.Value != idUserMatrix)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameEnergy = context.Stars
                .Where(x => x.Matrix.VitalEnergy == userMatrix.VitalEnergy && x.MatrixId.Value != idUserMatrix)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameCognitive = context.Stars
                .Where(x => x.Matrix.CognitiveCreative == userMatrix.CognitiveCreative && x.MatrixId.Value != idUserMatrix)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameHealth = context.Stars
                .Where(x => x.Matrix.HealthBeauty == userMatrix.HealthBeauty && x.MatrixId.Value != idUserMatrix)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameLogic = context.Stars
                .Where(x => x.Matrix.LogicIntuition == userMatrix.LogicIntuition && x.MatrixId.Value != idUserMatrix)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameLabor = context.Stars
                .Where(x => x.Matrix.LaborSkill == userMatrix.LaborSkill && x.MatrixId.Value != idUserMatrix)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameLuck = context.Stars
                .Where(x => x.Matrix.Luck == userMatrix.Luck && x.MatrixId.Value != idUserMatrix)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameDuty = context.Stars
                .Where(x => x.Matrix.Duty == userMatrix.Duty && x.MatrixId.Value != idUserMatrix)
                .OrderBy(x => x.Id)
                .Skip(2)
                .Take(2)
                .ToList();

            var sameIntellect = context.Stars
                .Where(x => x.Matrix.IntellectMemory == userMatrix.IntellectMemory && x.MatrixId.Value != idUserMatrix)
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

        public IEnumerable<UserViewModel> GetSimilarStarsInPercent(int idUserMatrix)
        {
            var userMatrix = context.PythagorianMatrices.FirstOrDefault(x => x.Id == idUserMatrix);

            var similarStarsCount = context.Stars
                .Count(x => x.Matrix.CharacterWill == userMatrix.CharacterWill ||
                            x.Matrix.CognitiveCreative == userMatrix.CognitiveCreative ||
                            x.Matrix.Duty == userMatrix.Duty ||
                            x.Matrix.HealthBeauty == userMatrix.HealthBeauty ||
                            x.Matrix.IntellectMemory == userMatrix.IntellectMemory ||
                            x.Matrix.LaborSkill == userMatrix.LaborSkill ||
                            x.Matrix.LogicIntuition == userMatrix.LogicIntuition ||
                            x.Matrix.Luck == userMatrix.Luck ||
                            x.Matrix.VitalEnergy == userMatrix.VitalEnergy ||
                            x.Matrix.Id != userMatrix.Id);

            var skipCount = new Random().Next(similarStarsCount);

            var similarStars = context.Stars
                .Where(x => x.Matrix.CharacterWill == userMatrix.CharacterWill ||
                            x.Matrix.CognitiveCreative == userMatrix.CognitiveCreative ||
                            x.Matrix.Duty == userMatrix.Duty ||
                            x.Matrix.HealthBeauty == userMatrix.HealthBeauty ||
                            x.Matrix.IntellectMemory == userMatrix.IntellectMemory ||
                            x.Matrix.LaborSkill == userMatrix.LaborSkill ||
                            x.Matrix.LogicIntuition == userMatrix.LogicIntuition ||
                            x.Matrix.Luck == userMatrix.Luck ||
                            x.Matrix.VitalEnergy == userMatrix.VitalEnergy ||
                            x.Matrix.Id != userMatrix.Id)
                .OrderBy(x => x.Id)
                .Skip(skipCount)
                .Take(30)
                .ToList();

            var forUser = Mapper.ToUsers(similarStars);

            foreach (var userViewModel in forUser)
            {
                userViewModel.SimilarPercent =
                    calculator.GetComparePercentMatrix(userMatrix, userViewModel.PythagorianMatrix);
            }

            return forUser.OrderByDescending(x => x.SimilarPercent);
        }

        public IEnumerable<Star> GetCompatibilityStars(int idUserMatrix, int idSecodaryAbilities)
        {
            var userMatrix = context.PythagorianMatrices.FirstOrDefault(x => x.Id == idUserMatrix);
            var userSecondary = context.SecondaryAbilitieses.FirstOrDefault(x => x.Id == idSecodaryAbilities);

            var starsCount = context.Stars
                .Count(x => Math.Abs(x.Matrix.CharacterWill.Length -
                             userMatrix.CharacterWill.Length) > 2 &&
                    Math.Abs(x.Matrix.CharacterWill.Length -
                             userMatrix.CharacterWill.Length) < 5 &&
                    Math.Abs(x.SecondaryAbilities.Temperament - userSecondary.Temperament) < 2 &&
                    Math.Abs(x.SecondaryAbilities.DomesticBliss - userSecondary.DomesticBliss) < 2);

            var skipCount = new Random().Next(starsCount);

            var stars = context.Stars.Where(x =>
                 Math.Abs(x.Matrix.CharacterWill.Length -
                          userMatrix.CharacterWill.Length) > 2 &&
                 Math.Abs(x.Matrix.CharacterWill.Length -
                          userMatrix.CharacterWill.Length) < 5 &&
                 Math.Abs(x.SecondaryAbilities.Temperament - userSecondary.Temperament) < 2 &&
                 Math.Abs(x.SecondaryAbilities.DomesticBliss - userSecondary.DomesticBliss) < 2)
                .OrderBy(x => x.Id)
                .Skip(skipCount)
                .Take(30)
                .ToList();

            return stars;
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
    }
}