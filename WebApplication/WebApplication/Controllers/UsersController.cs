using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication.Contracts;

namespace WebApplication.Controllers
{
    public class UsersController: ControllerBase
    {

        // GET api/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new List<User>();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PermissionsEnum.UsersView))]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

