using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRedisDbContext
    {
        Task<T> Get<T>(string key);
        Task Add<T>(string key, T value, int time = 1);
        Task Delete(string key);
    }
}
