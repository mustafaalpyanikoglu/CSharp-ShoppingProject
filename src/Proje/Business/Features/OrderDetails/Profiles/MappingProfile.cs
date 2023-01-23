using AutoMapper;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Models;
using Business.Features.OrderDetails.Commands.CreateOrder;
using Business.Features.OrderDetails.Commands.UpdateOrder;
using Business.Features.Orders.Dtos;
using Core.Persistence.Paging;
using Entities.Concrete;
using Business.Features.OrderDetails.Commands.DeleteOrder;
using Business.Features.OrderDetails.Commands.UpdateOrderDetailForCustomer;

namespace Business.Features.OrderDetails.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDetail, CreateOrderDetailCommand>()
                .ReverseMap();
            CreateMap<OrderDetail, DeleteOrderDetailCommand>().ReverseMap();
            CreateMap<OrderDetail, UpdateOrderDetailCommand>().ReverseMap();
            CreateMap<OrderDetail, UpdateOrderDetailForCustomerCommand>().ReverseMap();
            CreateMap<OrderDetail, UpdatedOrderDetailForCustomerDto>().ReverseMap();
            CreateMap<OrderDetail, CreatedOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, DeletedOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, UpdatedOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(t => t.FirstName, opt => opt.MapFrom(u => u.Order.UserCart.User.FirstName))
                .ForMember(t => t.LastName, opt => opt.MapFrom(u => u.Order.UserCart.User.LastName))
                .ForMember(t => t.OrderNumber, opt => opt.MapFrom(u => u.Order.OrderNumber))
                .ForMember(t => t.OrderAmount, opt => opt.MapFrom(u => u.Order.OrderAmount))
                .ForMember(t => t.OrderDate, opt => opt.MapFrom(u => u.Order.OrderDate))
                .ForMember(t => t.ApprovalDate, opt => opt.MapFrom(u => u.Order.ApprovalDate))
                .ForMember(t => t.OrderStatus, opt => opt.MapFrom(u => u.Order.Status))
                .ForMember(t => t.CategoryName, opt => opt.MapFrom(u => u.Product.Category.Name))
                .ForMember(t => t.ProductName, opt => opt.MapFrom(u => u.Product.Name))
                .ForMember(t => t.ProductQuantity, opt => opt.MapFrom(u => u.Product.Quantity))
                .ForMember(t => t.ProductPrice, opt => opt.MapFrom(u => u.Product.Price))
                .ForMember(t => t.ProductTotalPrice, opt => opt.MapFrom(u => u.TotalPrice))
                .ReverseMap();
            CreateMap<OrderDetail, OrderDetailListDto>()
                .ForMember(t => t.FirstName, opt => opt.MapFrom(u => u.Order.UserCart.User.FirstName))
                .ForMember(t => t.LastName, opt => opt.MapFrom(u => u.Order.UserCart.User.LastName))
                .ForMember(t => t.OrderNumber, opt => opt.MapFrom(u => u.Order.OrderNumber))
                .ForMember(t => t.OrderAmount, opt => opt.MapFrom(u => u.Order.OrderAmount))
                .ForMember(t => t.OrderDate, opt => opt.MapFrom(u => u.Order.OrderDate))
                .ForMember(t => t.ApprovalDate, opt => opt.MapFrom(u => u.Order.ApprovalDate))
                .ForMember(t => t.OrderStatus, opt => opt.MapFrom(u => u.Order.Status))
                .ForMember(t => t.CategoryName, opt => opt.MapFrom(u => u.Product.Category.Name))
                .ForMember(t => t.ProductName, opt => opt.MapFrom(u => u.Product.Name))
                .ForMember(t => t.ProductQuantity, opt => opt.MapFrom(u => u.Product.Quantity))
                .ForMember(t => t.ProductPrice, opt => opt.MapFrom(u => u.Product.Price))
                .ForMember(t => t.ProductTotalPrice, opt => opt.MapFrom(u => u.TotalPrice))
                .ReverseMap();
            CreateMap<OrderDetail, OrderDetailListDtoForCustomer>()
                .ForMember(t => t.FirstName, opt => opt.MapFrom(u => u.Order.UserCart.User.FirstName))
                .ForMember(t => t.LastName, opt => opt.MapFrom(u => u.Order.UserCart.User.LastName))
                .ForMember(t => t.Address, opt => opt.MapFrom(u => u.Order.UserCart.User.Address))
                .ForMember(t => t.OrderNumber, opt => opt.MapFrom(u => u.Order.OrderNumber))
                .ForMember(t => t.OrderAmount, opt => opt.MapFrom(u => u.Order.OrderAmount))
                .ForMember(t => t.OrderDate, opt => opt.MapFrom(u => u.Order.OrderDate))
                .ForMember(t => t.CategoryName, opt => opt.MapFrom(u => u.Product.Category.Name))
                .ForMember(t => t.ProductName, opt => opt.MapFrom(u => u.Product.Name))
                .ForMember(t => t.ProductPrice, opt => opt.MapFrom(u => u.Product.Price))
                .ForMember(t => t.ProductTotalPrice, opt => opt.MapFrom(u => u.TotalPrice))
                .ReverseMap();
            CreateMap<OrderDetail, UserPastOrderListDto>()
               .ForMember(t => t.FirstName, opt => opt.MapFrom(u => u.Order.UserCart.User.FirstName))
               .ForMember(t => t.LastName, opt => opt.MapFrom(u => u.Order.UserCart.User.LastName))
               .ForMember(t => t.Address, opt => opt.MapFrom(u => u.Order.UserCart.User.Address))
               .ForMember(t => t.OrderNumber, opt => opt.MapFrom(u => u.Order.OrderNumber))
               .ForMember(t => t.OrderAmount, opt => opt.MapFrom(u => u.Order.OrderAmount))
               .ForMember(t => t.OrderDate, opt => opt.MapFrom(u => u.Order.OrderDate))
               .ForMember(t => t.ApprovalDate, opt => opt.MapFrom(u => u.Order.ApprovalDate))
               .ForMember(t => t.CategoryName, opt => opt.MapFrom(u => u.Product.Category.Name))
               .ForMember(t => t.ProductName, opt => opt.MapFrom(u => u.Product.Name))
               .ForMember(t => t.ProductPrice, opt => opt.MapFrom(u => u.Product.Price))
               .ForMember(t => t.ProductTotalPrice, opt => opt.MapFrom(u => u.TotalPrice))
               .ReverseMap();


            CreateMap<IPaginate<OrderDetail>, OrderDetailListModel>().ReverseMap();
            CreateMap<IPaginate<OrderDetail>, OrderDetailListByUserCartModel>().ReverseMap();
            CreateMap<IPaginate<OrderDetail>, UserPastOrderListModel>().ReverseMap();
        }
    }
}
