using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUp.Models.DBModels
{
    [Table("People")]
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string IdUser { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
        
        public int? MatrixId { get; set; }
        public virtual PythagorianMatrix Matrix { get; set; }

        public int? SecondaryAbilitiesId { get; set; }
        public virtual SecondaryAbilities SecondaryAbilities { get; set; }
    }
}