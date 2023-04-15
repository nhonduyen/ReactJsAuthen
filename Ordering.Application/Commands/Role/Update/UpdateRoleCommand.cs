using MediatR;
using Ordering.Application.Common.Interfaces;

namespace Ordering.Application.Commands.Role.Update
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public UpdateRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.UpdateRole(request.Id, request.RoleName);
            return result;
        }
    }
}
