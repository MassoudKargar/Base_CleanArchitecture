using Microsoft.AspNetCore.Mvc;
using Base.Extensions.Caching.Abstractions;

namespace Base.Extensions.Caching.Distributed.Redis.Sample.Controllers;

public class CacheController(ICacheAdapter cacheAdapter) : BaseController
{
    [HttpPost]
    public IActionResult Add(string Key, string Value)
    {

        cacheAdapter.Add(Key, Value, DateTime.Now.AddDays(1), null);
        return Ok();
    }

    [HttpGet]
    public IActionResult Get(string Key)
    {
        return Ok(cacheAdapter.Get<string>(Key));
    }

    [HttpDelete]
    public IActionResult Delete(string Key)
    {
        cacheAdapter.RemoveCache(Key);
        return Ok();
    }
}