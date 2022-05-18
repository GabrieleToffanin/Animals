using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Models.DTOInputModels
{
    public class SpecieDTO
    {
        public SpecieDTO()
        {
            Animals = new HashSet<string>();
        }
        public string SpecieName { get; set; }
        public ICollection<string> Animals { get; set; }
    }
}
