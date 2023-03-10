using AutoMapper;
using Business.Features.Categories.Dtos;
using Business.Features.Categorys.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using static Business.Features.Categories.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdCategoryQuery:IRequest<CategoryDto>,ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin, CategoryGet };

        public class GetByIdCategoryQueryHandler:IRequestHandler<GetByIdCategoryQuery,CategoryDto>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public GetByIdCategoryQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, CategoryBusinessRules categoryBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<CategoryDto> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
            {
                await _categoryBusinessRules.CategoryIdShouldExistWhenSelected(request.Id);

                Category? category = await _unitOfWork.CategoryDal.GetAsync(m => m.Id == request.Id);
                CategoryDto CategoryDto = _mapper.Map<CategoryDto>(category);

                return CategoryDto;
            }
        }
    }
}
