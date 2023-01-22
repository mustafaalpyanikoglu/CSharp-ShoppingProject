using AutoMapper;
using Business.Features.Products.Dtos;
using Business.Features.Products.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Products.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQuery : IRequest<ProductDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin, Customer};

        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ProductDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ProductBusinessRules _productBusinessRules;

            public GetByIdProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ProductBusinessRules productBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<ProductDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
                await _productBusinessRules.ProductIdShouldExistWhenSelected(request.Id);

                Product? Product = await _unitOfWork.ProductDal.GetAsync(m => m.Id == request.Id,
                                                              include: x=>x.Include(c=> c.Category));
                ProductDto ProductDto = _mapper.Map<ProductDto>(Product);

                return ProductDto;
            }
        }
    }
}
