using Application.Features.User.Queries;
using WebApi.Models.Address.Response;

namespace WebApi.Models.User.Reponse
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<GetAddressResponse> Addresses { get; set; }
    }
}
