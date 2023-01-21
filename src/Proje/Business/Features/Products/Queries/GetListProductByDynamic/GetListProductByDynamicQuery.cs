﻿using AutoMapper;
using Business.Features.Products.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Products.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Products.Queries.GetListProductByDynamic
{
    public class GetListProductByDynamicQuery : IRequest<ProductListModel>,ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }

        public string[] Roles => new[] { Admin, Customer };

        public class GetListProductByDynamicQueryHandler : IRequestHandler<GetListProductByDynamicQuery, ProductListModel>
        {
            private readonly IProductDal _productDal;
            private readonly IMapper _mapper;

            public GetListProductByDynamicQueryHandler(IProductDal productDal, IMapper mapper)
            {
                _productDal = productDal;
                _mapper = mapper;
            }

            public async Task<ProductListModel> Handle(GetListProductByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Product> products = await _productDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      include: x => x.Include(c => c.Category),
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                ProductListModel mappedProductListModel = _mapper.Map<ProductListModel>(products);
                return mappedProductListModel;
            }
        }
    }
}
