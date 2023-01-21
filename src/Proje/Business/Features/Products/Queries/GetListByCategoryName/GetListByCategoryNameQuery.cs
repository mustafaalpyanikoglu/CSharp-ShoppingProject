using AutoMapper;
using Business.Features.Products.Dtos;
using Business.Features.Products.Models;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
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
            private readonly IProductDal _productDal;
            private readonly IMapper _mapper;

            public GetListByCategoryNameQueryHandlder(IProductDal productDal, IMapper mapper)
            {
                _productDal = productDal;
                _mapper = mapper;
            }

            public async Task<ProductListByNameModel> Handle(GetListByCategoryNameQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Product> Products = await _productDal.GetListAsync(
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
