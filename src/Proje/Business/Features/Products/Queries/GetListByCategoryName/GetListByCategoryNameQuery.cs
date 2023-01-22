using AutoMapper;
using Business.Features.Products.Models;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Features.Products.Queries.GetListProductByName
{
    public class GetListByCategoryNameQuery : IRequest<ProductListByNameModel>
    {
        public string CategoryName { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListByCategoryNameQueryHandlder : IRequestHandler<GetListByCategoryNameQuery, ProductListByNameModel>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListByCategoryNameQueryHandlder(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProductListByNameModel> Handle(GetListByCategoryNameQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Product> Products = await _unitOfWork.ProductDal.GetListAsync(
                    p=> p.Category.Name == request.CategoryName,
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    include: x => x.Include(c => c.Category));
                ProductListByNameModel mappedProductListModel = _mapper.Map<ProductListByNameModel>(Products);
                return mappedProductListModel;
            }
        }
    }

}
