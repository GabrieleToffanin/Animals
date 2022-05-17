using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Animals.Core.Models
{
    [Table("Animals")]
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        
        public string? Name { get; set; }

        
        
        public Specie OwnSpecie { get; set; }
    }
}