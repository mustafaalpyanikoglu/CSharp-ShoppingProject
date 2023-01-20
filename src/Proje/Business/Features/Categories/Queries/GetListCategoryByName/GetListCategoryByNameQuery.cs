using AutoMapper;
using Business.Features.Products.Models;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Features.Categories.Queries.GetListCategoryByName
{
    public class GetListCategoryByNameQuery : IRequest<CategoryListByNameModel>
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }

        public class GetListCategoryByNameQueryHandler : IRequestHandler<GetListCategoryByNameQuery, CategoryListByNameModel>
        {
            private readonly ICategoryDal _CategoryDal;
            private readonly IMapper _mapper;

            public GetListCategoryByNameQueryHandler(ICategoryDal CategoryDal, IMapper mapper)
            {
                _CategoryDal = CategoryDal;
                _mapper = mapper;
            }

            public async Task<CategoryListByNameModel> Handle(GetListCategoryByNameQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Category> Categorys = await _CategoryDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      null,
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                CategoryListByNameModel mappedCategoryListModel = _mapper.Map<CategoryListByNameModel>(Categorys);
                return mappedCategoryListModel;
            }
        }
    }
}
