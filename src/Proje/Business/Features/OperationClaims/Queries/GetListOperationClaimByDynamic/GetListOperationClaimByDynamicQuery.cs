using AutoMapper;
using Business.Features.OperationClaims.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListOperationClaimByDynamicQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<OperationClaimListModel> Handle(GetListOperationClaimByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await _unitOfWork.OperationClaimDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      null,
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                OperationClaimListModel mappedOperationClaimListModel = _mapper.Map<OperationClaimListModel>(operationClaims);
                return mappedOperationClaimListModel;
            }
        }
    }
}
