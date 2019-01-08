using System.Linq;
using AutoMapper;
using DATINGAPP.API.Dtos;
using DATINGAPP.API.Models;

namespace DATINGAPP.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User,UserForListDtos>()
                 .ForMember(dest => dest.PhotoUrl, opt => {
                     opt.MapFrom(src => src.Photos.FirstOrDefault(x=>x.IsMain).Url);
                 })
                 .ForMember(dest => dest.Age, opt => {
                     opt.ResolveUsing(src => src.DateOfBirth.CalculateAge());
                 });

            CreateMap<User,UserDetailsForDtos>()
            .ForMember(dest => dest.PhotoUrl, opt => {
                     opt.MapFrom(src => src.Photos.FirstOrDefault(x=>x.IsMain).Url);
                 })
                 .ForMember(dest => dest.Age, opt => {
                     opt.ResolveUsing(src => src.DateOfBirth.CalculateAge());
                 });
             CreateMap<Photo,PhotoForDetailsDtos>();
        }

    }
}