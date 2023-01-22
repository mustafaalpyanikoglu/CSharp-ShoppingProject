using AutoMapper;
using Business.Features.Purses.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Concrete.Contexts;
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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListPurseByDynamicQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<PurseListModel> Handle(GetListPurseByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Purse> Purses = await _unitOfWork.PurseDal.GetListByDynamicAsync(
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
