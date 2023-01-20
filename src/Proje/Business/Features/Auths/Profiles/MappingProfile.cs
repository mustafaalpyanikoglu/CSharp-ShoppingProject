using AutoMapper;
using Business.Features.Auths.Commands.ChangePassword;
using Business.Features.Auths.Dtos;
using Entities.Concrete;

namespace Business.Features.Auths.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, ChangePasswordCommand>().ReverseMap();
            CreateMap<User, UserForChangePasswordDto>().ReverseMap();
        }
    }
}
