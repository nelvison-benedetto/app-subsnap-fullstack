using System.Collections.Concurrent;

namespace SubSnap.Infrastructure.Caching;

public class QueryCache
{
    private readonly ConcurrentDictionary<string, object> _cache = new();
    public bool TryGet<T>(string key, out T value)
    {
        if (_cache.TryGetValue(key, out var obj) && obj is T typed)
        {
            value = typed;
            return true;
        }

        value = default!;
        return false;
    }
    public void Set<T>(string key, T value)
    {
        _cache[key] = value!;
    }

}
