using AutoMapper;
using Business.Features.Categories.Dtos;
using Business.Features.Categorys.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<DeletedCategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                await _categoryBusinessRules.CategoryIdShouldExistWhenSelected(request.Id);

                Category mappedCategory = _mapper.Map<Category>(request);
                Category DeletedCategory = await _unitOfWork.CategoryDal.DeleteAsync(mappedCategory);
                DeletedCategoryDto DeleteCategoryDto = _mapper.Map<DeletedCategoryDto>(DeletedCategory);

                await _unitOfWork.SaveChangesAsync();

                return DeleteCategoryDto;
            }
        }
    }
}
