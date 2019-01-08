using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodLog.Api.Database;
using FoodLog.Api.Models;
using FoodLog.DTOs;
using log4net;

namespace FoodLog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly FoodContext _context;
        private readonly ILog _log;

        public EntriesController(FoodContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        // GET: api/Entries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntryDTO>>> GetEntries()
        {
            var entities = await _context.Entries.ToListAsync();
            return Ok(entities.Select(x => EntryMapper.Map(x, new EntryDTO())));
        }

        // GET: api/Entries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntryDTO>> GetEntry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = await _context.Entries.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            return Ok(EntryMapper.Map(entry, new EntryDTO()));
        }

        // PUT: api/Entries/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutEntry([FromRoute] int id, [FromBody] EntryDTO dto)
        {
            Console.WriteLine("I shouldn't be here!");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.EntryId)
                return BadRequest();

            var entry = EntryMapper.Map(dto, new Entry());

            _context.Entry(entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Entries
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<EntryDTO>> PostEntry([FromBody] EntryDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entry = EntryMapper.Map(dto, new Entry());

            Console.WriteLine(entry.EntryId);

            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            Console.WriteLine(entry.EntryId);

            return CreatedAtAction("GetEntry", new { id = entry.EntryId }, entry);
        }

        // DELETE: api/Entries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = await _context.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();

            return Ok(entry);
        }

        private bool EntryExists(int id)
        {
            return _context.Entries.Any(e => e.EntryId == id);
        }
    }
}