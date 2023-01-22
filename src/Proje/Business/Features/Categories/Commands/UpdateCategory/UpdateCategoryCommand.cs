using AutoMapper;
using Business.Features.Categories.Dtos;
using Business.Features.Categorys.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using static Business.Features.Categories.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<UpdatedCategoryDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string[] Roles => new[] { Admin, CategoryUpdate };

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdatedCategoryDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<UpdatedCategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                await _categoryBusinessRules.CategoryIdShouldExistWhenSelected(request.Id);

                Category mappedCategory = _mapper.Map<Category>(request);
                Category UpdatedCategory = await _unitOfWork.CategoryDal.UpdateAsync(mappedCategory);
                UpdatedCategoryDto UpdateCategoryDto = _mapper.Map<UpdatedCategoryDto>(UpdatedCategory);

                await _unitOfWork.SaveChangesAsync();

                return UpdateCategoryDto;
            }
        }
    }
}
