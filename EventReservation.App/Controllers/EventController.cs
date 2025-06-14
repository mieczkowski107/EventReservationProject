using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventReservation.App.Controllers;

[Route("api/events")]
[ApiController]
//[Authorize(Roles = nameof(Roles.Admin))]
public class EventController(AppDbContext dbContext) : ControllerBase
{
   [HttpGet]
   public async Task<IActionResult> GetEvents()
   {
      var allEvents = await dbContext.Event.ToListAsync();
      var eventsDto = allEvents.Select(x => new EventDto
      {
         Id = x.Id,
         Name = x.Name,
         Description = x.Description,
         StartTime = x.StartTime,
         EndTime = x.EndTime,
         Location = x.Location,
         EventEmail = x.EventEmail,
         IsOverLappingAllowed = x.IsOverLappingAllowed,
         CoordinatorName = x.CoordinatorName,
         CoordinatorSurname = x.CoordinatorSurname,
         CoordinatorPhone = x.CoordinatorPhone,
      });
      return Ok(eventsDto);
   }

   [HttpGet("{id:int}")]
   public async Task<IActionResult> GetEvent(int id) // PL/SQL: Procedura
   {
      var eventFromDb = await dbContext.Event.FindAsync(id);
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
   public async Task<IActionResult> CreateEvent([FromBody] EventDto newEvent) // PL/SQL: Procedura
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
      await dbContext.Event.AddAsync(createdEvent);   
      await dbContext.SaveChangesAsync();
        //TODO: Should return DTO, not Entity
        return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
   }

   [HttpPut("{id:int}")]
   public async Task<IActionResult> UpdateEvent(int id,[FromBody] UpdateEventDto updatedEvent) // PL/SQL: Procedura
   {
      var eventFromDb = await dbContext.Event.FindAsync(id);
      if (eventFromDb == null)
      {
         return NotFound("Event not found");
      }
      //TODO: Pamiętać o sprawdzaniu timestampa.
      // Cel: Unikanie 'podwójnego' aktualizowanai tej samej encji np. przez dwóch adminów w tym samym czasie 
      dbContext.Entry(eventFromDb).CurrentValues.SetValues(updatedEvent);
      await dbContext.SaveChangesAsync();
      var eventDto = new EventDto
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
      return Ok(eventDto);
   }

   [HttpDelete("{id:int}")]
   public async Task<IActionResult> DeleteEvent(int id) // PL/SQL: Procedura
   {
      var eventFromDb = await dbContext.Event.FindAsync(id);
      if (eventFromDb == null)
      {
         return NotFound("Event not found");
      }
      dbContext.Event.Remove(eventFromDb);
      await dbContext.SaveChangesAsync();
      return NoContent();
   }
   
}

//TODO: PL/SQL
// - Funkcja sprawdzająca czy istnieje dany event
// - MOŻE: Ustalić limit Eventów w tej samej lokalizacji? Wtedy byłby trigger
// - 