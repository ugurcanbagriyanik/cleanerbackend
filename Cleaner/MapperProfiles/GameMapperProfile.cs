using AutoMapper;
using Cleaner.Entities;
using Cleaner.Models;

namespace Cleaner.MapperProfiles
{
    public class GameMapperProfile : Profile
    {

        public GameMapperProfile()
        {

            CreateMap<GeneratableCleaner, GeneratableCleanerDTO>();
            CreateMap<GeneratableCleanerDTO,GeneratableCleaner>();
            
            CreateMap<PlayerCleaner,PlayerCleanerDTO>();
            CreateMap<PlayerCleanerDTO,PlayerCleaner>();
            
            CreateMap<GeneratableBodyPart,GeneratableBodyPartDTO>();
            CreateMap<GeneratableBodyPartDTO,GeneratableBodyPart>();
            
            CreateMap<PlayerBodyPart,PlayerBodyPartDTO>();
            CreateMap<PlayerBodyPartDTO,PlayerBodyPart>();
            
            CreateMap<PlayerWarMachinePart,PlayerWarMachinePartDTO>();
            CreateMap<PlayerWarMachinePartDTO,PlayerWarMachinePart>();
            
            CreateMap<PlayerWarMachine,PlayerWarMachineDTO>();
            CreateMap<PlayerWarMachine,EnemyWarMachineDTO>()
                .ForMember(dest => dest.Player, operations => operations
                .MapFrom(
                    source => source.PlayerCleaner.Player));
            CreateMap<PlayerWarMachineDTO,PlayerWarMachine>();
        }

    }
}