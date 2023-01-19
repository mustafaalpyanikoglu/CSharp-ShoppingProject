using AutoMapper;
using Business.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Business.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Business.Features.UserOperationClaims.Commands.UpdateUserOperationClaim;
using Business.Features.UserOperationClaims.Dtos;
using Business.Features.UserOperationClaims.Models;
using Core.Persistence.Paging;
using Entities.Concrete;

namespace Business.Features.UserOperationClaims.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, DeleteUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, CreateUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, DeleteUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, UpdateUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, UserOperationClaimDto>().ForMember(t => t.UserFirstName, opt => opt.MapFrom(u => u.User.FirstName))
                                                                      .ForMember(t => t.UserLastName, opt => opt.MapFrom(u => u.User.LastName))
                                                                      .ForMember(t => t.OperationClaimName, opt => opt.MapFrom(u => u.OperationClaim.Name))
                                                                      .ForMember(t => t.OperationClaimNameDescription, opt => opt.MapFrom(u => u.OperationClaim.Description))
                                                                      .ReverseMap();
            CreateMap<UserOperationClaim, UserOperationClaimListDto>().ForMember(t => t.UserFirstName, opt => opt.MapFrom(u => u.User.FirstName))
                                                                      .ForMember(t => t.UserLastName, opt => opt.MapFrom(u => u.User.LastName))
                                                                      .ForMember(t => t.OperationClaimName, opt => opt.MapFrom(u => u.OperationClaim.Name))
                                                                      .ForMember(t => t.OperationClaimNameDescription, opt => opt.MapFrom(u => u.OperationClaim.Description))
                                                                      .ReverseMap();
            CreateMap<IPaginate<UserOperationClaim>, UserOperationClaimListModel>().ReverseMap();
        }
    }
}
