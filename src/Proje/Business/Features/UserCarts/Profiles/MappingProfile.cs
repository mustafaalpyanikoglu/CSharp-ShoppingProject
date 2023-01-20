using AutoMapper;
using Business.Features.UserCarts.Commands.CreateUserCart;
using Business.Features.UserCarts.Commands.DeleteUserCart;
using Business.Features.UserCarts.Commands.UpdateUserCart;
using Business.Features.UserCarts.Dtos;
using Business.Features.UserCarts.Models;
using Core.Persistence.Paging;
using Entities.Concrete;

namespace Business.Features.UserCarts.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCart, CreateUserCartCommand>().ReverseMap();
            CreateMap<UserCart, DeleteUserCartCommand>().ReverseMap();
            CreateMap<UserCart, UpdateUserCartCommand>().ReverseMap();
            CreateMap<UserCart, CreatedUserCartDto>().ReverseMap();
            CreateMap<UserCart, DeletedUserCartDto>().ReverseMap();
            CreateMap<UserCart, UpdatedUserCartDto>().ReverseMap();
            CreateMap<UserCart, UserCartDto>().ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                                        .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.User.LastName))
                                        .ReverseMap();
            CreateMap<UserCart, UserCartListDto>().ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                                        .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.User.LastName))
                                        .ReverseMap();
            CreateMap<IPaginate<UserCart>, UserCartListModel>().ReverseMap();
        }
    }
}
