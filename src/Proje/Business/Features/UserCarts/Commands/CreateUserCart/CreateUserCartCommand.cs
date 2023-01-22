using AutoMapper;
using Business.Features.UserCarts.Dtos;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
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
            private readonly IUserCartDal _userCartDal;
            private readonly UserBusinessRules _userBusinessRules;

            public CreateUserCartCommandHandler(IMapper mapper, IUserCartDal userCartDal, UserBusinessRules userBusinessRules)
            {
                _mapper = mapper;
                _userCartDal = userCartDal;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<CreatedUserCartDto> Handle(CreateUserCartCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdMustBeAvailable(request.UserId);
                await _userBusinessRules.ShouldNotHaveUserCart(request.UserId);

                UserCart mappedUserCart = _mapper.Map<UserCart>(request);
                UserCart createdUserCart = await _userCartDal.AddAsync(mappedUserCart);
                CreatedUserCartDto createUserCartDto = _mapper.Map<CreatedUserCartDto>(createdUserCart);

                return createUserCartDto;
            }
        }
    }
}
