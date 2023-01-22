using AutoMapper;
using Business.Features.Products.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using static Business.Features.Categories.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Categories.Queries.GetListCategoryByDynamic
{
    public class GetListCategoryByDynamicQuery : IRequest<CategoryListModel>,ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }

        public string[] Roles => new[] { Admin, CategoryGet };

        public class GetListCategoryByDynamicQueryHandler : IRequestHandler<GetListCategoryByDynamicQuery, CategoryListModel>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListCategoryByDynamicQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CategoryListModel> Handle(GetListCategoryByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Category> Categorys = await _unitOfWork.CategoryDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      null,
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                CategoryListModel mappedCategoryListModel = _mapper.Map<CategoryListModel>(Categorys);
                return mappedCategoryListModel;
            }
        }
    }
}
