using System.Collections.Generic;
using System.Linq;
using MatchUp.Models;
using MatchUp.Models.DBModels;
using MatchUp.Shared;

namespace MatchUp.Services
{
    public class DescriptionService
    {
        ApplicationDbContext context = ApplicationDbContext.Create();

        public Dictionary<int, Description> GetDescriptions(PythagorianMatrix matrix)
        {

            var name = Constants.NamesEn[1];
            var character = context.Descriptions.FirstOrDefault(x => x.Numbers == matrix.CharacterWill &&
                                                                     x.Name.Contains(name));

            name = Constants.NamesEn[2];
            var energy = context.Descriptions.FirstOrDefault(x => x.Numbers == matrix.VitalEnergy &&
                                                                  x.Name.Contains(name));

            name = Constants.NamesEn[3];
            var cognitive = context.Descriptions.FirstOrDefault(x => x.Numbers == matrix.CognitiveCreative &&
                                                                     x.Name.Contains(name));

            name = Constants.NamesEn[4];
            var health = context.Descriptions.FirstOrDefault(x => x.Numbers == matrix.HealthBeauty &&
                                                                  x.Name.Contains(name));

            name = Constants.NamesEn[5];
            var logic = context.Descriptions.FirstOrDefault(x => x.Numbers == matrix.LogicIntuition &&
                                                                 x.Name.Contains(name));

            name = Constants.NamesEn[6];
            var labor = context.Descriptions.FirstOrDefault(x => x.Numbers == matrix.LaborSkill &&
                                                                 x.Name.Contains(name));

            name = Constants.NamesEn[7];
            var luck = context.Descriptions.FirstOrDefault(x => x.Numbers == matrix.Luck &&
                                                                x.Name.Contains(name));

            name = Constants.NamesEn[8];
            var duty = context.Descriptions.FirstOrDefault(x => x.Numbers == matrix.Duty &&
                                                                x.Name.Contains(name));

            name = Constants.NamesEn[9];
            var intellect = context.Descriptions.FirstOrDefault(x => x.Numbers == matrix.IntellectMemory &&
                                                                     x.Name.Contains(name));

            Dictionary<int, Description> descriptions = new Dictionary<int, Description>
            {
                {1, character},
                {2, energy},
                {3, cognitive},
                {4, health},
                {5, logic},
                {6, labor},
                {7, luck},
                {8, duty},
                {9, intellect}
            };

            return descriptions;
        }
    }
}