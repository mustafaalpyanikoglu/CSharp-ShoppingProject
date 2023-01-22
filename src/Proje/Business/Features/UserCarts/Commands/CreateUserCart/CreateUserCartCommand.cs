using AutoMapper;
using Business.Features.UserCarts.Dtos;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using static Business.Features.UserCarts.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserCarts.Commands.CreateUserCart
{
    public class CreateUserCartCommand : IRequest<CreatedUserCartDto>, ISecuredRequest
    {
        public int UserId { get; set; }

        public string[] Roles => new[] { Admin ,UserCartAdd};

        public class CreateUserCartCommandHandler : IRequestHandler<CreateUserCartCommand, CreatedUserCartDto>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly UserBusinessRules _userBusinessRules;

            public CreateUserCartCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, UserBusinessRules userBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<CreatedUserCartDto> Handle(CreateUserCartCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdMustBeAvailable(request.UserId);
                await _userBusinessRules.ShouldNotHaveUserCart(request.UserId);

                UserCart mappedUserCart = _mapper.Map<UserCart>(request);
                UserCart createdUserCart = await _unitOfWork.UserCartDal.AddAsync(mappedUserCart);
                CreatedUserCartDto createUserCartDto = _mapper.Map<CreatedUserCartDto>(createdUserCart);

                await _unitOfWork.SaveChangesAsync();

                return createUserCartDto;
            }
        }
    }
}
