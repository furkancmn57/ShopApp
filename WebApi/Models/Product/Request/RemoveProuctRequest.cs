using Application.Features.Product.Commands;

namespace WebApi.Models.Product.Request
{
    public class RemoveProuctRequest
    {
        public RemoveProductCommand ToCommand(int id)
        {
            return new RemoveProductCommand(id);
        }
    }
}
