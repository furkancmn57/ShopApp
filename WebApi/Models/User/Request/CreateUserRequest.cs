using Application.Features.User.Commands;

namespace WebApi.Models.User.Request
{
    public class CreateUserRequest
    {
        public string FirtName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CreateUserCommand ToCommand()
        {
            return new CreateUserCommand(FirtName, LastName, Email, Password);
        }
    }
}
