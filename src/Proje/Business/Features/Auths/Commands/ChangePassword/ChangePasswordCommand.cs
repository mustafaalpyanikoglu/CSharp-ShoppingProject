using Application.Features.Auths.Rules;
using AutoMapper;
using Business.Features.Auths.Dtos;
using Core.Application.Pipelines.Authorization;
using Core.Security.Hashing;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Auths.Commands.ChangePassword
{
    public class ChangePasswordCommand:IRequest<UserForChangePasswordDto>,ISecuredRequest
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }

        public string[] Roles => new[] { Admin,Customer };

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, UserForChangePasswordDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly AuthBusinessRules _authBusinessRules;

            public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, AuthBusinessRules authBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<UserForChangePasswordDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.PasswordsEnteredMustBeTheSame(request.NewPassword,request.RepeatPassword);
                await _authBusinessRules.UserEmailMustBeAvailable(request.Email);

                byte[] passwordHash, passwordSalt;

                User? user = await _unitOfWork.UserDal.GetAsync(u => u.Email == request.Email);

                HashingHelper.CreatePasswordHash(request.NewPassword, out passwordHash, out passwordSalt);

                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;

                User updatedUser= await _unitOfWork.UserDal.UpdateAsync(user);
                UserForChangePasswordDto updatedUserDto = _mapper.Map<UserForChangePasswordDto>(updatedUser);
                updatedUserDto.Password = request.NewPassword;

                await _unitOfWork.SaveChangesAsync();

                return updatedUserDto;

            }
        }
    }
}
