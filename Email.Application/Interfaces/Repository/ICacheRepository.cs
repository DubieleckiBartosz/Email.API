using System;
using System.Threading.Tasks;

namespace Email.Application.Interfaces.Repository
{
    public interface ICacheRepository
    {
        Task<T> GetAsync<T>(string key);
        Task<bool> SetAsync<T>(string key, T data, TimeSpan cacheTime, TimeSpan? slidingTime = null);
        Task<bool> RefreshAsync(string key);
        Task<bool> DeleteAsync(string key);
    }
}
