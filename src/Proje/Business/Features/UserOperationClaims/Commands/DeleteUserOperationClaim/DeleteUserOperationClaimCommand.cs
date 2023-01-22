using AutoMapper;
using Business.Features.UserOperationClaims.Dtos;
using Business.Features.UserOperationClaims.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using static Business.Features.UserOperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimCommand:IRequest<DeleteUserOperationClaimDto>,ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new []{Admin,UserOperationClaimDelete};

        public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeleteUserOperationClaimDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public DeleteUserOperationClaimCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, 
                UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<DeleteUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserOperationClaimIdMustBeAvailable(request.Id);

                UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim deletedUserOperationClaim = await _unitOfWork.UserOperationClaimDal.DeleteAsync(mappedUserOperationClaim);
                DeleteUserOperationClaimDto deleteUserOperationClaimDto = _mapper.Map<DeleteUserOperationClaimDto>(deletedUserOperationClaim);

                return deleteUserOperationClaimDto;
            }
        }
    }
}
