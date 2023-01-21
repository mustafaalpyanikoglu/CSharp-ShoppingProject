using AutoMapper;
using Business.Features.Orders.Commands.ConfirmOrder;
using Business.Features.Orders.Commands.CreateOrder;
using Business.Features.Orders.Commands.DeleteOrder;
using Business.Features.Orders.Commands.UpdateOrder;
using Business.Features.Orders.Commands.UpdateProductQuantityInOrder;
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
            CreateMap<Order, ConfirmOrderDto>().ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Product.Name))
                                               .ForMember(x => x.ProductPrice, opt => opt.MapFrom(x => x.Product.Price))
                                               .ReverseMap();
            CreateMap<Order, UpdateProductQuantityInOrderDto>().ReverseMap();
            CreateMap<Order, CreatedOrderDto>().ReverseMap();
            CreateMap<Order, DeletedOrderDto>().ReverseMap();
            CreateMap<Order, UpdatedOrderDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderListDto>().ReverseMap();
            CreateMap<Order, OrderListDtoForCustomer>().ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Product.Name))
                                                    .ForMember(x => x.ProductPrice, opt => opt.MapFrom(x => x.Product.Price))
                                                    .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Product.Category.Name))
                                                    .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.UserCart.User.FirstName))
                                                    .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.UserCart.User.LastName))
                                                    .ForMember(x => x.Address, opt => opt.MapFrom(x => x.UserCart.User.Address))
                                                    .ReverseMap();
            CreateMap<IPaginate<Order>, OrderListModel>().ReverseMap();
            CreateMap<IPaginate<Order>, OrderListByUserCartModel>().ReverseMap();
        }
    }
}
