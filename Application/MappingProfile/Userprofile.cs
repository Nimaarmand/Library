using Application.Dtos.Books;
using Application.Dtos.Identity.UserProfile;
using AutoMapper;
using Domain.Entities.Books;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfile
{
    public class Userprofile: AutoMapper.Profile
    {
        public Userprofile()
        {
            CreateMap<ProfileUser, UserProfileDto>().ReverseMap();

        }
    }
}
