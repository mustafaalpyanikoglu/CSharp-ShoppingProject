using AutoMapper;
using Business.Features.Users.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Users.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Users.Queries.GetListUserByDynamic
{
    public class GetListUserByDynamicQuery:IRequest<UserListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }
        public string[] Roles => new[] { Admin, UserGet };

        public class GetListUserByDynamicQueryHandler : IRequestHandler<GetListUserByDynamicQuery, UserListModel>
        {
            private readonly IUserDal _userDal;
            private readonly IMapper _mapper;

            public GetListUserByDynamicQueryHandler(IUserDal userDal, IMapper mapper)
            {
                _userDal = userDal;
                _mapper = mapper;
            }

            public async Task<UserListModel> Handle(GetListUserByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User> userOperationClaims = await _userDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      include: c => c.Include(c => c.UserOperationClaims),
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                UserListModel mappedUserListModel = _mapper.Map<UserListModel>(userOperationClaims);
                return mappedUserListModel;
            }
        }
    }
}
