using Animals.Core.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Services
{
    public sealed class MappingService : IMappingService
    {
        public TAsked MapFrom<TStart, TAsked>(TStart item) where TStart : class
                                                           where TAsked : class
        {
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<TStart, TAsked>());
            var mapper = config.CreateMapper();

            return mapper.Map<TAsked>(item);
        }
    }
}
