using AutoMapper;
using Business.Features.OperationClaims.Dtos;
using Business.Features.OperationClaims.Rules;
using DataAccess.Abstract;
using MediatR;
using Entities.Concrete;
using static Business.Features.OperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.EfUnitOfWork;

namespace Business.Features.OperationClaims.Commands.DeleteOperationClaim
{
    public class DeleteOperationClaimCommand:IRequest<DeletedOperationClaimDto>,ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] {Admin,OperationClaimDelete};

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public DeleteOperationClaimCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(request.Id);

                OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim deletedOperationClaim = await _unitOfWork.OperationClaimDal.DeleteAsync(mappedOperationClaim);
                DeletedOperationClaimDto deleteOperationClaimDto = _mapper.Map<DeletedOperationClaimDto>(deletedOperationClaim);

                await _unitOfWork.SaveChangesAsync();

                return deleteOperationClaimDto;
            }
        }
    }
}
