using Microsoft.Extensions.DependencyInjection;

namespace HZY.Infrastructure.Redis
{
    public static class RedisUtil
    {

        /// <summary>
        /// 注册 Redis 模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void AddRedisService(this IServiceCollection services, string connectionString)
        {
            var csredis = new CSRedis.CSRedisClient(connectionString);
            RedisHelper.Initialization(csredis);
            services.AddSingleton(csredis);
        }



    }
}
