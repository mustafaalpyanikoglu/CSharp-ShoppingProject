using AutoMapper;
using Business.Features.Products.Models;
using Business.Features.Purses.Commands.CreatePurse;
using Business.Features.Purses.Commands.DeletePurse;
using Business.Features.Purses.Commands.UpdatePurseMoney;
using Business.Features.Purses.Dtos;
using Business.Features.Purses.Models;
using Core.Persistence.Paging;
using Entities.Concrete;

namespace Business.Features.Purses.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Purse, CreatePurseCommand>().ReverseMap();
            CreateMap<Purse, DeletePurseCommand>().ReverseMap();
            CreateMap<Purse, UpdatePurseCommand>().ReverseMap();
            CreateMap<Purse, UpdatePurseMoneyCommand>().ReverseMap();
            CreateMap<Purse, CreatedPurseDto>().ReverseMap();
            CreateMap<Purse, DeletedPurseDto>().ReverseMap();
            CreateMap<Purse, UpdatedPurseDto>().ReverseMap();
            CreateMap<Purse, UpdatedPurseMoneyDto>().ReverseMap();
            CreateMap<Purse, PurseDto>().ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                                        .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.User.LastName))
                                        .ReverseMap();
            CreateMap<Purse, PurseListDto>().ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                                        .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.User.LastName))
                                        .ReverseMap();
            CreateMap<IPaginate<Purse>, PurseListModel>().ReverseMap();
        }
    }
}
