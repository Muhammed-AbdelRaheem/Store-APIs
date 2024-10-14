using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Servecies.Contract
{
    public interface ICacheService
    {

        public Task<string> GetCacheKeyAsync(string key);

        public Task  SetCacheKeyAsync(string key, object response,TimeSpan expireTime);

    }
}
