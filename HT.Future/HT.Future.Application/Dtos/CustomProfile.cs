using AutoMapper;
using HT.Future.Common;
using HT.Future.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Application
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(source => source.Gender, target => target.MapFrom(a => a.Gender.ToDisplay(DisplayProperty.Name)))
                .ReverseMap();
        }
    }
}
