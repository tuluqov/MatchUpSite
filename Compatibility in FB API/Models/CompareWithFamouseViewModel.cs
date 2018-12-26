using System.Collections.Generic;

namespace MatchUp.Models
{
    public class CompareWithFamouseViewModel
    {
        public UserViewModel Person { get; set; }

        public IEnumerable<UserViewModel> FamousePersons { get; set; }
    }
}