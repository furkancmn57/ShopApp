namespace WebApi.Models.Order.Response
{
    public class GetOrdersResponse
    {
        public int Id { get; set; }
        public Guid OrderNumber { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        //public GetUserResponseWithOrder User { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
