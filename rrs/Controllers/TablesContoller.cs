using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rrs.DB;
using rrs.DB.Entities;

namespace rrs.Controllers
{
    [Route("api/Tables")]
    [ApiController]
    public class TablesContoller : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TablesContoller(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all tables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetTables()
        {
            return await _context.Tables.ToListAsync();
        }

        // Get a table by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
            var table = await _context.Tables.FindAsync(id);

            if (table == null)
            {
                return NotFound();
            }

            return table;
        }

        // Create a table
        [HttpPost]
        public async Task<ActionResult<Table>> PostTable(Table table)
        {
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTable", new { id = table.TableId }, table);
        }

        // Update a table
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Table table)
        {
            if (id != table.TableId)
            {
                return BadRequest();
            }

            _context.Entry(table).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a table
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Patch a table
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTable(int id, [FromBody] JsonPatchDocument<Table> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var table = await _context.Tables.FirstOrDefaultAsync(t => t.TableId == id);
            if (table == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(table, error => {
            ModelState.AddModelError(error.Operation.path, error.ErrorMessage);
            });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(table).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tables.Any(t => t.TableId == id))
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

    }
}
