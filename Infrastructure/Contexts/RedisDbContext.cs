
using Application.Common.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class RedisDbContext : IRedisDbContext
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _database;

        public RedisDbContext()
        {
            _connection = ConnectionMultiplexer.Connect("localhost");
            _database = _connection.GetDatabase();
        }

        public async Task<T> Get<T>(string key)
        {
            var value = await _database.StringGetAsync(key);

            if (value.IsNullOrEmpty)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(value);
        }


        public async Task Add<T>(string key, T value, int time = 1)
        {
            var jsonValue = JsonSerializer.Serialize(value);

            var timeSpan = TimeSpan.FromMinutes(time);

            await _database.StringSetAsync(key, jsonValue,timeSpan);
        }

        public async Task Delete(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

    }
}
