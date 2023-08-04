﻿using Newtonsoft.Json;
using static rate_limit.RateLimit.RateLimitMiddleware;

namespace Microsoft.Extensions.Caching.Distributed;

public static class DistributedCacheExtension
{
    public async static Task<ConsumptionData?> GetCustomerConsumptionDataFromContextAsync(
        this IDistributedCache cache, HttpContext context, CancellationToken cancellation = default)
    {
        var result = await cache.GetStringAsync(context.GetCustomerKey(), cancellation);
        if (result is null)
            return null;

        return JsonConvert.DeserializeObject<ConsumptionData>(result);
    }

    public async static Task SetCacheValueAsync(
        this IDistributedCache cache, string key, ConsumptionData? customerRequests, CancellationToken cancellation = default)
    {
        // If the customer does not yet have consumption data, we will create his record, starting the count at one request
        customerRequests ??= new ConsumptionData(DateTime.UtcNow, 1);

        await cache.SetStringAsync(key, JsonConvert.SerializeObject(customerRequests), cancellation);
    }
}
