using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft​.Extensions​.Caching​.Memory;

namespace SandboxAspnetCoreWebApi.Service.Controllers {

[Route("api/[controller]")]
public class EchosController : Controller
{
    protected static readonly string _globalCacheKey = "Echos";
    protected IMemoryCache _globalCache;

    /// <summary>Argument constructor.</summary>
    public EchosController(IMemoryCache memoryCache)
    {
        _globalCache = memoryCache;

        if(_globalCache.Get(_globalCacheKey) == null)
        {
            _globalCache.Set<IList<string>>(_globalCacheKey, new List<string> {
                "Message0",
                "Message1",
                "Message2",
                "Message3",
                "Message4",
                "Message5"
            }, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove));
        }
    }

    // GET: api/echos
    [HttpGet]
    public IActionResult Get()
    {
        return new JsonResult(_globalCache.Get<IList<string>>(_globalCacheKey));
    }

    // GET api/echos/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var items = _globalCache.Get<IList<string>>(_globalCacheKey);
        if(0 <= id && id < items.Count)
        {
            return Content(items[id]);
        }
        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
    }

    // POST api/echos
    [HttpPost]
    public IActionResult Post([FromBody]string message)
    {
        var items = _globalCache.Get<IList<string>>(_globalCacheKey);
        if(!String.IsNullOrEmpty(message))
        {
            items.Add(message);
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK);
        }
        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
    }

    // PUT api/echos/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]string message)
    {
        var items = _globalCache.Get<IList<string>>(_globalCacheKey);
        if(0 <= id && id < items.Count)
        {
            if(!String.IsNullOrEmpty(message))
            {
                items[id] = message;
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK);
            }
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
        }
        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
    }

    // DELETE api/echos/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var items = _globalCache.Get<IList<string>>(_globalCacheKey);
        if(0 <= id && id < items.Count)
        {
            items.RemoveAt(id);
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK);
        }
        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
    }
}

}
