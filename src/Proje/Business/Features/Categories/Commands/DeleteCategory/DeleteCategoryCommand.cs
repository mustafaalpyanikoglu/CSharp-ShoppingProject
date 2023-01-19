using AutoMapper;
using Business.Features.Categories.Dtos;
using Business.Features.Categorys.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Categories.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<DeletedCategoryDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin, CategoryDelete };

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeletedCategoryDto>
        {
            private readonly ICategoryDal _categoryDal;
            private readonly IMapper _mapper;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public DeleteCategoryCommandHandler(ICategoryDal categoryDal, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                _categoryDal = categoryDal;
                _mapper = mapper;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<DeletedCategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                await _categoryBusinessRules.CategoryIdShouldExistWhenSelected(request.Id);

                Category mappedCategory = _mapper.Map<Category>(request);
                Category DeletedCategory = await _categoryDal.DeleteAsync(mappedCategory);
                DeletedCategoryDto DeleteCategoryDto = _mapper.Map<DeletedCategoryDto>(DeletedCategory);

                return DeleteCategoryDto;
            }
        }
    }
}
