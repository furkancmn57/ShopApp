using Application.Features.User.Commands;

namespace WebApi.Models.User.Request
{
    public class UpdateUserRequest
    {
        public string FirtName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UpdateUserCommand ToCommand(int id)
        {
            return new UpdateUserCommand(id, FirtName, LastName, Email);
        }
    }
}
