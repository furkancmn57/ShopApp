using Application.Features.Address.Commands;

namespace WebApi.Models.Address.Request
{
    public class UpdateAddressRequest
    {
        public string AddressTitle { get; set; }
        public string Address { get; set; }

        public UpdateAddressCommand ToCommand(int id)
        {
            return new UpdateAddressCommand(id, AddressTitle, Address);
        }
    }
}
