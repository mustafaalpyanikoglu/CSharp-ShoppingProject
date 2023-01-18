using AutoMapper;
using Business.Features.OperationClaims.Dtos;
using Business.Features.OperationClaims.Rules;
using Core.Business.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.OperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OperationClaims.Queries.GetByIdOperationClaim
{
    public class GetByIdOperationClaimQuery : IRequest<OperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles => new[] { Admin, OperationClaimGet };

        public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, OperationClaimDto>
        {
            private readonly IOperationClaimDal _operationClaimDal;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public GetByIdOperationClaimQueryHandler(IOperationClaimDal operationClaimDal, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimDal = operationClaimDal;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<OperationClaimDto> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(request.Id);

                OperationClaim? operationClaim = await _operationClaimDal.GetAsync(m => m.Id == request.Id);
                OperationClaimDto operationClaimDto = _mapper.Map<OperationClaimDto>(operationClaim);

                return operationClaimDto;
            }
        }
    }
}
