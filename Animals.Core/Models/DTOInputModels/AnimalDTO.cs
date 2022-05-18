using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Models.DTOInputModels
{
    public class AnimalDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public string Specie { get; set; }
    }
}
