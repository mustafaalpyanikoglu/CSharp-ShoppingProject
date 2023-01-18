using AutoMapper;
using Business.Features.Users.Models;
using Core.Business.Pipelines.Authorization;
using Core.Business.Requests;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Users.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Users.Queries.GetListUser
{
    public class GetListUserQuery : IRequest<UserListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public string[] Roles => new[] { Admin, UserGet };

        public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, UserListModel>
        {
            private readonly IUserDal _userDal;
            private readonly IMapper _mapper;

            public GetListUserQueryHandler(IUserDal userDal, IMapper mapper)
            {
                _userDal = userDal;
                _mapper = mapper;
            }

            public async Task<UserListModel> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User> users = await _userDal.GetListAsync(index: request.PageRequest.Page,
                                                                    size: request.PageRequest.PageSize);
                UserListModel mappedUserListModel = _mapper.Map<UserListModel>(users);
                return mappedUserListModel;
            }
        }
    }
}
