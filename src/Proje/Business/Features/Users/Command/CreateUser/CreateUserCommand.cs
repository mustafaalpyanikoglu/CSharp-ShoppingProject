using AutoMapper;
using Business.Features.Users.Dtos;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Security.Hashing;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using static Business.Features.Users.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Users.Command.CreateUser
{
    public class CreateUserCommand:IRequest<CreatedUserDto>, ISecuredRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string[] Roles => new[] { Admin, UserAdd };

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<CreatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserEmailMustNotExist(request.Email);

                User mappedUser = _mapper.Map<User>(request);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                mappedUser.PasswordHash = passwordHash;
                mappedUser.PasswordSalt = passwordSalt;

                mappedUser.RegistrationDate = Convert.ToDateTime(DateTime.Now.ToString("F"));
                User createdUser = await _unitOfWork.UserDal.AddAsync(mappedUser);
                CreatedUserDto createdUserDto = _mapper.Map<CreatedUserDto>(createdUser);

                await _unitOfWork.SaveChangesAsync();

                return createdUserDto;

            }
        }
    }
}
