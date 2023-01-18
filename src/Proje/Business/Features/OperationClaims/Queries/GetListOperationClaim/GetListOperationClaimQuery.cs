using AutoMapper;
using Business.Features.OperationClaims.Models;
using Business.Features.OperationClaims.Rules;
using Core.Business.Pipelines.Authorization;
using Core.Business.Requests;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.OperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OperationClaims.Queries.GetListOperationClaim
{
    public class GetListOperationClaimQuery : IRequest<OperationClaimListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public string[] Roles => new[] { Admin, OperationClaimGet };

        public class GetListOperationClaimQueryHanlder : IRequestHandler<GetListOperationClaimQuery, OperationClaimListModel>
        {
            private readonly IOperationClaimDal _operationClaimDal;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public GetListOperationClaimQueryHanlder(IOperationClaimDal operationClaimDal, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimDal = operationClaimDal;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<OperationClaimListModel> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
            {
                //await _operationClaimBusinessRules.OperationMustBeAvailable();
                IPaginate<OperationClaim> operationClaims = await _operationClaimDal.GetListAsync(index: request.PageRequest.Page,
                                                                                                  size: request.PageRequest.PageSize);
                OperationClaimListModel mappedOperationClaimListModel = _mapper.Map<OperationClaimListModel>(operationClaims);
                return mappedOperationClaimListModel;

            }
        }
    }
}
