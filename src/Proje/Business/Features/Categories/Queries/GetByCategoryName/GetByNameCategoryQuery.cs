using AutoMapper;
using Business.Features.Categories.Dtos;
using Business.Features.Categorys.Rules;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Features.Categories.Queries.GetListCategoryByName
{
    public class GetByNameCategoryQuery : IRequest<CategoryDto>
    {
        public string CategoryName { get; set; }

        public class GetByNameCategoryQueryHandler : IRequestHandler<GetByNameCategoryQuery, CategoryDto>
        {
            private readonly ICategoryDal _CategoryDal;
            private readonly IMapper _mapper;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public GetByNameCategoryQueryHandler(ICategoryDal categoryDal, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                _CategoryDal = categoryDal;
                _mapper = mapper;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<CategoryDto> Handle(GetByNameCategoryQuery request, CancellationToken cancellationToken)
            {
                await _categoryBusinessRules.CategoryNameShouldExistWhenSelected(request.CategoryName);

                Category? category  = await _CategoryDal.GetAsync(c=> c.Name == request.CategoryName);
                CategoryDto CategoryDto = _mapper.Map<CategoryDto>(category);

                return CategoryDto;
            }
        }
    }
}
