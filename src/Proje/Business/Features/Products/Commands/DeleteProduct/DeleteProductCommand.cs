using AutoMapper;
using Business.Features.Products.Dtos;
using Business.Features.Products.Rules;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Products.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<DeletedProductDto>//, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin, ProductDelete };

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeletedProductDto>
        {
            private readonly IMapper _mapper;
            private readonly IProductDal _productDal;
            private readonly ProductBusinessRules _productBusinessRules;

            public DeleteProductCommandHandler(IMapper mapper, IProductDal productDal, ProductBusinessRules productBusinessRules)
            {
                _mapper = mapper;
                _productDal = productDal;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<DeletedProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                await _productBusinessRules.ProductIdShouldExistWhenSelected(request.Id);

                Product mappedProduct = _mapper.Map<Product>(request);
                Product DeletedProduct = await _productDal.DeleteAsync(mappedProduct);
                DeletedProductDto deleteProductDto = _mapper.Map<DeletedProductDto>(DeletedProduct);

                return deleteProductDto;
            }
        }
    }
}
