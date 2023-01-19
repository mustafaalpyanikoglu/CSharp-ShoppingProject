using AutoMapper;
using Business.Features.UserOperationClaims.Dtos;
using Business.Features.UserOperationClaims.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.UserOperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserOperationClaims.Queries.GetByIdUserOperationClaim
{
    public class GetByIdUserOperationClaimQuery:IRequest<UserOperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles => new[] { Admin, UserOperationClaimGet};

        public class GetByIdUserOperationClaimQueryHanlder : IRequestHandler<GetByIdUserOperationClaimQuery, UserOperationClaimDto>
        {
            private readonly IUserOperationClaimDal _userOperationClaimDal;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public GetByIdUserOperationClaimQueryHanlder(IUserOperationClaimDal userOperationClaimDal, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimDal = userOperationClaimDal;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<UserOperationClaimDto> Handle(GetByIdUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserOperationClaimIdMustBeAvailable(request.Id);

                UserOperationClaim? userOperationClaim = await _userOperationClaimDal.GetAsync(
                    u => u.Id == request.Id,
                    include: c => c.Include(c => c.User).Include(c => c.OperationClaim)
                    );
                UserOperationClaimDto userOperationClaimDto = _mapper.Map<UserOperationClaimDto>(userOperationClaim);

                return userOperationClaimDto;
            }
        }
    }
}
