using AutoMapper;
using Business.Features.UserOperationClaims.Models;
using Business.Features.UserOperationClaims.Rules;
using Core.Business.Pipelines.Authorization;
using Core.Business.Requests;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.UserOperationClaims.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserOperationClaims.Queries.GetListUserOperationClaim
{
    public class GetListUserUperationClaimQuery : IRequest<UserOperationClaimListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public string[] Roles => new[] {Admin, UserOperationClaimGet };

        public class GetListUserUperationClaimQueryHandler : IRequestHandler<GetListUserUperationClaimQuery, UserOperationClaimListModel>
        {
            private readonly IUserOperationClaimDal _userOperationClaimDal;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public GetListUserUperationClaimQueryHandler(IUserOperationClaimDal userOperationClaimDal, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimDal = userOperationClaimDal;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<UserOperationClaimListModel> Handle(GetListUserUperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimDal.GetListAsync(
                    include:c => c.Include(c => c.User).Include(c => c.OperationClaim),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);

                UserOperationClaimListModel mappedUserOperationClaimListModel = _mapper.Map<UserOperationClaimListModel>(userOperationClaims);
                return mappedUserOperationClaimListModel;
            }
        }
    }
}
