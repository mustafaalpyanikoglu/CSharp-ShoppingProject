using AutoMapper;
using Business.Features.Users.Dtos;
using Business.Features.Users.Rules;
using Core.Business.Pipelines.Authorization;
using DataAccess.Abstract;
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
            private readonly IUserDal _userDal;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public GetByIdUserQueryHanlder(IUserDal userDal, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userDal = userDal;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdMustBeAvailable(request.Id);

                User? user = await _userDal.GetAsync(u => u.Id == request.Id);
                UserDto userDto = _mapper.Map<UserDto>(user);

                return userDto;
            }
        }
    }
}
