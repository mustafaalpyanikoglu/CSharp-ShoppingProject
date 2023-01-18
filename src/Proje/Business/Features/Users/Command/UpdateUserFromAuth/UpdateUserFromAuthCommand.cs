using AutoMapper;
using Business.Features.Users.Dtos;
using Business.Features.Users.Rules;
using Business.Services.AuthService;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Features.Users.Command.UpdateUserFromAuth
{
    public class UpdateUserFromAuthCommand:IRequest<UpdatedUserFromAuthDto>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string? NewPassword { get; set; }

        public class UpdateUserFromAuthCommandHandler : IRequestHandler<UpdateUserFromAuthCommand, UpdatedUserFromAuthDto>
        {
            private readonly IUserDal _userDal;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IAuthService _authService;

            public UpdateUserFromAuthCommandHandler(IUserDal userDal, IMapper mapper, UserBusinessRules userBusinessRules, IAuthService authService)
            {
                _userDal = userDal;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
                _authService = authService;
            }

            public async Task<UpdatedUserFromAuthDto> Handle(UpdateUserFromAuthCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userDal.GetAsync(u => u.Id == request.Id);
                await _userBusinessRules.UserShouldBeExist(user);
                await _userBusinessRules.UserPasswordShouldBeMatch(user, request.Password);

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                if (request.NewPassword is not null && !string.IsNullOrWhiteSpace(request.NewPassword))
                {
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(request.NewPassword, out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }
                User updatedUser = await _userDal.UpdateAsync(user);
                UpdatedUserFromAuthDto updatedUserFromAuthDto = _mapper.Map<UpdatedUserFromAuthDto>(updatedUser);
                updatedUserFromAuthDto.AccessToken = await _authService.CreateAccessToken(user);
                return updatedUserFromAuthDto;
            }
        }
    }
}
