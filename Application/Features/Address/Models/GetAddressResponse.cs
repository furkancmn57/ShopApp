using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Address.Models
{
    public class GetAddressResponse
    {
        public int Id { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
