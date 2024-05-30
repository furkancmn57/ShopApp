using WebApi.Models.Product.Response;

namespace WebApi.Models.Order.Response
{
    public class GetOrderByIdResponse
    {
        public int Id { get; set; }
        public Guid OrderNumber { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public  List<GetProductResponseWithOrder> Products { get; set; }
        public GetAddressResponseWithOrder Address { get; set; }
        public GetUserResponseWithOrder User { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
