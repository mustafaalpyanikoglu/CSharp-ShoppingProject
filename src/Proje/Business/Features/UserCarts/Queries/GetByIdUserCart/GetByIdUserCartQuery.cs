using AutoMapper;
using Business.Features.UserCarts.Dtos;
using Business.Features.UserCarts.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.UserCarts.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserCarts.Queries.GetByIdUserCart
{
    public class GetByIdUserCartQuery : IRequest<UserCartDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin,UserCartGet };

        public class GetByIdUserCartQueryHandler : IRequestHandler<GetByIdUserCartQuery, UserCartDto>
        {
            private readonly IUserCartDal _UserCartDal;
            private readonly IMapper _mapper;
            private readonly UserCartBusinessRules _UserCartBusinessRules;

            public GetByIdUserCartQueryHandler(IUserCartDal UserCartDal, IMapper mapper, UserCartBusinessRules UserCartBusinessRules)
            {
                _UserCartDal = UserCartDal;
                _mapper = mapper;
                _UserCartBusinessRules = UserCartBusinessRules;
            }

            public async Task<UserCartDto> Handle(GetByIdUserCartQuery request, CancellationToken cancellationToken)
            {
                await _UserCartBusinessRules.UserCartIdShouldExistWhenSelected(request.Id);

                UserCart? UserCart = await _UserCartDal.GetAsync(m => m.Id == request.Id,
                                                              include: x => x.Include(c => c.User));
                UserCartDto UserCartDto = _mapper.Map<UserCartDto>(UserCart);

                return UserCartDto;
            }
        }
    }
}
