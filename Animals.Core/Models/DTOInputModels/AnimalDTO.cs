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
        [Required]
        [Column("Name", Order = 2, TypeName = "nvarchar(80)")]
        public string? Name { get; set; }
    }
}
