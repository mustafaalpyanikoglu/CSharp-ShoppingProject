using AutoMapper;
using Business.Features.Purses.Models;
using Business.Features.Purses.Queries.GetListPurseByDynamic;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Purses.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Purses.Queries.GetListPurseByDynamic
{
    public class GetListPurseByDynamicQuery : IRequest<PurseListModel>,ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }

        public string[] Roles => new[] { Admin };

        public class GetListPurseByDynamicQueryHandler : IRequestHandler<GetListPurseByDynamicQuery, PurseListModel>
        {
            private readonly IPurseDal _purseDal;
            private readonly IMapper _mapper;

            public GetListPurseByDynamicQueryHandler(IPurseDal PurseDal, IMapper mapper)
            {
                _purseDal = PurseDal;
                _mapper = mapper;
            }

            public async Task<PurseListModel> Handle(GetListPurseByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Purse> Purses = await _purseDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      include: x => x.Include(c => c.User),
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                PurseListModel mappedPurseListModel = _mapper.Map<PurseListModel>(Purses);
                return mappedPurseListModel;
            }
        }
    }
}
