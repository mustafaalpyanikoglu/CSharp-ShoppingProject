using AutoMapper;
using Business.Features.Categories.Dtos;
using Business.Features.Categorys.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Categories.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand:IRequest<CreatedCategoryDto>,ISecuredRequest
    {
        public string Name { get; set; }

        public string[] Roles => new[] { Admin, CategoryAdd };

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreatedCategoryDto>
        {
            private readonly ICategoryDal _categoryDal;
            private readonly IMapper _mapper;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public CreateCategoryCommandHandler(ICategoryDal categoryDal, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                _categoryDal = categoryDal;
                _mapper = mapper;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<CreatedCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                await _categoryBusinessRules.CategoryNameShouldBeNotExists(request.Name);

                Category mappedCategory = _mapper.Map<Category>(request);
                Category createdCategory = await _categoryDal.AddAsync(mappedCategory);
                CreatedCategoryDto createCategoryDto = _mapper.Map<CreatedCategoryDto>(createdCategory);

                return createCategoryDto;
            }
        }
    }
}
