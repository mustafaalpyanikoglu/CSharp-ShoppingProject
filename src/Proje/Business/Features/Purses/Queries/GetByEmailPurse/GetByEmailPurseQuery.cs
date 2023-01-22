using AutoMapper;
using Business.Features.Purses.Dtos;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.Contexts;
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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public GetByNamePurseQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<PurseDto> Handle(GetByEmailPurseQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserEmailMustBeAvailable(request.Email);

                User? user = await _unitOfWork.UserDal.GetAsync(u => u.Email == request.Email);
                Purse? purse = await _unitOfWork.PurseDal.GetAsync(m => m.User.Email == request.Email,
                                                        include: x => x.Include(c => c.User));
                PurseDto PurseDto = _mapper.Map<PurseDto>(purse);

                return PurseDto;
            }
        }
    }
}
