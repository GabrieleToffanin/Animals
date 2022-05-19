using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Models.DTOInputModels
{
    public class AnimalUpdateRequest
    {
        public string? Name { get; set; }

        public string Specie { get; set; }
    }
}
