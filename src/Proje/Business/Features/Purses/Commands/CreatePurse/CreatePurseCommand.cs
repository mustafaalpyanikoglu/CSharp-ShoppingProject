using AutoMapper;
using Business.Features.Purses.Dtos;
using Business.Features.Purses.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Purses.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;
using Business.Features.Users.Rules;

namespace Business.Features.Purses.Commands.CreatePurse
{
    public class CreatePurseCommand:IRequest<CreatedPurseDto>,ISecuredRequest
    {
        public int UserId { get; set; }
        public float Money { get; set; }

        public string[] Roles => new[] { Admin };

        public class CreatePurseCommandHandler : IRequestHandler<CreatePurseCommand, CreatedPurseDto>
        {
            private readonly IMapper _mapper;
            private readonly IPurseDal _purseDal;
            private readonly PurseBusinessRules _purseBusinessRules;
            private readonly UserBusinessRules _userBusinessRules;

            public CreatePurseCommandHandler(IMapper mapper, IPurseDal purseDal, PurseBusinessRules purseBusinessRules, UserBusinessRules userBusinessRules)
            {
                _mapper = mapper;
                _purseDal = purseDal;
                _purseBusinessRules = purseBusinessRules;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<CreatedPurseDto> Handle(CreatePurseCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdMustBeAvailable(request.UserId);

                Purse mappedPurse = _mapper.Map<Purse>(request);
                Purse createdPurse = await _purseDal.AddAsync(mappedPurse);
                CreatedPurseDto createPurseDto = _mapper.Map<CreatedPurseDto>(createdPurse);

                return createPurseDto;
            }
        }
    }
}
