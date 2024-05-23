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
    public class ProductConfigration : IEntityTypeConfiguration<ProductAggregate>
    {
        public void Configure(EntityTypeBuilder<ProductAggregate> builder)
        {
            builder.ToTable("product");
            builder.HasKey(p => p.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasColumnName("int");
            builder.Property(p => p.Name).HasColumnName("product_name").HasColumnType("varchar(250)");
            builder.Property(p => p.Description).HasColumnName("description").IsRequired(false);
            builder.Property(p => p.Price).HasColumnName("price").IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Quantity).HasColumnName("quantity").IsRequired().HasDefaultValue(0);
            builder.Property(p => p.CreatedDate).HasColumnName("created_date").HasColumnType("date");
            builder.Property(p => p.Ingredients).HasColumnName("ingredients").HasColumnType("jsonb");
        }
    }
}
