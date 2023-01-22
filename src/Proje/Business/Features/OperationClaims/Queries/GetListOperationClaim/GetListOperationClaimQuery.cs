using AutoMapper;
using Business.Features.OperationClaims.Models;
using Business.Features.OperationClaims.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using DataAccess.Concrete.EfUnitOfWork;
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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListOperationClaimQueryHanlder(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<OperationClaimListModel> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await _unitOfWork.OperationClaimDal.GetListAsync(index: request.PageRequest.Page,
                                                                                                  size: request.PageRequest.PageSize);
                OperationClaimListModel mappedOperationClaimListModel = _mapper.Map<OperationClaimListModel>(operationClaims);
                return mappedOperationClaimListModel;

            }
        }
    }
}
