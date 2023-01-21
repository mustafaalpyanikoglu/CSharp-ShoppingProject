using AutoMapper;
using Business.Features.Purses.Dtos;
using Business.Features.Purses.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Purses.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Purses.Queries.GetByIdPurse
{
    public class GetByIdPurseQuery : IRequest<PurseDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin };

        public class GetByIdPurseQueryHandler : IRequestHandler<GetByIdPurseQuery, PurseDto>
        {
            private readonly IPurseDal _purseDal;
            private readonly IMapper _mapper;
            private readonly PurseBusinessRules _purseBusinessRules;

            public GetByIdPurseQueryHandler(IPurseDal purseDal, IMapper mapper, PurseBusinessRules purseBusinessRules)
            {
                _purseDal = purseDal;
                _mapper = mapper;
                _purseBusinessRules = purseBusinessRules;
            }

            public async Task<PurseDto> Handle(GetByIdPurseQuery request, CancellationToken cancellationToken)
            {
                await _purseBusinessRules.PurseIdShouldExistWhenSelected(request.Id);

                Purse? purse = await _purseDal.GetAsync(m => m.Id == request.Id,
                                                              include: x => x.Include(c => c.User));
                PurseDto PurseDto = _mapper.Map<PurseDto>(purse);

                return PurseDto;
            }
        }
    }
}
