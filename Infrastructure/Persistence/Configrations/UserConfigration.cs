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
    public class UserConfigration : IEntityTypeConfiguration<UserAggregate>
    {
        public void Configure(EntityTypeBuilder<UserAggregate> builder)
        {

            builder.ToTable("user");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(x => x.FirstName).HasColumnName("first_name").HasColumnType("varchar(30)");
            builder.Property(x => x.LastName).HasColumnName("last_name").HasColumnType("varchar(30)");
            builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(50)");
            builder.Property(x => x.Password).HasColumnName("password").HasColumnType("varchar(250)");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date").HasColumnType("date");

            builder.HasMany(x => x.Addresses).WithOne(x => x.User);
            builder.HasMany(x => x.Orders).WithOne(x => x.User);
        }
    }
}
