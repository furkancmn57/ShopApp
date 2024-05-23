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
    public class AddressConfigration : IEntityTypeConfiguration<AddressAggregate>
    {
        public void Configure(EntityTypeBuilder<AddressAggregate> builder)
        {
            builder.ToTable("address");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(x => x.AddressTitle).HasColumnName("address_title").HasColumnType("varchar(50)");
            builder.Property(x => x.Address).HasColumnName("address").HasColumnType("varchar(250)");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date").HasColumnType("date");

            builder.HasOne(x => x.User).WithMany(x => x.Addresses);
            builder.HasMany(x => x.Orders).WithOne(x => x.Address);
        }
    }
}
