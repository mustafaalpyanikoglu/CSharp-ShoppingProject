using AutoMapper;
using Business.Features.UserOperationClaims.Dtos;
using Business.Features.UserOperationClaims.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using static Business.Features.UserOperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserOperationClaims.Commands.UpdateUserOperationClaim
{
    public class UpdateUserOperationClaimCommand:IRequest<UpdateUserOperationClaimDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public string[] Roles => new []{Admin,UserOperationClaimUpdate};

        public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdateUserOperationClaimDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public UpdateUserOperationClaimCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, 
                UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<UpdateUserOperationClaimDto> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserOperationClaimIdMustBeAvailable(request.Id);
                await _userOperationClaimBusinessRules.UserIdMustBeAvailable(request.UserId);
                await _userOperationClaimBusinessRules.OperationClaimIdMustBeAvailable(request.OperationClaimId);

                UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim updatedUserOperationClaim = await _unitOfWork.UserOperationClaimDal.UpdateAsync(mappedUserOperationClaim);
                UpdateUserOperationClaimDto updateUserOperationClaimDto = _mapper.Map<UpdateUserOperationClaimDto>(updatedUserOperationClaim);

                return updateUserOperationClaimDto;
            }
        }
    }
}
