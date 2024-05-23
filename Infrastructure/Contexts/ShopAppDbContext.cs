using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Configrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class ShopAppDbContext : DbContext, IShopAppDbContext
    {
        public DbSet<ProductAggregate> Products { get; set; }
        public DbSet<OrderAggregate> Orders { get; set; }
        public DbSet<UserAggregate> Users { get; set; }
        public DbSet<AddressAggregate> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new NpgsqlDataSourceBuilder("Server=localhost;Port=5432;Database=ShopAppDb;Userid=postgres;Password=123456;Include Error Detail=True;");
            builder.EnableDynamicJson();
            var dataSource = builder.Build();
            optionsBuilder.UseNpgsql(dataSource);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfigration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
