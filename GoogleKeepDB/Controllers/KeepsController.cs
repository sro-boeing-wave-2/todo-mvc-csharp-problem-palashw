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
        
        //// GETByID: api/keep/id
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetKeepByID([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var keep = await _context.Keep.Include(x => x.checklist).FirstOrDefaultAsync(x => x.keepID == id);

        //    if (keep == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(keep);
        //}

        //// GETBylabel: api/keep/label/{label}
        //[HttpGet("label/{label}")]
        //public  async Task<IActionResult> GetKeepByLabel([FromRoute] string label)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var keep = await _context.Keep.Include(x => x.checklist).ToListAsync();

        //    List<Keep> _keep = new List<Keep>();

        //    foreach(var p in keep)
        //    {
        //        if(p.label == label)
        //        {
        //            _keep.Add(p);
        //        }
        //    }
        //    if(_keep.Count() == 0)
        //    {
        //        return NotFound("label does not exist");
        //    }
        //    else
        //        return Ok(_keep);
        //}

        //// GETBytitle: api/keep/title/{title}
        //[HttpGet("title/{title}")]
        //public async Task<IActionResult> GetKeepByTitle([FromRoute] string title)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var keep = await _context.Keep.Include(x => x.checklist).ToListAsync();

        //    List<Keep> _keep = new List<Keep>();

        //    foreach (var p in keep)
        //    {
        //        if (p.title == title)
        //        {
        //            _keep.Add(p);
        //        }
        //    }
        //    if (_keep.Count() == 0)
        //    {
        //        return NotFound("title does not exist");
        //    }
        //    else
        //        return Ok(_keep);
        //}

        //// GETBypinned: api/keep/ispinned/{ispinned}
        //[HttpGet("ispinned/{ispinned}")]
        //public async Task<IActionResult> GetKeepByIsPinned([FromRoute] bool ispinned)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var keep = await _context.Keep.Include(x => x.checklist).ToListAsync();

        //    List<Keep> _keep = new List<Keep>();

        //    foreach (var p in keep)
        //    {
        //        if (p.isPinned == ispinned)
        //        {
        //            _keep.Add(p);
        //        }
        //    }
        //    if (_keep.Count() == 0)
        //    {
        //        return NotFound("label does not exist");
        //    }
        //    else
        //        return Ok(_keep);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteKeep(/*[FromRoute] */int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var keep = await _context.Keep.Include(x => x.checklist).FirstOrDefaultAsync(x => x.keepID == id);
        //    if (keep == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Keep.Remove(keep);
        //    await _context.SaveChangesAsync();

        //    return Ok(keep);
        //}

        //// PUTByID: api/keep/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutKeepByID([FromRoute] int id, [FromBody] Keep keep)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != keep.keepID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Keep.Update(keep); // dafq man!! - updates the field as well as the state of the keep entity. No need to explicitly write EntityState.Modified

        //    // _context.Entry(keep).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!KeepExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Ok(keep);
        //}


        //private bool KeepExists(int id)
        //{
        //    return _context.Keep.Any(e => e.keepID == id);
        //}
    }
}