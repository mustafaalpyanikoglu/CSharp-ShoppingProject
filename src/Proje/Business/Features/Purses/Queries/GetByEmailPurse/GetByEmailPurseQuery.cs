using AutoMapper;
using Business.Features.Purses.Dtos;
using Business.Features.Purses.Rules;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Purses.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Purses.Queries.GetByIdPurse
{
    public class GetByEmailPurseQuery : IRequest<PurseDto>, ISecuredRequest
    {
        public string Email { get; set; }

        public string[] Roles => new[] { Admin, Customer };

        public class GetByNamePurseQueryHandler : IRequestHandler<GetByEmailPurseQuery, PurseDto>
        {
            private readonly IPurseDal _purseDal;
            private readonly IUserDal _userDal;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public GetByNamePurseQueryHandler(IPurseDal purseDal, IUserDal userDal, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _purseDal = purseDal;
                _userDal = userDal;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<PurseDto> Handle(GetByEmailPurseQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserEmailMustBeAvailable(request.Email);

                User? user = await _userDal.GetAsync(u => u.Email == request.Email);
                Purse? purse = await _purseDal.GetAsync(m => m.User.Email == request.Email,
                                                        include: x => x.Include(c => c.User));
                PurseDto PurseDto = _mapper.Map<PurseDto>(purse);

                return PurseDto;
            }
        }
    }
}
