using System;
using Newtonsoft.Json;
using System.Linq;
using Payscrow.Escrow.Application.Interfaces;
using StackExchange.Redis;

namespace Payscrow.Escrow.Application.Services
{
    public class RedisDistributedCacheManager : ICacheManager
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisDistributedCacheManager(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public void Clear()
        {
            var endPoints = _connectionMultiplexer.GetEndPoints();
            var server = _connectionMultiplexer.GetServer(endPoints[0]);
            server.FlushDatabase();
        }

        public T Get<T>(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();

            var objString = db.StringGet(new RedisKey(key));

            return JsonConvert.DeserializeObject<T>(objString);
        }

        public bool IsSet(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();

            return db.StringGet(new RedisKey(key)).HasValue;
        }

        public void Remove(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            db.KeyDelete(new RedisKey(key));
        }

        public void RemoveByPattern(string pattern)
        {
            var endPoints = _connectionMultiplexer.GetEndPoints();
            var server = _connectionMultiplexer.GetServer(endPoints[0]);
            var db = _connectionMultiplexer.GetDatabase();

            if (server != null)
            {
                var keys = server.Keys(pattern: pattern + "*").ToArray();
                db.KeyDelete(keys);
            }
        }

        public void Set(string key, object data, int cacheTime)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var redisKey = new RedisKey(key);
            var objString = JsonConvert.SerializeObject(data);
            var expire = TimeSpan.FromMinutes(cacheTime);
            db.StringSet(redisKey, objString, expire);
        }
    }
}