using AutoMapper;
using Business.Features.OperationClaims.Commands.CreateOperationClaim;
using Business.Features.OperationClaims.Commands.DeleteOperationClaim;
using Business.Features.OperationClaims.Commands.UpdateOperationClaim;
using Business.Features.OperationClaims.Dtos;
using Business.Features.OperationClaims.Models;
using Core.DataAccess.Paging;
using Entities.Concrete;

namespace Business.Features.OperationClaims.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, DeleteOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, CreatedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, DeletedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, UpdatedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, OperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, OperationClaimListDto>().ReverseMap();
            CreateMap<IPaginate<OperationClaim>, OperationClaimListModel>().ReverseMap();
        }
    }
}
