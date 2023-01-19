using AutoMapper;
using Business.Features.Products.Models;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Features.Categories.Queries.GetListCategoryByDynamic
{
    public class GetListCategoryByDynamicQuery : IRequest<CategoryListModel>
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }

        public class GetListCategoryByDynamicQueryHandler : IRequestHandler<GetListCategoryByDynamicQuery, CategoryListModel>
        {
            private readonly ICategoryDal _CategoryDal;
            private readonly IMapper _mapper;

            public GetListCategoryByDynamicQueryHandler(ICategoryDal CategoryDal, IMapper mapper)
            {
                _CategoryDal = CategoryDal;
                _mapper = mapper;
            }

            public async Task<CategoryListModel> Handle(GetListCategoryByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Category> Categorys = await _CategoryDal.GetListByDynamicAsync(
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
