using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Models
{
    [Table("Specie")]
    public class Specie
    {
        public Specie()
        {
            Animals = new HashSet<Animal>();
        }

        [Key]
        public int Specie_Id { get; set; }
        
        public string SpecieName { get; set; }
        
        public virtual ICollection<Animal> Animals { get; set; }
    }
}
