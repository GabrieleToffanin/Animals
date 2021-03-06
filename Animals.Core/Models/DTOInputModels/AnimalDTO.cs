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
        public string? Name { get; set; }
        public bool? IsProtectedSpecie { get; set; }
        public int AnimalHistoryAge { get; set; }
        public int Left { get; set; }

        public string Specie { get; set; }
    }
}
