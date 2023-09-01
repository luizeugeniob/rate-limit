﻿namespace rate_limit.RateLimit;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class RateLimitAttribute : Attribute
{
    public int TimeWindowInSeconds { get; set; }
    public int MaxRequests { get; set; }
}
