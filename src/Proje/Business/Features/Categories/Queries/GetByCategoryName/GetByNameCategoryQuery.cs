using AutoMapper;
using Business.Features.Categories.Dtos;
using Business.Features.Categorys.Rules;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;

namespace Business.Features.Categories.Queries.GetListCategoryByName
{
    public class GetByNameCategoryQuery : IRequest<CategoryDto>
    {
        public string CategoryName { get; set; }

        public class GetByNameCategoryQueryHandler : IRequestHandler<GetByNameCategoryQuery, CategoryDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public GetByNameCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<CategoryDto> Handle(GetByNameCategoryQuery request, CancellationToken cancellationToken)
            {
                await _categoryBusinessRules.CategoryNameShouldExistWhenSelected(request.CategoryName);

                Category? category  = await _unitOfWork.CategoryDal.GetAsync(c=> c.Name == request.CategoryName);
                CategoryDto CategoryDto = _mapper.Map<CategoryDto>(category);

                return CategoryDto;
            }
        }
    }
}
