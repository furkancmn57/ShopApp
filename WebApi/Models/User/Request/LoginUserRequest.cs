using Application.Features.User.Commands;

namespace WebApi.Models.User.Request
{
    public class LoginUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginUserCommand ToCommand()
        {
            return new LoginUserCommand(Email, Password);
        }
    }
}
