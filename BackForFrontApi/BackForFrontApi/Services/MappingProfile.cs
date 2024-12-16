using AutoMapper;
using BackForFrontApi.Dtos;
using BackForFrontApi.Models;

namespace BackForFrontApi.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<HouseEntity, HouseDto>().ReverseMap();
            CreateMap<HouseEntity, HouseDetailsDto>().ReverseMap();
            CreateMap<HouseDetailsDto, HouseEntity>();
        }
    }
}
