using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchUp.Models.DBModels
{
    [Table("Descriptions")]
    public class Description
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Numbers { get; set; }

        [MaxLength(10000)]
        public string Details { get; set; }
    }
}