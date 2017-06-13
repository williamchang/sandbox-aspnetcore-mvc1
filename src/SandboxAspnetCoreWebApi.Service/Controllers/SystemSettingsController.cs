using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SandboxAspnetCoreWebApi.Service.Controllers {

[Route("api/[controller]")]
public class SystemSettingsController : Controller
{
    // GET api/systemsettings
    [HttpGet]
    public IEnumerable<string> Get()
    {
        throw new NotImplementedException();
    }

    // GET api/systemsettings/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        throw new NotImplementedException();
    }

    // POST api/systemsettings
    [HttpPost]
    public void Post([FromBody]string value)
    {
        throw new NotImplementedException();
    }

    // PUT api/systemsettings/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
        throw new NotImplementedException();
    }

    // DELETE api/systemsettings/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}

}
