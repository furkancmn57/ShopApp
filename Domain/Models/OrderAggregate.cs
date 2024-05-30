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
        private OrderAggregate(double totalAmount, double discountAmount, string customerName, List<ProductAggregate> products, AddressAggregate address, UserAggregate user)
        {
            UserId = user.Id;
            AddressId = address.Id;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            CustomerName = customerName;
            Status = Enums.OrderStatus.Pending;
            Products = products;
            Address = address;
            User = user;
            OrderDate = DateTime.Now;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public Guid OrderNumber { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public string CustomerName { get; set; }
        public Enums.OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<ProductAggregate> Products { get; set; }
        public virtual AddressAggregate Address { get; set; }
        public virtual UserAggregate User { get; set; }


        public static OrderAggregate Create(double totalAmount, double discountAmount, string customerName, List<ProductAggregate> products, AddressAggregate address, UserAggregate user)
        {
            return new OrderAggregate(totalAmount, discountAmount, customerName, products, address, user);
        }

        public OrderAggregate Update(Enums.OrderStatus status)
        {
            Status = status;
            return this;
        }
    }
}
