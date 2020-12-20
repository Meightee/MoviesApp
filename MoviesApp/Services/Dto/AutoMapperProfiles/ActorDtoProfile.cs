using AutoMapper;
using MoviesApp.Models;

namespace MoviesApp.Services.Dto.AutoMapperProfiles
{
    public class ActorDtoProfile : Profile
    {

        public ActorDtoProfile()
        {
            CreateMap<Actor, ActorDto>().ReverseMap();
        }

    }
}