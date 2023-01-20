using AutoMapper;
using Business.Features.Products.Commands.CreateProduct;
using Business.Features.Products.Commands.DeleteProduct;
using Business.Features.Products.Commands.UpdateProduct;
using Business.Features.Products.Dtos;
using Business.Features.Products.Models;
using Core.Persistence.Paging;
using Entities.Concrete;

namespace Business.Features.Products.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, DeleteProductCommand>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();
            CreateMap<Product, CreatedProductDto>().ReverseMap();
            CreateMap<Product, DeletedProductDto>().ReverseMap();
            CreateMap<Product, UpdatedProductDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ForMember(x=> x.CategoryName, opt=> opt.MapFrom(x=> x.Category.Name))
                                            .ReverseMap();
            CreateMap<Product, ProductListDto>().ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                                                .ReverseMap();
            CreateMap<Product, ProductListByNameDto>().ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                                                .ReverseMap();
            CreateMap<IPaginate<Product>, ProductListModel>().ReverseMap();
            CreateMap<IPaginate<Product>, ProductListByNameModel>().ReverseMap();

        }
    }
}
