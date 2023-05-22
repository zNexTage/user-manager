using System;
using AutoMapper;
using UserManager.Data.DTO;
using UserManager.Models;

namespace UserManager.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDTO, User>();
    }
}
