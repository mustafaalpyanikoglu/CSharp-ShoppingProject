using AutoMapper;
using Business.Features.OperationClaims.Models;
using Core.Business.Pipelines.Authorization;
using Core.Business.Requests;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.OperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OperationClaims.Queries.GetListOperationClaimByDynamic
{
    public class GetListOperationClaimByDynamicQuery:IRequest<OperationClaimListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }
        public string[] Roles => new[] { Admin, OperationClaimGet };

        public class GetListOperationClaimByDynamicQueryHandler : IRequestHandler<GetListOperationClaimByDynamicQuery, OperationClaimListModel>
        {
            private readonly IOperationClaimDal _operationClaimDal;
            private readonly IMapper _mapper;

            public GetListOperationClaimByDynamicQueryHandler(IOperationClaimDal operationClaimDal, IMapper mapper)
            {
                _operationClaimDal = operationClaimDal;
                _mapper = mapper;
            }

            public async Task<OperationClaimListModel> Handle(GetListOperationClaimByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await _operationClaimDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      null,
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                OperationClaimListModel mappedCarListModel = _mapper.Map<OperationClaimListModel>(operationClaims);
                return mappedCarListModel;
            }
        }
    }
}
