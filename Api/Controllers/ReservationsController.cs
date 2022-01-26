using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationsController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/<ReservationsController>
        [HttpGet]
        public async Task<List<Reservation>> Get() => await _reservationService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Reservation>> Get(string id)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            return reservation;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Reservation newReservation)
        {
            await _reservationService.CreateAsync(newReservation);

            return CreatedAtAction(nameof(Get), new { id = newReservation.Id }, newReservation);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Reservation updatedReservation)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            updatedReservation.Id = reservation.Id;

            await _reservationService.UpdateAsync(id, updatedReservation);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            await _reservationService.RemoveAsync(reservation.Id);

            return NoContent();
        }
    }
}
