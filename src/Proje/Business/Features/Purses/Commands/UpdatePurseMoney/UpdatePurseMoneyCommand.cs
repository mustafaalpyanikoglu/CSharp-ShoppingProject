using AutoMapper;
using Business.Features.Purses.Dtos;
using Business.Features.Purses.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using static Business.Features.Purses.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Purses.Commands.UpdatePurseMoney
{
    public class UpdatePurseMoneyCommand:IRequest<UpdatedPurseMoneyDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public float Money { get; set; }

        public string[] Roles => new[] { Admin,Customer };

        public class UpdatePurseMoneyCommandHandler : IRequestHandler<UpdatePurseMoneyCommand, UpdatedPurseMoneyDto>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly PurseBusinessRules _purseBusinessRules;

            public UpdatePurseMoneyCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, 
                PurseBusinessRules purseBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _purseBusinessRules = purseBusinessRules;
            }

            public async Task<UpdatedPurseMoneyDto> Handle(UpdatePurseMoneyCommand request, CancellationToken cancellationToken)
            {
                await _purseBusinessRules.PurseIdShouldExistWhenSelected(request.Id);

                Purse mappedPurse = _mapper.Map<Purse>(request);
                Purse UpdatedPurse = await _unitOfWork.PurseDal.UpdateAsync(mappedPurse);
                UpdatedPurseMoneyDto UpdatePurseDto = _mapper.Map<UpdatedPurseMoneyDto>(UpdatedPurse);

                await _unitOfWork.SaveChangesAsync();

                return UpdatePurseDto;
            }
        }
    }
}
