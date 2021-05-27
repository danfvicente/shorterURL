using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortIn_API.Domain;
using ShortIn_API.Domain.Context;

namespace ShortIn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UrlsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Urls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Url>>> GetUrls()
        {
            return await _context.Urls.ToListAsync();
        }

        // GET: api/Urls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Url>> GetUrl(int id)
        {
            var url = await _context.Urls.FindAsync(id);

            if (url == null)
            {
                return NotFound();
            }

            return url;
        }

        // PUT: api/Urls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUrl(int id, Url url)
        {
            if (id != url.urlId)
            {
                return BadRequest();
            }

            _context.Entry(url).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrlExists(id))
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

        // POST: api/Urls
        [HttpPost]
        public async Task<ActionResult<Url>> PostUrl(Url url)
        {
            _context.Urls.Add(url);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetUrl", new { id = url.urlId }, url);
        }

        // DELETE: api/Urls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrl(int id)
        {
            var url = await _context.Urls.FindAsync(id);
            if (url == null)
            {
                return NotFound();
            }

            _context.Urls.Remove(url);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UrlExists(int id)
        {
            return _context.Urls.Any(e => e.urlId == id);
        }
    }
}
