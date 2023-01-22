using AutoMapper;
using Business.Features.OperationClaims.Dtos;
using Business.Features.OperationClaims.Rules;
using DataAccess.Abstract;
using MediatR;
using Entities.Concrete;
using Core.Application.Pipelines.Authorization;
using static Business.Features.OperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;
using DataAccess.Concrete.Contexts;

namespace Business.Features.OperationClaims.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommand:IRequest<UpdatedOperationClaimDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] Roles => new[] {Admin,OperationClaimUpdate};

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public UpdateOperationClaimCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(request.Id);
                await _operationClaimBusinessRules.OperationClaimNameShouldBeNotExists(request.Name);

                OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim updatedOperationClaim = await _unitOfWork.OperationClaimDal.UpdateAsync(mappedOperationClaim);
                UpdatedOperationClaimDto updateOperationClaimDto = _mapper.Map<UpdatedOperationClaimDto>(updatedOperationClaim);

                await _unitOfWork.SaveChangesAsync();

                return updateOperationClaimDto;
            }
        }
    }
}
