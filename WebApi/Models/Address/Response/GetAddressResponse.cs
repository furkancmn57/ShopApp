namespace WebApi.Models.Address.Response
{
    public class GetAddressResponse
    {
        public int Id { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
