using AutoMapper;
using Business.Features.OperationClaims.Dtos;
using Business.Features.OperationClaims.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using static Business.Features.OperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OperationClaims.Commands.CreateOperationClaim
{
    public class CreateOperationClaimCommand:IRequest<CreatedOperationClaimDto>,ISecuredRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] Roles => new[] {Admin,OperationClaimAdd};

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public CreateOperationClaimCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                
                await _operationClaimBusinessRules.OperationClaimNameShouldBeNotExists(request.Name);

                OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim createdOperationClaim = await _unitOfWork.OperationClaimDal.AddAsync(mappedOperationClaim);
                CreatedOperationClaimDto createOperationClaimDto = _mapper.Map<CreatedOperationClaimDto>(createdOperationClaim);

                await _unitOfWork.SaveChangesAsync();

                return createOperationClaimDto;
            }
        }
    }
}
