using Application.Features.Address.Commands;

namespace WebApi.Models.Address.Request
{
    public class RemoveAddressRequest
    {
        public RemoveAddressCommand ToCommand(int id)
        {
            return new RemoveAddressCommand(id);
        }
    }
}
