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
            builder.Property(p => p.Description).HasColumnName("description").HasColumnType("varchar(250)");
            builder.Property(p => p.Price).HasColumnName("price").IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Quantity).HasColumnName("quantity").IsRequired().HasDefaultValue(0);
            builder.Property(p => p.Ingredients).HasColumnName("ingredients").HasColumnType("jsonb");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasColumnName("boolean");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date").HasColumnType("date");
            builder.Property(x => x.UpdatedDate).HasColumnName("updated_date").HasColumnType("date");
            builder.Property(x => x.DeletedDate).HasColumnName("deleted_date").HasColumnType("date");
            
        }
    }
}
