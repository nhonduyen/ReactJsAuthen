using MediatR;
using Ordering.Application.Common.Interfaces;

namespace Ordering.Application.Commands.User.Update
{
    public class AssignUsersRoleCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class AssignUsersRoleCommandHandler : IRequestHandler<AssignUsersRoleCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public AssignUsersRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(AssignUsersRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.AssignUserToRole(request.UserName, request.Roles);
            return result;
        }
    }
}
