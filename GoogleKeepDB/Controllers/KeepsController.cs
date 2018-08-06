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
        public List<Keep> GetKeep()
        {
            return _context.Keep.Include(x => x.checklist).ToList();
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

            return Ok(keep);
            // return CreatedAtAction("GetKeep", new { id = keep.keepID }, keep);
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

        // GETBylabel: api/keep/label/{label}
        [HttpGet("label/{label}")]
        public  async Task<IActionResult> GetKeepByLabel([FromRoute] string label)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var keep = await _context.Keep.Include(x => x.checklist).ToListAsync();

            List<Keep> _keep = new List<Keep>();

            foreach(var p in keep)
            {
                if(p.label == label)
                {
                    _keep.Add(p);
                }
            }
            if(_keep.Count() == 0)
            {
                return NotFound("label does not exist");
            }
            else
                return Ok(_keep);
        }

        // GETBytitle: api/keep/title/{title}
        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetKeepByTitle([FromRoute] string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var keep = await _context.Keep.Include(x => x.checklist).ToListAsync();

            List<Keep> _keep = new List<Keep>();

            foreach (var p in keep)
            {
                if (p.title == title)
                {
                    _keep.Add(p);
                }
            }
            if (_keep.Count() == 0)
            {
                return NotFound("title does not exist");
            }
            else
                return Ok(_keep);
        }

        // GETBypinned: api/keep/ispinned/{ispinned}
        [HttpGet("ispinned/{ispinned}")]
        public async Task<IActionResult> GetKeepByIsPinned([FromRoute] bool ispinned)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var keep = await _context.Keep.Include(x => x.checklist).ToListAsync();

            List<Keep> _keep = new List<Keep>();

            foreach (var p in keep)
            {
                if (p.isPinned == ispinned)
                {
                    _keep.Add(p);
                }
            }
            if (_keep.Count() == 0)
            {
                return NotFound("label does not exist");
            }
            else
                return Ok(_keep);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKeep(/*[FromRoute] */int id)
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

            _context.Keep.Remove(keep);
            await _context.SaveChangesAsync();

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

            _context.Keep.Update(keep); // dafq man!! - updates the field as well as the state of the keep entity. No need to explicitly write EntityState.Modified

            // _context.Entry(keep).State = EntityState.Modified;

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

            return Ok(keep);
        }


        private bool KeepExists(int id)
        {
            return _context.Keep.Any(e => e.keepID == id);
        }
    }
}