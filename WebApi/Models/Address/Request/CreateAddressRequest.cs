using Application.Features.Address.Commands;

namespace WebApi.Models.Address.Request
{
    public class CreateAddressRequest
    {
        public string AddressTitle { get; set; }
        public string Address { get; set; }

        public CreateAddressCommand ToCommand(int userId)
        {
            return new CreateAddressCommand(userId,AddressTitle, Address);
        }
    }
}
