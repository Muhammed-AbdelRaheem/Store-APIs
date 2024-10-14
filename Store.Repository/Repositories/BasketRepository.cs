using StackExchange.Redis;
using Store.Core.Entities.Basket;
using Store.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

         
        public async Task<bool> DeleteBasketAsync(string Id)
        {
            return await _database.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Id) 
        {
            var basket= await _database.StringGetAsync(Id);
            return basket.IsNullOrEmpty?null:JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);

           var createOrUpdateBasket=await _database.StringSetAsync(basket.Id, JsonBasket, TimeSpan.FromDays(30));
            if (createOrUpdateBasket is false) return null;

            return await GetBasketAsync(basket.Id);

        }
    }
}
