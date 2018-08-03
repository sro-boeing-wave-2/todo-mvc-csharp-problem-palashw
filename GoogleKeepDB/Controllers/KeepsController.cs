using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoogleKeepDB.Model;
using GoogleKeepDB.Models;

namespace GoogleKeepDB.Controllers
{
    [Route("api/keep")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepContext _context; // connection instance

        public KeepsController(KeepContext context)
        {
            _context = context; // for security-- preventing _context to be accessed from out of the class
        }
        
        // GET: api/keep
        [HttpGet]
        public IEnumerable<Keep> GetKeep()
        {
            return _context.Keep.Include(x => x.checklist);
        }

        // POST: api/keep
        [HttpPost]
        public async Task<IActionResult> PostKeep([FromBody] Keep keep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Keep.Add(keep);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKeep", new { id = keep.keepID }, keep);
        }

        // GETByID: api/keep/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKeepByID([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var keep = await _context.Keep.Include(x => x.checklist).FirstOrDefaultAsync(x => x.keepID == id);

            if (keep == null)
            {
                return NotFound();
            }

            return Ok(keep);
        }

        // PUTByID: api/Keeps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKeepByID([FromRoute] int id, [FromBody] Keep keep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != keep.keepID)
            {
                return BadRequest();
            }

            _context.Entry(keep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeepExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated");
        }

        // DELETE: api/keeps
        //[HttpDelete("{id}")]
        //public IActionResult Delete([FromRoute] int id)
        //{
        //    var Note = _context.Keep.Where(p => p.keepID == id);

        //    if (Note == null)
        //    {
        //        return NotFound();
        //    }

        //    //_context.Keep.Remove(Note);
        //    foreach (var item in Note)
        //    {
        //        _context.Keep.Remove(item);
        //    }
        //    return Ok(Note);
        //} 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKeep(/*[FromRoute] */int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var keep = await _context.Keep.FindAsync(id);
            if (keep == null)
            {
                return NotFound();
            }

            _context.Keep.Remove(keep);
            await _context.SaveChangesAsync();

            return Ok(keep);
        }

        private bool KeepExists(int id)
        {
            return _context.Keep.Any(e => e.keepID == id);
        }
    }
}