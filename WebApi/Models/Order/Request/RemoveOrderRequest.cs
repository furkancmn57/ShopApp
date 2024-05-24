using Application.Features.Order.Commands;

namespace WebApi.Models.Order.Request
{
    public class RemoveOrderRequest
    {
        public RemoveOrderCommand ToCommand(int id)
        {
            return new RemoveOrderCommand(id);
        }
    }
}
