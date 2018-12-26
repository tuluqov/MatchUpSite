using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUp.Models.DBModels
{
    [Table("Stars")]
    public class Star
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string Details { get; set; }

        [Required]
        [MaxLength(1000)]
        public string PhotoUrl { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public int? MatrixId { get; set; }
        public virtual PythagorianMatrix Matrix { get; set; }

        //public int? SecondaryAbilitiesId { get; set; }
        //public virtual SecondaryAbilities SecondaryAbilities { get; set; }
    }
}