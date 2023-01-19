using AutoMapper;
using Business.Features.Categorys.Rules;
using Business.Features.Products.Dtos;
using Business.Features.Products.Rules;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Products.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdatedProductDto>//, ISecuredRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public string[] Roles => new[] { Admin, ProductUpdate };

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdatedProductDto>
        {
            private readonly IMapper _mapper;
            private readonly IProductDal _productDal;
            private readonly ProductBusinessRules _productBusinessRules;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public UpdateProductCommandHandler(IMapper mapper, IProductDal productDal, ProductBusinessRules productBusinessRules, CategoryBusinessRules categoryBusinessRules)
            {
                _mapper = mapper;
                _productDal = productDal;
                _productBusinessRules = productBusinessRules;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<UpdatedProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                await _productBusinessRules.ProductIdShouldExistWhenSelected(request.Id);
                await _categoryBusinessRules.CategoryIdShouldExistWhenSelected(request.CategoryId);

                Product mappedProduct = _mapper.Map<Product>(request);
                Product updatedProduct = await _productDal.UpdateAsync(mappedProduct);
                UpdatedProductDto updateProductDto = _mapper.Map<UpdatedProductDto>(updatedProduct);

                return updateProductDto;
            }
        }
    }
}
