using AutoMapper;
using Business.Features.UserOperationClaims.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.UserOperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserOperationClaims.Queries.GetListUserOperationClaimByDynamic
{
    public class GetListUserOperationClaimByDynamicQuery:IRequest<UserOperationClaimListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }
        public string[] Roles => new[] {Admin, UserOperationClaimGet };

        public class GetListUserOperationClaimByDynamicQueryHandler : IRequestHandler<GetListUserOperationClaimByDynamicQuery, UserOperationClaimListModel>
        {
            private readonly IUserOperationClaimDal _userOperationClaimDal;
            private readonly IMapper _mapper;

            public GetListUserOperationClaimByDynamicQueryHandler(IUserOperationClaimDal userOperationClaimDal, IMapper mapper)
            {
                _userOperationClaimDal = userOperationClaimDal;
                _mapper = mapper;
            }

            public async Task<UserOperationClaimListModel> Handle(GetListUserOperationClaimByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      include: c => c.Include(c => c.User).Include(c => c.OperationClaim),
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                UserOperationClaimListModel mappedUserOperationClaimListModel = _mapper.Map<UserOperationClaimListModel>(userOperationClaims);
                return mappedUserOperationClaimListModel;
            }
        }
    }
}
