using AutoMapper;
using Business.Features.Users.Dtos;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.Contexts;
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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<DeletedUserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdShouldExistWhenSelected(request.Id);

                User mappedUser = _mapper.Map<User>(request);
                mappedUser.Status = false;
                User deletedUser = await _unitOfWork.UserDal.UpdateAsync(mappedUser);
                DeletedUserDto deletedUserDto = _mapper.Map<DeletedUserDto>(deletedUser);

                await _unitOfWork.SaveChangesAsync();

                return deletedUserDto;
            }
        }
    }
}
