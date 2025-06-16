using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Event;
using EventReservation.Models.DTO.Export;
using EventReservation.Models.DTO.Session;
using EventReservation.Models.DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Text;

namespace EventReservation.App.Controllers;

[ApiController]
public class ExportController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet("api/sessions/{id:int}/export/json")]
    public async Task<IActionResult> ExportSessionToJson(int id)
    {
        var session = await dbContext.Session.Include(s => s.SessionLimit)
                                             .Include(s => s.Event)
                                             .Include(s => s.Registrations)
                                             .ThenInclude(r => r.AppUser)
                                             .FirstOrDefaultAsync(s => s.Id == id);

        if (session == null || session.Event == null)
        {
            return NotFound();
        }

        session.Registrations ??= new List<Registration>();
        if (session.SessionLimit == null)
        {
            return BadRequest("Session limit not found for the session.");
        }



        var ViewEventDto = new ViewEventDto();
        var SessionWithLimitDto = new SessionWithLimitDto();
        var participants = new List<ViewUsersDto>();

        MapFromEntityToDto(session.Event, ViewEventDto);
        MapFromEntityToDto(session, session.SessionLimit, SessionWithLimitDto);
        foreach (var registration in session.Registrations)
        {
            var userDto = new ViewUsersDto();
            MapFromEntityToDto(registration.AppUser, userDto);
            participants.Add(userDto);
        }

        var exportDto = new SessionJsonDto
        {
            Event = ViewEventDto,
            Session = SessionWithLimitDto,
            Participants = participants
        };

        var jsonString = exportDto.ToJson(); 
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        return new FileStreamResult(stream, "application/json")
        {
            FileDownloadName = $"session_{exportDto.Event.Name}_{exportDto.Session.Name}_{DateTime.UtcNow}.json"
        };
    }
    [HttpGet("api/events/{id:int}/export/json")]
    public async Task<IActionResult> ExportEventToJson(int id)
    {
        var @event = await dbContext.Event.FirstOrDefaultAsync(e => e.Id == id);
        var sessions = await dbContext.Session.Include(s => s.SessionLimit)
                                            .Include(s => s.Registrations)
                                            .ThenInclude(r => r.AppUser)
                                            .Where(s => s.Event.Id == id)
                                            .ToListAsync();

        if (sessions == null || @event == null)
        {
            return NotFound();
        }
        var eventJsonDto = new EventJsonDto();
        var viewEventDto = new ViewEventDto();
        MapFromEntityToDto(@event, viewEventDto);

        foreach (var session in sessions)
        {
            session.Registrations ??= new List<Registration>();
            if (session.SessionLimit == null)
            {
                return BadRequest("Session limit not found for the session.");
            }
            var SessionWithLimitDto = new SessionWithLimitDto();
            var participants = new List<ViewUsersDto>();

            MapFromEntityToDto(session, session.SessionLimit, SessionWithLimitDto);
            foreach (var registration in session.Registrations)
            {
                var userDto = new ViewUsersDto();
                MapFromEntityToDto(registration.AppUser, userDto);
                participants.Add(userDto);
            }
            var sessionUsersPair = new SessionUsersPair
            {
                Session = SessionWithLimitDto,
                Users = participants
            };
            eventJsonDto.SessionUser.Add(sessionUsersPair);
        }
        eventJsonDto.Event = viewEventDto;

        var jsonString = eventJsonDto.ToJson(); 
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)); 
        return new FileStreamResult(stream, "application/json")
        {
            FileDownloadName = $"event_{eventJsonDto.Event.Name}_{DateTime.UtcNow}.json"
        };
    }

    private static void MapFromEntityToDto(Session session, SessionLimit sessionLimit, SessionWithLimitDto dto)
    {
        dto.Id = session.Id;
        dto.Name = session.Name;
        dto.Description = session.Description;
        dto.StartTime = session.StartTime;
        dto.Duration = session.Duration;
        dto.MaxParticipants = sessionLimit.MaxParticipants;
        dto.CurrentReserved = sessionLimit.CurrentReserved;
    }
    private static void MapFromEntityToDto(AppUser user, ViewUsersDto dto)
    {
        dto.Id = user.IntId;
        dto.Name = user.FirstName;
        dto.Surname = user.LastName;
        dto.Email = user.Email;
        dto.Phone = user.PhoneNumber;
    }
    private static void MapFromEntityToDto(Event @event, ViewEventDto dto)
    {
        dto.Id = @event.Id;
        dto.Name = @event.Name;
        dto.Description = @event.Description;
        dto.StartTime = @event.StartTime;
        dto.EndTime = @event.EndTime;
        dto.Location = @event.Location!;
        dto.EventEmail = @event.EventEmail!;
        dto.CoordinatorName = @event.CoordinatorName!;
        dto.CoordinatorSurname = @event.CoordinatorSurname!;
        dto.CoordinatorPhone = @event.CoordinatorPhone!;
    }

    private static void MapFromEntityToDto(Event @event, EventJsonDto dto)
    {
        dto.Event.Id = @event.Id;
        dto.Event.Name = @event.Name;
        dto.Event.Description = @event.Description;
        dto.Event.StartTime = @event.StartTime;
        dto.Event.EndTime = @event.EndTime;
        dto.Event.Location = @event.Location!;
        dto.Event.EventEmail = @event.EventEmail!;
        dto.Event.CoordinatorName = @event.CoordinatorName!;
        dto.Event.CoordinatorSurname = @event.CoordinatorSurname!;
        dto.Event.CoordinatorPhone = @event.CoordinatorPhone!;
    }



}
