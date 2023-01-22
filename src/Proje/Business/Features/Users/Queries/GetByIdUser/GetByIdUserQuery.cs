using AutoMapper;
using Business.Features.Users.Dtos;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using static Business.Features.Users.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Users.Queries.GetByIdUser
{
    public class GetByIdUserQuery:IRequest<UserDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles => new[] { Admin, UserGet };

        public class GetByIdUserQueryHanlder : IRequestHandler<GetByIdUserQuery, UserDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public GetByIdUserQueryHanlder(IUnitOfWork unitOfWork, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdMustBeAvailable(request.Id);

                User? user = await _unitOfWork.UserDal.GetAsync(u => u.Id == request.Id);
                UserDto userDto = _mapper.Map<UserDto>(user);

                return userDto;
            }
        }
    }
}
