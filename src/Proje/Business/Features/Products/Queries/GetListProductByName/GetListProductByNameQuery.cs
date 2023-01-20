using AutoMapper;
using Business.Features.Products.Models;
using Business.Features.Products.Queries.GetListProductByDynamic;
using Business.Features.Products.Rules;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Features.Products.Queries.GetListProductByName
{
    public class GetListProductByNameQuery : IRequest<ProductListByNameModel>
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }
        public class GetListProductByNameQueryHandler : IRequestHandler<GetListProductByNameQuery, ProductListByNameModel>
        {
            private readonly IProductDal _productDal;
            private readonly IMapper _mapper;

            public GetListProductByNameQueryHandler(IProductDal productDal, IMapper mapper)
            {
                _productDal = productDal;
                _mapper = mapper;
            }

            public async Task<ProductListByNameModel> Handle(GetListProductByNameQuery request, CancellationToken cancellationToken)
            {

                IPaginate<Product> products = await _productDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      include: x => x.Include(c => c.Category),
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                ProductListByNameModel mappedProductListModel = _mapper.Map<ProductListByNameModel>(products);
                return mappedProductListModel;
            }
        }
    }
}
