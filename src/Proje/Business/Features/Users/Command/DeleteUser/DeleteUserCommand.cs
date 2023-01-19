using AutoMapper;
using Business.Features.Users.Dtos;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Users.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Users.Command.DeleteUser
{
    public class DeleteUserCommand:IRequest<DeletedUserDto>,ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin, UserDelete };

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserDto>
        {
            private readonly IUserDal _userDal;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public DeleteUserCommandHandler(IUserDal userDal, IMapper mapper,
                                            UserBusinessRules userBusinessRules)
            {
                _userDal = userDal;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<DeletedUserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdShouldExistWhenSelected(request.Id);

                User mappedUser = _mapper.Map<User>(request);
                mappedUser.Status = false;
                User deletedUser = await _userDal.UpdateAsync(mappedUser);
                DeletedUserDto deletedUserDto = _mapper.Map<DeletedUserDto>(deletedUser);
                return deletedUserDto;
            }
        }
    }
}
