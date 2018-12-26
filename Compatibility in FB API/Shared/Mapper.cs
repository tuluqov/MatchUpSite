using System.Collections.Generic;
using MatchUp.Models;
using MatchUp.Models.DBModels;

namespace MatchUp.Shared
{

    public static class Mapper
    {
        public static UserViewModel ToUser(Person model)
        {
            UserViewModel user = new UserViewModel
            {
                Id = model.Id.ToString(),
                Name = model.Name,
                Birthday = model.Birthday,
                PhotoUrl = @"\Content\img\ava.png",
                PythagorianMatrix = model.Matrix,
                SecondaryAbilities = model.SecondaryAbilities
            };

            return user;
        }

        public static UserViewModel ToUser(Star person)
        {
            UserViewModel user = new UserViewModel
            {
                Id = person.Id.ToString(),
                Name = person.Name,
                Birthday = person.Birthday,
                Details = person.Details,
                PhotoUrl = $"../../HtmlFiles/{person.PhotoUrl.Replace("./", "")}",
                PythagorianMatrix = person.Matrix,
                SecondaryAbilities = person.SecondaryAbilities
            };

            return user;
        }

        public static UserViewModel ToUser(ApplicationUser applicationUser)
        {
            UserViewModel user = new UserViewModel
            {
                Id = applicationUser.Id,
                Name = applicationUser.Name,
                Birthday = applicationUser.Birthday,
                PythagorianMatrix = applicationUser.Matrix,
                PhotoUrl = applicationUser.PhotoUrl,
                SecondaryAbilities = applicationUser.SecondaryAbilities
            };

            return user;
        }

        public static List<UserViewModel> ToUsers(List<Star> stars)
        {
            List<UserViewModel> models = new List<UserViewModel>();

            foreach (Star mostSimilarStar in stars)
            {
                models.Add(ToUser(mostSimilarStar));
            }

            return models;
        }


        public static List<UserViewModel> ToUsers(List<Person> persons)
        {
            List<UserViewModel> models = new List<UserViewModel>();

            foreach (Person mostSimilarStar in persons)
            {
                models.Add(ToUser(mostSimilarStar));
            }

            return models;
        }

        public static Dictionary<int, List<UserViewModel>> ToSameUsers(Dictionary<int, List<Star>> stars)
        {
            Dictionary<int, List<UserViewModel>> modelsLists = new Dictionary<int, List<UserViewModel>>();

            foreach (var list in stars)
            {
                modelsLists.Add(list.Key, ToUsers(list.Value));
            }

            return modelsLists;
        }

        public static Star ToFamouse(UserViewModel model)
        {
            Star famouse = new Star
            {
                Id = int.Parse(model.Id),
                Name = model.Name,
                Birthday = model.Birthday,
                Details = model.Details,
                PhotoUrl = model.PhotoUrl
            };

            return famouse;
        }

        public static Person ToPerson(UserViewModel model)
        {
            Person person = new Person
            {
                Id = int.Parse(model.Id),
                Name = model.Name,
                Birthday = model.Birthday
            };

            return person;
        }
    }
}