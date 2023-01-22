using AutoMapper;
using Business.Features.UserCarts.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.UserCarts.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserCarts.Queries.GetListUserCartByDynamic
{
    public class GetListUserCartByDynamicQuery : IRequest<UserCartListModel>,ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }

        public string[] Roles => new[] { Admin, UserCartGet };

        public class GetListUserCartByDynamicQueryHandler : IRequestHandler<GetListUserCartByDynamicQuery, UserCartListModel>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListUserCartByDynamicQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<UserCartListModel> Handle(GetListUserCartByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserCart> UserCarts = await _unitOfWork.UserCartDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      include: x => x.Include(c => c.User),
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                UserCartListModel mappedUserCartListModel = _mapper.Map<UserCartListModel>(UserCarts);
                return mappedUserCartListModel;
            }
        }
    }
}
