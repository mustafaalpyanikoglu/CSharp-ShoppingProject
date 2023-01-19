using AutoMapper;
using Business.Features.Categorys.Rules;
using Business.Features.Products.Dtos;
using Business.Features.Products.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Products.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<CreatedProductDto>//, ISecuredRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public string[] Roles => new[] { Admin, ProductAdd };

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductDto>
        {
            private readonly IMapper _mapper;
            private readonly IProductDal _productDal;
            private readonly ProductBusinessRules _productBusinessRules;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public CreateProductCommandHandler(IMapper mapper, IProductDal productDal, ProductBusinessRules productBusinessRules, CategoryBusinessRules categoryBusinessRules)
            {
                _mapper = mapper;
                _productDal = productDal;
                _productBusinessRules = productBusinessRules;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<CreatedProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                await _productBusinessRules.ProductNameShouldBeNotExists(request.Name);
                await _categoryBusinessRules.CategoryIdShouldExistWhenSelected(request.CategoryId);

                Product mappedProduct= _mapper.Map<Product>(request);
                Product createdProduct = await _productDal.AddAsync(mappedProduct);
                CreatedProductDto createProductDto = _mapper.Map<CreatedProductDto>(createdProduct);

                return createProductDto;
            }
        }
    }
}
