using Application.Features.Order.Commands;

namespace WebApi.Models.Order.Request
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public string CustomerName { get; set; }
        public List<int> ProductIds { get; set; }


        public CreateOrderCommand ToCommand()
        {
            return new CreateOrderCommand(UserId, AddressId, CustomerName, ProductIds);
        }
    }
}
