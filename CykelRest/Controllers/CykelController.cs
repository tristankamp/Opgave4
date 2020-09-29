using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Opgave_1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CykelRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CykelController : ControllerBase
    {
        private static List<Cykel> _cykler = new List<Cykel>()
        {
            new Cykel(2, "red", 9001, 7)
        };
        

        // GET: api/<CykelController>
        [HttpGet]
        [ProducesResponseType(statusCode:StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public IActionResult Get()
        {
            if (_cykler != null || _cykler.Count != 0)
            {
                return Ok(_cykler);
            }
            else return NoContent();
        }

        // GET api/<CykelController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            if (_cykler.Exists(i => i.Id == id))
            {
                return Ok(_cykler.Find(i => i.Id == id));
            }
            else
                return NotFound($"Item {id} not found");
        }

        // POST api/<CykelController>
        [HttpPost]
        public void Post([FromBody] Cykel value)
        {
            int highestId = 0;
            foreach (Cykel c in _cykler)
            {
                if (c.Id > highestId)
                {
                    highestId = c.Id;
                }
            }
            value.Id = highestId + 1;
            _cykler.Add(value);
        }

        // PUT api/<CykelController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cykel value)
        {
            Cykel c = _cykler.Find(i => i.Id == id);
            if (c != null)
            {
                _cykler[_cykler.IndexOf(c)] = value;
            }
        }

        // DELETE api/<CykelController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        Cykel c = _cykler.Find(i => i.Id == id);
        _cykler.Remove(c);

    }
    }
}
