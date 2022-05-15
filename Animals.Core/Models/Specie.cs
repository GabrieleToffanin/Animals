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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Specie_Id { get; set; }
        public string SpecieName { get; set; }
        public ICollection<Animal> Animals { get; set; }
    }
}
