using AutoMapper;
using Business.Features.Purses.Dtos;
using Business.Features.Purses.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using static Business.Features.Purses.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Purses.Commands.DeletePurse
{
    public class DeletePurseCommand : IRequest<DeletedPurseDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin };

        public class DeletePurseCommandHandler : IRequestHandler<DeletePurseCommand, DeletedPurseDto>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly PurseBusinessRules _purseBusinessRules;

            public DeletePurseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, PurseBusinessRules purseBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _purseBusinessRules = purseBusinessRules;
            }

            public async Task<DeletedPurseDto> Handle(DeletePurseCommand request, CancellationToken cancellationToken)
            {
                await _purseBusinessRules.PurseIdShouldExistWhenSelected(request.Id);

                Purse mappedPurse = _mapper.Map<Purse>(request);
                Purse DeletedPurse = await _unitOfWork.PurseDal.DeleteAsync(mappedPurse);
                DeletedPurseDto DeletePurseDto = _mapper.Map<DeletedPurseDto>(DeletedPurse);

                await _unitOfWork.SaveChangesAsync();

                return DeletePurseDto;
            }
        }
    }
}
