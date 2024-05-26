using Application.Features.Product.Queries;
using Domain.Models;

namespace WebApi.Models.Product.Response
{
    public class GetProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
