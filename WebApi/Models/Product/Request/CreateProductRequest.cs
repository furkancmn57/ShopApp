using Application.Features.Product.Commands;
using Domain.Models;

namespace WebApi.Models.Product.Request
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }

        public CreateProductCommand ToCommand()
        {
            return new CreateProductCommand(Name, Description, Price, Ingredients, Quantity);
        }
    }
}
