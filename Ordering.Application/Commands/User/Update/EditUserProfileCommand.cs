using MediatR;
using Ordering.Application.Common.Interfaces;

namespace Ordering.Application.Commands.User.Update
{
    public class EditUserProfileCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }

    public class EditUserProfileCommandHandler : IRequestHandler<EditUserProfileCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public EditUserProfileCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.UpdateUserProfile(request.Id, request.FullName, request.Email, request.Roles);
            return result;
        }
    }
}
