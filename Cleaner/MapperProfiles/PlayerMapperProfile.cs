using AutoMapper;
using Cleaner.Entities;
using Cleaner.Models;
using SharedLibrary.Models;
namespace Cleaner.MapperProfiles
{
    public class PlayerMapperProfile : Profile
    {

        public PlayerMapperProfile(){

            CreateMap<Player, PlayerDTO>();

            CreateMap<UserRequest,Player>();

        }

    }
}