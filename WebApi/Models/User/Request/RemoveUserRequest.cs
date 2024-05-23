using Application.Features.User.Commands;

namespace WebApi.Models.User.Request
{
    public class RemoveUserRequest
    {
        public RemoveUserCommand ToCommand(int id)
        {
            return new RemoveUserCommand(id);
        }
    }
}
