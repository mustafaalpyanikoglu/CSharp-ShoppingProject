using AutoMapper;
using Business.Features.UserCarts.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.UserCarts.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserCarts.Queries.GetListUserCart
{
    public class GetListUserCartQuery : IRequest<UserCartListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { Admin,UserCartGet };

        public class GetListUserCartQueryHanlder : IRequestHandler<GetListUserCartQuery, UserCartListModel>
        {
            private readonly IUserCartDal _UserCartDal;
            private readonly IMapper _mapper;

            public GetListUserCartQueryHanlder(IUserCartDal UserCartDal, IMapper mapper)
            {
                _UserCartDal = UserCartDal;
                _mapper = mapper;
            }

            public async Task<UserCartListModel> Handle(GetListUserCartQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserCart> UserCarts = await _UserCartDal.GetListAsync(index: request.PageRequest.Page,
                                                                             size: request.PageRequest.PageSize,
                                                                             include: x => x.Include(c => c.User));
                UserCartListModel mappedUserCartListModel = _mapper.Map<UserCartListModel>(UserCarts);
                return mappedUserCartListModel;

            }
        }
    }
}
