using MediatR;
using Ordering.Application.Common.Interfaces;

namespace Ordering.Application.Commands.Role.Delete
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public DeleteRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.DeleteRoleAsync(request.RoleId);
            return result;
        }
    }
}
