using API.Repositories;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Repository, Entity, Primary> : ControllerBase
        where Entity : class
        where Repository : IGeneralRepository<Entity, Primary>
    {
        Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        //GET
        [HttpGet]
        public IActionResult Get()
        {
            var data = repository.Get();
            return Ok(new { result = 200, data = data });
        }

        [HttpGet("{id}")]
        public IActionResult Get(Primary id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var data = repository.Get(id);
            if (data != null)
                return Ok(data);
            return NotFound();
        }

        // POST
        [HttpPost]
        public IActionResult Post(Entity entity)
        {
            if (ModelState.IsValid)
            {
                var result = repository.Post(entity);
                if (result > 0)
                    return Ok(new { result = 200, message = "successfully inserted" });
            }
            return BadRequest();
        }

        // PUT
        [HttpPut("{id}")]
        public IActionResult Put(Primary id, Entity entity)
        {
            if (ModelState.IsValid)
            {
                // var data = Get(id);
                var result = repository.Put(id, entity);
                if (result > 0)
                    return Ok();
            }
            return BadRequest();
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(Primary id)
        {
            // var data = Get(id);
            var result = repository.Delete(id);
            if (result > 0)
                return Ok();
            return BadRequest();
        }
    }
}
