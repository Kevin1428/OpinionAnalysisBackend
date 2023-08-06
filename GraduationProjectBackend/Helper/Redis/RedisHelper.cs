﻿using GraduationProjectBackend.Enums;
using StackExchange.Redis;

namespace GraduationProjectBackend.Helper.Redis
{
    public static class RedisHelper
    {
        private static readonly ConnectionMultiplexer RedisConnection = ConnectionMultiplexer.Connect("redis:6379");
        private static readonly IDatabase Db = RedisConnection.GetDatabase();

        public static string GetRedisKey(string topic, DateOnly startDate,
            DateOnly endDate, int dateRange, bool? isExactMatch, SearchModeEnum searchMode)
        {
            return topic
                   + startDate
                   + endDate
                   + dateRange
                   + isExactMatch
                   + searchMode;
        }

        public static IDatabase GetRedisDatabase()
        {
            return Db;
        }
    }
}
