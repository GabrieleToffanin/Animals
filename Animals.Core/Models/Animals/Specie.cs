using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Models.Animals
{
    [Table("Specie")]
    public class Specie
    {
        public Specie()
        {
            Animals = new HashSet<Animal>();
        }

        [Key]
        public int SpecieId { get; set; }

        public string SpecieName { get; set; }
        public string? TypeOfBirth { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }
    }
}
