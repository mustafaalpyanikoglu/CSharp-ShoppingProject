using AutoMapper;
using Business.Features.Products.Dtos;
using Business.Features.Products.Rules;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Products.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Products.Queries.GetByNameProduct
{
    public class GetByNameProductQuery : IRequest<ProductDto>
    {
        public string ProductName { get; set; }

        public class GetByNameProductQueryHandler : IRequestHandler<GetByNameProductQuery, ProductDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ProductBusinessRules _productBusinessRules;

            public GetByNameProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ProductBusinessRules productBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<ProductDto> Handle(GetByNameProductQuery request, CancellationToken cancellationToken)
            {
                await _productBusinessRules.ProductNameShouldExistWhenSelected(request.ProductName);

                Product? Product = await _unitOfWork.ProductDal.GetAsync(m => m.Name == request.ProductName,
                                                              include: x=>x.Include(c=> c.Category));
                ProductDto ProductDto = _mapper.Map<ProductDto>(Product);

                return ProductDto;
            }
        }
    }
}
