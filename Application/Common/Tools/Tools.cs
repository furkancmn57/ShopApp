using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Tools
{
    public class Tools
    {
        private readonly static IShopAppDbContext _context;
        public static async Task<string> GenerateUniqueOrderNumber(CancellationToken token)
        {
            string orderNumber = GenerateNumber();

            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderNumber == orderNumber, token);

            if (order is not null)
            {
                return await GenerateUniqueOrderNumber(token);
            }

            return orderNumber;
        }
        private static string GenerateNumber()
        {
            string orderNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();

            return orderNumber;
        }

        public string GenerateAuthUserName(int userId)
        {
            string authUserName = $"session_{userId}";

            return authUserName;
        }

    }
}
