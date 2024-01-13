using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rrs.DB;
using rrs.DB.Entities;

namespace rrs.Controllers
{
    [Route("api/Reservations")]
    [ApiController]
    public class ReservationsController:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        // Get a reservation by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if(reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // Create a reservation
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new {id = reservation.ReservationId}, reservation);
        }

        // Update a reservation
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if(id != reservation.ReservationId)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a reservation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if(reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Patch a reservation
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchReservation(int id, [FromBody] JsonPatchDocument<Reservation> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if(reservation == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(reservation, error => {
                ModelState.AddModelError(error.Operation.path, error.ErrorMessage);
            });

            var isValid = TryValidateModel(reservation);
            if(!isValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
