using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AddressAggregate
    {
        public AddressAggregate() 
        {
            // only db
        }
        private AddressAggregate(string addressTitle, string address, UserAggregate user)
        {
            UserId = user.Id;
            AddressTitle = addressTitle;
            Address = address;
            User = user;
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual UserAggregate User { get; set; }
        public virtual List<OrderAggregate> Orders { get; set; }

        public static AddressAggregate Create(string addressTitle, string address, UserAggregate user)
        {
            return new AddressAggregate(addressTitle, address, user);
        }

        public AddressAggregate Update(string addressTitle, string address)
        {
            AddressTitle = addressTitle;
            Address = address;
            return this;
        }
    }
}
