using AutoMapper;
using Business.Features.Products.Models;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;

namespace Business.Features.Categories.Queries.GetListCategory
{
    public class GetListCategoryQuery : IRequest<CategoryListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListCategoryQueryHanlder : IRequestHandler<GetListCategoryQuery, CategoryListModel>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListCategoryQueryHanlder(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CategoryListModel> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Category> Categorys = await _unitOfWork.CategoryDal.GetListAsync(index: request.PageRequest.Page,
                                                                                                  size: request.PageRequest.PageSize);
                CategoryListModel mappedCategoryListModel = _mapper.Map<CategoryListModel>(Categorys);
                return mappedCategoryListModel;

            }
        }
    }
}
