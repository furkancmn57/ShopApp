using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IShopAppDbContext
    {
        DbSet<UserAggregate> Users { get; set; }
        DbSet<AddressAggregate> Addresses { get; set; }
        DbSet<ProductAggregate> Products { get; set; }
        DbSet<OrderAggregate> Orders { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
