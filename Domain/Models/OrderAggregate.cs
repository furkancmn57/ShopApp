using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderAggregate
    {
        public OrderAggregate()
        {
            // only db
        }
        public OrderAggregate(string orderNumber, double totalAmount, double discountAmount, string customerName, List<ProductAggregate> products, AddressAggregate address, UserAggregate user)
        {
            UserId = user.Id;
            AddressId = address.Id;
            OrderNumber = orderNumber;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            CustomerName = customerName;
            Products = products;
            Address = address;
            User = user;
            OrderDate = DateTime.Now;
            
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public string OrderNumber { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public virtual List<ProductAggregate> Products { get; set; }
        public virtual AddressAggregate Address { get; set; }
        public virtual UserAggregate User { get; set; }


        public static OrderAggregate Create(string orderNumber, double totalAmount, double discountAmount, string customerName, List<ProductAggregate> products, AddressAggregate address, UserAggregate user)
        {
            return new OrderAggregate(orderNumber, totalAmount, discountAmount, customerName, products, address, user);
        }
    }
}
