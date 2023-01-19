using AutoMapper;
using Business.Features.Users.Dtos;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Users.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Users.Command.UpdateUser
{
    public class UpdateUserCommand:IRequest<UpdatedUserDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public string[] Roles => new[] { Admin, UserUpdate };

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserDto>
        {
            private readonly IUserDal _userDal;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public UpdateUserCommandHandler(IUserDal userDal, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userDal = userDal;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UpdatedUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdShouldExistWhenSelected(request.Id);
                await _userBusinessRules.UserNameMustBePresent(request.UserName);

                User mappedUser = _mapper.Map<User>(request);

                User? user = await _userDal.GetAsync(u => u.Id == request.Id);
                mappedUser.PasswordHash = user.PasswordHash;
                mappedUser.PasswordSalt = user.PasswordSalt;

                User updatedUser = await _userDal.UpdateAsync(mappedUser);
                UpdatedUserDto updatedUserDto = _mapper.Map<UpdatedUserDto>(updatedUser);
                return updatedUserDto;
            }
        }
    }
}
