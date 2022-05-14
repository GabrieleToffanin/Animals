using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Interfaces
{
    public interface IMappingService
    {
        TAsked MapFrom<TStart, TAsked>(TStart item) where TStart : class 
                                                    where TAsked : class;
    }
}
