using AutoMapper;
using Business.Features.UserCarts.Dtos;
using Business.Features.UserCarts.Rules;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using static Business.Features.UserCarts.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserCarts.Commands.UpdateUserCart
{
    public class UpdateUserCartCommand : IRequest<UpdatedUserCartDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string[] Roles => new[] { Admin, UserCartUpdate };

        public class UpdateUserCartCommandHandler : IRequestHandler<UpdateUserCartCommand, UpdatedUserCartDto>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly UserCartBusinessRules _userCartBusinessRules;
            private readonly UserBusinessRules _userBusinessRules;

            public UpdateUserCartCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, UserCartBusinessRules userCartBusinessRules, UserBusinessRules userBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _userCartBusinessRules = userCartBusinessRules;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UpdatedUserCartDto> Handle(UpdateUserCartCommand request, CancellationToken cancellationToken)
            {
                await _userCartBusinessRules.UserCartIdShouldExistWhenSelected(request.Id);
                await _userBusinessRules.UserIdMustBeAvailable(request.UserId);

                UserCart mappedUserCart = _mapper.Map<UserCart>(request);
                UserCart UpdatedUserCart = await _unitOfWork.UserCartDal.UpdateAsync(mappedUserCart);
                UpdatedUserCartDto UpdateUserCartDto = _mapper.Map<UpdatedUserCartDto>(UpdatedUserCart);

                await _unitOfWork.SaveChangesAsync();

                return UpdateUserCartDto;
            }
        }
    }
}
