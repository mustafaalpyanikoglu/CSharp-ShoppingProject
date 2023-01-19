using AutoMapper;
using Business.Features.Categories.Commands.CreateCategory;
using Business.Features.Categories.Commands.DeleteCategory;
using Business.Features.Categories.Commands.UpdateCategory;
using Business.Features.Categories.Dtos;
using Business.Features.Products.Models;
using Core.Persistence.Paging;
using Entities.Concrete;

namespace Business.Features.Categorys.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, DeleteCategoryCommand>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<Category, CreatedCategoryDto>().ReverseMap();
            CreateMap<Category, DeletedCategoryDto>().ReverseMap();
            CreateMap<Category, UpdatedCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<IPaginate<Category>, CategoryListModel>().ReverseMap();
        }
    }
}
