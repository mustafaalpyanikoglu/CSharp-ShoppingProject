using AutoMapper;
using Business.Features.UserOperationClaims.Dtos;
using Business.Features.UserOperationClaims.Rules;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.UserOperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand : IRequest<CreateUserOperationClaimDto>//, ISecuredRequest
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public string[] Roles => new []{Admin,UserOperationClaimAdd}; 
        public class CreateUserOperationClaimCommandHanlder : IRequestHandler<CreateUserOperationClaimCommand, CreateUserOperationClaimDto>
        {
            private readonly IUserOperationClaimDal _userOperationClaimDal;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public CreateUserOperationClaimCommandHanlder(IUserOperationClaimDal userOperationClaimDal, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimDal = userOperationClaimDal;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<CreateUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserIdMustBeAvailable(request.UserId);
                await _userOperationClaimBusinessRules.OperationClaimIdMustBeAvailable(request.OperationClaimId);

                UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim createdUserOperationClaim = await _userOperationClaimDal.AddAsync(mappedUserOperationClaim);
                CreateUserOperationClaimDto createUserOperationClaimDto = _mapper.Map<CreateUserOperationClaimDto>(createdUserOperationClaim);

                return createUserOperationClaimDto;
            }
        }
    }
}
