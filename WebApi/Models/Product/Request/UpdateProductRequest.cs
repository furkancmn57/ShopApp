using Application.Features.Product.Commands;
using Domain.Models;

namespace WebApi.Models.Product.Request
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }

        public UpdateProductCommand ToCommand(int id)
        {
            return new UpdateProductCommand(id, Name, Description, Price, Ingredients, Quantity);
        }
    }
}
