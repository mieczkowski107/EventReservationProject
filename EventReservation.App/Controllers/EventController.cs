using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventReservation.App.Controllers;

[Route("api/events")]
[ApiController]
//[Authorize(Roles = nameof(Roles.Admin))]
public class EventController(AppDbContext context) : ControllerBase
{
   [HttpGet]
   public async Task<IActionResult> GetEventsAsync()
   {
      var allEvents = await context.Event.ToListAsync();
      //TODO: DTO
      return Ok(allEvents.Count == 0 ? [] : allEvents);
   }

   [HttpGet("{id:guid}")]
   public async Task<IActionResult> GetSingleEventAsync(Guid id) // PL/SQL: Procedura
   {
      var eventFromDb = await context.Event.FindAsync(id);
      if (eventFromDb == null)
      {
         return NotFound("Event not found");
      }
      var eventToReturn = new EventDto
      {
         Id = eventFromDb.Id,
         Name = eventFromDb.Name,
         Description = eventFromDb.Description,
         StartTime = eventFromDb.StartTime,
         EndTime = eventFromDb.EndTime,
         Location = eventFromDb.Location,
         EventEmail = eventFromDb.EventEmail,
         IsOverLappingAllowed = eventFromDb.IsOverLappingAllowed,
         CoordinatorName = eventFromDb.CoordinatorName,
         CoordinatorSurname = eventFromDb.CoordinatorSurname,
         CoordinatorPhone = eventFromDb.CoordinatorPhone,
      };
      return Ok(eventToReturn);
   }

   [HttpPost]
   public async Task<IActionResult> CreateEventAsync([FromBody] EventDto newEvent) // PL/SQL: Procedura
   {
      var createdEvent = new Event
      {
         Id = newEvent.Id,
         Name = newEvent.Name,
         Description = newEvent.Description,
         StartTime = newEvent.StartTime,
         EndTime = newEvent.EndTime,
         Location = newEvent.Location,
         EventEmail = newEvent.EventEmail,
         IsOverLappingAllowed = newEvent.IsOverLappingAllowed,
         CoordinatorName = newEvent.CoordinatorName,
         CoordinatorSurname = newEvent.CoordinatorSurname,
         CoordinatorPhone = newEvent.CoordinatorPhone,
      };
      await context.Event.AddAsync(createdEvent);   
      await context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetSingleEventAsync), new { id = createdEvent.Id }, newEvent);
   }

   [HttpPut("{id:guid}")]
   public async Task<IActionResult> UpdateEvent(Guid id,[FromBody] UpdateEventDto updatedEvent) // PL/SQL: Procedura
   {
      var eventFromDb = await context.Event.FindAsync(id);
      if (eventFromDb == null)
      {
         return NotFound("Event not found");
      }
      //TODO: Pamiętać o sprawdzaniu timestampa.
      // Cel: Unikanie 'podwójnego' aktualizowanai tej samej encji np. przez dwóch adminów w tym samym czasie 
      context.Entry(eventFromDb).CurrentValues.SetValues(updatedEvent);
      await context.SaveChangesAsync();
      return Ok(eventFromDb);
   }

   [HttpDelete("{id:guid}")]
   public async Task<IActionResult> DeleteEvent(Guid id) // PL/SQL: Procedura
   {
      var eventFromDb = await context.Event.FindAsync(id);
      if (eventFromDb == null)
      {
         return NotFound("Event not found");
      }
      context.Event.Remove(eventFromDb);
      await context.SaveChangesAsync();
      return Ok();
   }
   
}

//TODO: PL/SQL
// - Funkcja sprawdzająca czy istnieje dany event
// - MOŻE: Ustalić limit Eventów w tej samej lokalizacji? Wtedy byłby trigger
// - 