using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Models.DTOInputModels
{
    public class AnimalCreationRequest
    {
        public string? Name { get; set; }
        public bool? IsProtectedSpecie { get; set; }
        public int AnimalHistoryAge { get; set; }
        public int Left { get; set; }

        public string Specie { get; set; }
    }
}
