using AutoMapper;
using CodeSmashWithAngular.Dtos;
using CodeSmashWithAngular.Models;

namespace CodeSmashWithAngular.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() {

            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<User, LoginResDto>().ReverseMap();
        }
    }
}
