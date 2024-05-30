using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserAggregate
    {
        public UserAggregate()
        {
            // only db
        }
        private UserAggregate(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<AddressAggregate> Addresses { get; set; }
        public virtual List<OrderAggregate> Orders { get; }

        public static UserAggregate Create(string firstName, string lastName, string email, string password)
        {
            return new UserAggregate(firstName, lastName, email,password);
        }

        public UserAggregate Update(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            return this;
        }
    }
}
