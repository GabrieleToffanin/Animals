using Animals.Core.Exceptions;
using Animals.Core.Interfaces;
using Animals.Core.Models;
using Animals.Core.Models.DTOInputModels;
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
        private IMapper? _mapper
            => ConfigureMapper().CreateMapper();


        private MapperConfiguration ConfigureMapper()
            => new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<AnimalDTO, Animal>();
                    cfg.CreateMap<Animal, AnimalDTO>();
                    cfg.CreateMap<Specie, Animal>()
                        .ForMember(cfg => cfg.Specie, opt => opt.MapFrom(map => map.SpecieName));
                    cfg.CreateMap<Animal, Specie>()
                    .ForMember(cfg => cfg.SpecieName, opt => 
                    {
                        opt.MapFrom(map => map.Specie);
                    });
                });
        
                

        public TAsked MapFrom<TStart, TAsked>(TStart item) where TStart : class
                                                           where TAsked : class
            => _mapper?.Map<TAsked>(item) ?? throw new UnableToPerfomMappingException("Provided data has no mapping configuration, or bad data provided");
        
    }
}
