using AutoMapper;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Models;
using Business.Features.Orders.Commands.ConfirmOrder;
using Business.Features.Orders.Commands.CreateOrder;
using Business.Features.Orders.Commands.DeleteOrder;
using Business.Features.Orders.Commands.UpdateOrder;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Models;
using Business.Services.OrderService;
using Core.Persistence.Paging;
using Entities.Concrete;

namespace Business.Features.Orders.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Order, DeleteOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
            CreateMap<Order, ConfirmOrderCommand>().ReverseMap();
            CreateMap<Order, ConfirmOrderDto>().ReverseMap();
            CreateMap<Order, CreatedOrderDto>().ReverseMap();
            CreateMap<Order, DeletedOrderDto>().ReverseMap();
            CreateMap<Order, UpdatedOrderDto>().ReverseMap();
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.UserCart.User.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.UserCart.User.LastName))
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.UserCart.User.Address))
                .ReverseMap();
            CreateMap<Order, OrderListDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.UserCart.User.FirstName)).
                ForMember(x => x.LastName, opt => opt.MapFrom(x => x.UserCart.User.LastName))
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.UserCart.User.Address))
                .ReverseMap();
            CreateMap<IPaginate<Order>, OrderListModel>().ReverseMap();
        }
    }
}
