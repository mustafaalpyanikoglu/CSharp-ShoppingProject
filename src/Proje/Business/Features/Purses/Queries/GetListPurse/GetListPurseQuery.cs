using AutoMapper;
using Business.Features.Purses.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Purses.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Purses.Queries.GetListPurse
{
    public class GetListPurseQuery : IRequest<PurseListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { Admin };

        public class GetListPurseQueryHanlder : IRequestHandler<GetListPurseQuery, PurseListModel>
        {
            private readonly IPurseDal _purseDal;
            private readonly IMapper _mapper;

            public GetListPurseQueryHanlder(IPurseDal purseDal, IMapper mapper)
            {
                _purseDal = purseDal;
                _mapper = mapper;
            }

            public async Task<PurseListModel> Handle(GetListPurseQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Purse> Purses = await _purseDal.GetListAsync(index: request.PageRequest.Page,
                                                                             size: request.PageRequest.PageSize,
                                                                             include: x => x.Include(c => c.User));
                PurseListModel mappedPurseListModel = _mapper.Map<PurseListModel>(Purses);
                return mappedPurseListModel;

            }
        }
    }
}
