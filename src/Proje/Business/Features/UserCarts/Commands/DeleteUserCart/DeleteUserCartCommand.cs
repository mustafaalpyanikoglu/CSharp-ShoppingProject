using AutoMapper;
using Business.Features.UserCarts.Dtos;
using Business.Features.UserCarts.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using static Business.Features.UserCarts.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.UserCarts.Commands.DeleteUserCart
{
    public class DeleteUserCartCommand : IRequest<DeletedUserCartDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin, UserCartDelete };

        public class DeleteUserCartCommandHandler : IRequestHandler<DeleteUserCartCommand, DeletedUserCartDto>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly UserCartBusinessRules _userCartBusinessRules;

            public DeleteUserCartCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, UserCartBusinessRules userCartBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _userCartBusinessRules = userCartBusinessRules;
            }

            public async Task<DeletedUserCartDto> Handle(DeleteUserCartCommand request, CancellationToken cancellationToken)
            {
                await _userCartBusinessRules.UserCartIdShouldExistWhenSelected(request.Id);

                UserCart mappedUserCart = _mapper.Map<UserCart>(request);
                UserCart DeletedUserCart = await _unitOfWork.UserCartDal.DeleteAsync(mappedUserCart);
                DeletedUserCartDto DeleteUserCartDto = _mapper.Map<DeletedUserCartDto>(DeletedUserCart);

                await _unitOfWork.SaveChangesAsync();

                return DeleteUserCartDto;
            }
        }
    }
}
