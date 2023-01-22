using AutoMapper;
using Business.Features.Purses.Dtos;
using Business.Features.Purses.Rules;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using static Business.Features.Purses.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Purses.Commands.DeletePurse
{
    public class UpdatePurseCommand : IRequest<UpdatedPurseDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Money { get; set; }

        public string[] Roles => new[] { Admin };

        public class UpdatePurseCommandHandler : IRequestHandler<UpdatePurseCommand, UpdatedPurseDto>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly PurseBusinessRules _purseBusinessRules;
            private readonly UserBusinessRules _userBusinessRules;

            public UpdatePurseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, 
                PurseBusinessRules purseBusinessRules, UserBusinessRules userBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _purseBusinessRules = purseBusinessRules;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UpdatedPurseDto> Handle(UpdatePurseCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdMustBeAvailable(request.UserId);
                await _purseBusinessRules.PurseIdShouldExistWhenSelected(request.Id);

                Purse mappedPurse = _mapper.Map<Purse>(request);
                Purse UpdatedPurse = await _unitOfWork.PurseDal.UpdateAsync(mappedPurse);
                UpdatedPurseDto UpdatePurseDto = _mapper.Map<UpdatedPurseDto>(UpdatedPurse);

                await _unitOfWork.SaveChangesAsync();

                return UpdatePurseDto;
            }
        }
    }
}
