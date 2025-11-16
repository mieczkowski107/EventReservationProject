using EventReservation.App.Services.Interfaces;
using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Event;
using EventReservation.Models.DTO.Page;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventReservation.App.Controllers;

[Route("api/events")]
[ApiController]
//[Authorize(Roles = nameof(Roles.Admin))]
public class EventController(AppDbContext dbContext, IOverlappingService overlappingService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetEvents(int page = 1, int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var totalCount = await dbContext.Event.CountAsync();

        //TODO: dodać index na StartTime, w celu przyspieszenia OrderBy

        var events = await dbContext.Event
            .OrderBy(e => e.StartTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var eventsDto = events.Select(x => new EventDto
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
        }).ToList();

        var result = new PagedResult<EventDto>
        {
            Items = eventsDto,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            PageCount = (int)Math.Ceiling((double)totalCount / pageSize)
        };

        return Ok(result);
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
        return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto updatedEvent) // PL/SQL: Procedura
    {
        var eventFromDb = await dbContext.Event.Include(s=>s.Sessions).FirstOrDefaultAsync(e => e.Id == id);
        if (eventFromDb == null)
        {
            return NotFound("Event not found");
        }

        if (updatedEvent.StartTime.HasValue && updatedEvent.EndTime.HasValue)
        {
            if (updatedEvent.StartTime.Value >= updatedEvent.EndTime.Value)
            {
                return BadRequest("Start time must be before end time.");
            }
        }


        MapFromDtoToEntity(updatedEvent, eventFromDb);

        if (updatedEvent.IsOverLappingAllowed == false && eventFromDb.Sessions != null)
        {
            if (overlappingService.AreSessionsOverlapping(eventFromDb.Sessions.ToList()))
            {
                dbContext.Entry(eventFromDb).State = EntityState.Unchanged; // Reset changes to the event
                return BadRequest("Overlapping sessions are not allowed for this event. Delete overlapping events to change this property.");
            }
        }
        if (overlappingService.AreSessionsOverlapping(eventFromDb.Sessions?.ToList() ?? new List<Session>()))
        {
            dbContext.Entry(eventFromDb).State = EntityState.Unchanged; // Reset changes to the event
            return BadRequest("Overlapping sessions are not allowed for this event. Delete overlapping events to change this property.");
        }

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

    private void MapFromDtoToEntity(UpdateEventDto source, Event destination)
    {
        destination.Name = source.Name ?? destination.Name;
        destination.Description = source.Description ?? destination.Description;
        destination.StartTime = source.StartTime ?? destination.StartTime;
        destination.EndTime = source.EndTime ?? destination.EndTime;
        destination.Location = source.Location ?? destination.Location;
        destination.EventEmail = source.EventEmail ?? destination.EventEmail;
        destination.IsOverLappingAllowed = source.IsOverLappingAllowed ?? destination.IsOverLappingAllowed;
        destination.CoordinatorName = source.CoordinatorName ?? destination.CoordinatorName;
        destination.CoordinatorSurname = source.CoordinatorSurname ?? destination.CoordinatorSurname;
        destination.CoordinatorPhone = source.CoordinatorPhone ?? destination.CoordinatorPhone;
    }

}

//TODO: PL/SQL
// - Funkcja sprawdzająca czy istnieje dany event
// - MOŻE: Ustalić limit Eventów w tej samej lokalizacji? Wtedy byłby trigger
// - 