using AutoMapper;
using Dating.API.Models;
using Dating.API.Dtos;
using System.Linq;
using System;

namespace Dating.API.Helpers
{
    public class AutoMappersProfiles : Profile
    {
        public AutoMappersProfiles() 
        {
          CreateMap<User, UserForListDtos> ()
           .ForMember(dest => dest.PhotoUrl, opt =>
            opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
           .ForMember(dest => dest.Age, opt =>
            opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
          CreateMap<User, UserForDetailedDtos> ()
            .ForMember(dest => dest.PhotoUrl, opt =>
            opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(dest => dest.Age, opt =>
            opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
          CreateMap<Photo, PhotosForDetailedDtos> (); 
          CreateMap<UserForUpdateDtos, User> ();
          CreateMap<Photo, PhotoForReturnDtos> ();
          CreateMap<PhotoForCreationDtos, Photo> ();
          CreateMap<UserRegisterDtos, User> ();
        }

       
      
    }
}