using System;
using System.ComponentModel.DataAnnotations;

namespace MatchUp.Models
{
    public class PersonEditViewModel
    {
        public int Id { get; set; }

        public string IdUser { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}