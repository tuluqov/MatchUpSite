using System;
using System.Collections.Generic;
using MatchUp.Models.DBModels;

namespace MatchUp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public int SimilarPercent { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Birthday { get; set; }
        public PythagorianMatrix PythagorianMatrix { get; set; }
        public SecondaryAbilities SecondaryAbilities { get; set; }
        public Dictionary<int, List<UserViewModel>> SameStars { get; set; }
        public Dictionary<int, Description> Descriptions { get; set; }
        public Dictionary<int, string> Matrix { get; set; }
        public Dictionary<int, int> SquarePersent { get; set; }
        public Dictionary<string, int> MoreInfo { get; set; }
        public Dictionary<string, int> MoreInfoPersent { get; set; }
    }
}