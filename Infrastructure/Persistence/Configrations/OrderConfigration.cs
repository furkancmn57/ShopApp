using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configrations
{
    public class OrderConfigration : IEntityTypeConfiguration<OrderAggregate>
    {
        public void Configure(EntityTypeBuilder<OrderAggregate> builder)
        {
            builder.ToTable("order");
            builder.HasKey(o => o.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(o => o.OrderNumber).HasColumnName("order_number").HasColumnType("uuid");
            builder.Property(o => o.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(18,2)");
            builder.Property(o => o.DiscountAmount).HasColumnName("discount_amount").HasColumnType("decimal(18,2)");;
            builder.Property(o => o.CustomerName).HasColumnName("customer_name").HasColumnType("varchar(250)");
            builder.Property(o => o.Status).HasColumnName("status").HasColumnType("int").HasConversion<int>();
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasColumnName("boolean");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date").HasColumnType("date");
            builder.Property(x => x.UpdatedDate).HasColumnName("updated_date").HasColumnType("date");
            builder.Property(x => x.DeletedDate).HasColumnName("deleted_date").HasColumnType("date");

            builder.HasMany(o => o.Products).WithMany(p => p.Orders);
            builder.HasOne(o => o.Address).WithMany(a => a.Orders);
            builder.HasOne(o => o.User).WithMany(u => u.Orders);

        }
    }
}
