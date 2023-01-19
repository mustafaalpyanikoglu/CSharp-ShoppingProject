using AutoMapper;
using Business.Features.Products.Models;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Features.Products.Queries.GetListProduct
{
    public class GetListProductQuery : IRequest<ProductListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListProductQueryHanlder : IRequestHandler<GetListProductQuery, ProductListModel>
        {
            private readonly IProductDal _productDal;
            private readonly IMapper _mapper;

            public GetListProductQueryHanlder(IProductDal productDal, IMapper mapper)
            {
                _productDal = productDal;
                _mapper = mapper;
            }

            public async Task<ProductListModel> Handle(GetListProductQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Product> Products = await _productDal.GetListAsync(index: request.PageRequest.Page,
                                                                             size: request.PageRequest.PageSize,
                                                                             include: x => x.Include(c => c.Category));
                ProductListModel mappedProductListModel = _mapper.Map<ProductListModel>(Products);
                return mappedProductListModel;

            }
        }
    }
}
