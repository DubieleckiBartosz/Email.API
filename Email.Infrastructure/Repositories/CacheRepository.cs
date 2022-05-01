using System;
using System.Threading.Tasks;
using Email.Application.Helpers;
using Email.Application.Interfaces.Repository;
using Microsoft.Extensions.Caching.Distributed;

namespace Email.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache;

        public CacheRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<bool> DeleteAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
            return true;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var bytes = await _distributedCache.GetAsync(key);
            return Serialize.Deserialize<T>(bytes);
        }

        public async Task<bool> RefreshAsync(string key)
        {
            await _distributedCache.RefreshAsync(key);
            return true;
        }

        public async Task<bool> SetAsync<T>(string key, T data, TimeSpan cacheTime, TimeSpan? slidingTime)
        {
            var bytes = data.DataSerialize();
            if (bytes == null)
            {
                return false;
            }

            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(cacheTime)
                .SetSlidingExpiration(slidingTime != null ? (TimeSpan)slidingTime : TimeSpan.FromMinutes(1));

            await _distributedCache.SetAsync(key, bytes, cacheEntryOptions);
            return true;
        }
    }
}
