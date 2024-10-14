using StackExchange.Redis;
using Store.Core.Servecies.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Services.Servecies
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _dataBase;

        public CacheService(IConnectionMultiplexer redis)
        {
            _dataBase = redis.GetDatabase();
        }

        public async Task<string> GetCacheKeyAsync(string key)
        {

            var response =await _dataBase.StringGetAsync(key);

            if (response.IsNullOrEmpty)
            {
                return null;
            }

            return response.ToString();
        }

        public async Task SetCacheKeyAsync(string key, object response, TimeSpan expireTime)
        {
            if (response is null) return;


            var options = new JsonSerializerOptions() {PropertyNamingPolicy=JsonNamingPolicy.CamelCase };
            var jsonResponse = JsonSerializer.Serialize(response, options);


            await _dataBase.StringSetAsync(key, jsonResponse, expireTime);



        }
    }
}
