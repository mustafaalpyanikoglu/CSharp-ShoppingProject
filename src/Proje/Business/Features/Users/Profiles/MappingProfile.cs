using AutoMapper;
using Business.Features.Users.Command.CreateUser;
using Business.Features.Users.Command.DeleteUser;
using Business.Features.Users.Command.UpdateUser;
using Business.Features.Users.Command.UpdateUserFromAuth;
using Business.Features.Users.Dtos;
using Business.Features.Users.Models;
using Core.DataAccess.Paging;
using Entities.Concrete;

namespace Business.Features.Users.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, DeleteUserCommand>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<User, CreatedUserDto>().ReverseMap();
            CreateMap<User, UpdatedUserDto>().ReverseMap();
            CreateMap<User, DeletedUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserListDto>().ReverseMap();
            CreateMap<IPaginate<User>, UserListModel>().ReverseMap();
            CreateMap<User, UpdateUserFromAuthCommand>().ReverseMap();
            CreateMap<User, UpdatedUserFromAuthDto>().ReverseMap();
        }
    }
}
