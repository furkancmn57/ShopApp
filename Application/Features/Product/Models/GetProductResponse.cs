using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Models
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
