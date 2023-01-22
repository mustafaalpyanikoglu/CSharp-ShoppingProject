using AutoMapper;
using Business.Features.Products.Models;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Concrete.EfUnitOfWork;
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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListProductQueryHanlder(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProductListModel> Handle(GetListProductQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Product> products = await _unitOfWork.ProductDal.GetListAsync(
                                                                             include: x => x.Include(c => c.Category),
                                                                             index: request.PageRequest.Page,
                                                                             size: request.PageRequest.PageSize);
                ProductListModel mappedProductListModel = _mapper.Map<ProductListModel>(products);
                return mappedProductListModel;

            }
        }
    }
}
