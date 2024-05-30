using Application.Features.Order.Commands;
using Domain.Enums;

namespace WebApi.Models.Order.Request
{
    public class UpdateOrderRequest
    {
        public OrderStatus Status { get; set; }

        public UpdateOrderCommand ToCommand(int id)
        {
            return new UpdateOrderCommand(id, Status);
        }
    }
}
