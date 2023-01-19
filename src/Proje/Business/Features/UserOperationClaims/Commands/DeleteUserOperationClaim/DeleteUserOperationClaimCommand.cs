using AutoMapper;
using Business.Features.UserOperationClaims.Dtos;
using Business.Features.UserOperationClaims.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
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
            private readonly IUserOperationClaimDal _userOperationClaimDal;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public DeleteUserOperationClaimCommandHandler(IUserOperationClaimDal userOperationClaimDal, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimDal = userOperationClaimDal;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<DeleteUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserOperationClaimIdMustBeAvailable(request.Id);

                UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim deletedUserOperationClaim = await _userOperationClaimDal.DeleteAsync(mappedUserOperationClaim);
                DeleteUserOperationClaimDto deleteUserOperationClaimDto = _mapper.Map<DeleteUserOperationClaimDto>(deletedUserOperationClaim);

                return deleteUserOperationClaimDto;
            }
        }
    }
}
