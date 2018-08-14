using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoogleKeepDB.Model;
using MongoDB.Driver;
using MongoDB.Bson;

namespace GoogleKeepDB.Controllers
{
    

    [Route("api/keep")]
    public class KeepsController : ControllerBase
    {

        dataAcc _context; // connection instance

        public KeepsController( dataAcc d )
        {
            _context = d; // for security-- preventing _context to be accessed from out of the class
        }
        
       // GET: api/keep
       [HttpGet]
       public IEnumerable<Keep> getall()
        {
            return _context.GetKeep();
        }

        // POST: api/keep
        [HttpPost]
        public IActionResult Post([FromBody]Keep p)
        {
            _context.Create(p);
            return new OkObjectResult(p);
        }
        // get by id
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            var keep = _context.GetKeepByID(new ObjectId(id));
            if (keep == null)
            {
                return NotFound();
            }
            return new ObjectResult(keep);
        }

        // get by label
        [HttpGet("label/{label}")]
        public IActionResult GetByLabel(string label)
        {
            var keep = _context.GetKeepByLabel(label);
            if (keep == null)
            {
                return NotFound();
            }
            return new ObjectResult(keep);
        }

        // get by title
        [HttpGet("title/{title}")]
        public IActionResult GetByTitle(string title)
        {
            var keep = _context.GetKeepByTitle(title);
            if (keep == null)
            {
                return NotFound();
            }
            return new ObjectResult(keep);
        }

        // get by pinned
        [HttpGet("ispinned/{ispinned}")]
        public IActionResult GetByPinned(bool ispinned)
        {
            var keep = _context.GetKeepByPinned(ispinned);
            if (keep == null)
            {
                return NotFound();
            }
            return new ObjectResult(keep);
        }

        // put by id
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody]Keep p)
        {
            var recId = new ObjectId(id);
            var product = _context.GetKeepByID(recId);
            if (product == null)
            {
                return NotFound();
            }

            _context.Update(recId, p);
            return new OkResult();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var keep = _context.GetKeepByID(new ObjectId(id));
            if (keep == null)
            {
                return NotFound();
            }

            _context.Remove(keep.Id);
            return new OkResult();
        }
        
    }
}