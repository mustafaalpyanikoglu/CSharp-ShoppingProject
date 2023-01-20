using AutoMapper;
using Business.Features.Purses.Commands.DeletePurse;
using Business.Features.Purses.Dtos;
using Business.Features.Purses.Rules;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
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
            private readonly IPurseDal _purseDal;
            private readonly PurseBusinessRules _purseBusinessRules;

            public DeletePurseCommandHandler(IMapper mapper, IPurseDal purseDal, PurseBusinessRules purseBusinessRules)
            {
                _mapper = mapper;
                _purseDal = purseDal;
                _purseBusinessRules = purseBusinessRules;
            }

            public async Task<DeletedPurseDto> Handle(DeletePurseCommand request, CancellationToken cancellationToken)
            {
                await _purseBusinessRules.PurseIdShouldExistWhenSelected(request.Id);

                Purse mappedPurse = _mapper.Map<Purse>(request);
                Purse DeletedPurse = await _purseDal.DeleteAsync(mappedPurse);
                DeletedPurseDto DeletePurseDto = _mapper.Map<DeletedPurseDto>(DeletedPurse);

                return DeletePurseDto;
            }
        }
    }
}
