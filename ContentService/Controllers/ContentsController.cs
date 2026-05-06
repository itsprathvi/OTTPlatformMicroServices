using ContentService.Data;
using ContentService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentService.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private readonly ContentServiceContext _context;

        private readonly ILogger<ContentsController> _logger;
        public ContentsController(ContentServiceContext context, ILogger<ContentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Contents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Content>>> GetContent()
        {
            return await _context.Content.ToListAsync();
        }

        // GET: api/Contents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> GetContent(Guid id)
        {
            var content = await _context.Content.FindAsync(id);

            if (content == null)
            {
                return NotFound();
            }

            return content;
        }

        // PUT: api/Contents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContent(Guid id, Content content)
        {
            _logger.LogInformation("Updating content with ID {ContentId}.", id);
            if (id != content.Id)
            {
                _logger.LogWarning("Content ID mismatch: {ContentId} != {ContentId}.", id, content.Id);
                return BadRequest();
            }

            _context.Entry(content).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Content with ID {ContentId} updated successfully.", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(id))
                {
                    _logger.LogWarning("Content with ID {ContentId} not found during update.", id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Concurrency error occurred while updating Content with ID {ContentId}.", id);
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Content>> PostContent(Content content)
        {
            _context.Content.Add(content);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContent", new { id = content.Id }, content);
        }

        // DELETE: api/Contents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(Guid id)
        {
            var content = await _context.Content.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            _context.Content.Remove(content);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContentExists(Guid id)
        {
            return _context.Content.Any(e => e.Id == id);
        }
    }
}
