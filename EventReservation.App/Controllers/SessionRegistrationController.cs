using EventReservation.App.Services;
using EventReservation.App.Services.Interfaces;
using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Event;
using EventReservation.Models.DTO.Page;
using EventReservation.Models.DTO.Session;
using EventReservation.Models.DTO.SessionRegistration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Utility;

namespace EventReservation.App.Controllers;

[ApiController]
//[Authorize(Roles = nameof(Roles.Client))]
public class SessionRegistrationController(AppDbContext dbContext, IOverlappingService overlap) : ControllerBase
{
    [HttpGet]
    [Route("api/registrations")]
    public async Task<IActionResult> GetUserRegistration(int page = 1, int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var userId = UserService.GetUserId(User);

        var query = dbContext.Registration
            .Where(r => r.UserId == userId);

        var totalCount = await query.CountAsync();

        var registrations = await query
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionLimit)
            .OrderBy(r => r.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var items = registrations.Select(r => new GetSessionRegistrationDto
        {
            Id = r.Id,
            UserId = userId,
            SessionId = r.SessionId,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt,
            RegistrationStatus = r.RegistrationStatus,
            Session = new SessionWithLimitDto
            {
                Id = r.Session.Id,
                Name = r.Session.Name,
                Description = r.Session.Description,
                StartTime = r.Session.StartTime,
                Duration = r.Session.Duration,
                MaxParticipants = r.Session.SessionLimit?.MaxParticipants ?? 0,
                CurrentReserved = r.Session.SessionLimit?.CurrentReserved ?? 0
            }
        }).ToList();

        var result = new PagedResult<GetSessionRegistrationDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            PageCount = (int)Math.Ceiling((double)totalCount / pageSize)
        };

        return Ok(result);
    }
    [HttpPost]
    [Route("api/session/{sessionId:int}/registrations")]
    public async Task<IActionResult> SessionSignIn(int sessionId)
    {
        var userId = UserService.GetUserId(User);

        /*
        if (await dbContext.Registration.CountAsync(r => r.UserId == userId && r.SessionId == sessionId) > 0)
        {
            return BadRequest("User is already registered for this session.");
        }
        */
        var session = await dbContext.Session
            .Include(s => s.Event)
            .SingleOrDefaultAsync(s => s.Id == sessionId);
        if (session == null || session.Event == null)
        {
            return NotFound("Session not found.");
        }

        if (session.Event.IsOverLappingAllowed == true)
        {
            var existingSessions = await dbContext.Registration
                .Where(r => r.UserId == userId && r.Session.EventId == session.EventId)
                .Select(r => r.Session)
                .ToListAsync();

            if (existingSessions != null)
            {
                if (overlap.AreSessionsOverlapping(existingSessions, session))
                {
                    return BadRequest("Session overlaps with existing sessions.");
                }
            }

        }

        try
        {
            dbContext.Database
                     .ExecuteSqlInterpolated($"BEGIN SESSION_PKG.REGISTER_FOR_SESSION({sessionId},{userId}); END;");
        }
        catch (OracleException ex)
        {
            if (ex.Number >= 20000 && ex.Number <= 20999)
            {
                switch (ex.Number)
                {
                    case 20010: // Sesja o ID nie istnieje
                        return NotFound(ex.Message);

                    case 20011: // Nie można zapisać się na sesję, bo już się odbyła
                    case 20012: // Użytkownik jest już zarejestrowany na tę sesje
                    case 20013: // Sesja jest pełna, brak miejsc
                        return BadRequest(ex.Message);

                    default:
                        // Inny błąd aplikacyjny
                        return StatusCode(500);
                }
            }
        }

        dbContext.SaveChanges();
        /*  var limit = await dbContext.SessionLimit.SingleOrDefaultAsync(sl => sl.SessionId == sessionId);
          if (limit == null)
          {
              return BadRequest("Session limit not configured.");
          }
          if (limit.CurrentReserved >= limit.MaxParticipants)
          {
              return BadRequest("Session is full.");
          }

          var userRegistration = new Registration
          {
              SessionId = sessionId,
              UserId = userId,
              CreatedAt = DateTime.UtcNow
          };
          dbContext.Registration.Add(userRegistration);

          limit.CurrentReserved++;
          dbContext.SessionLimit.Update(limit);

          // Race condition handling
          try
          {
              await dbContext.SaveChangesAsync();
          }
          catch (Exception)
          {

              return Conflict("Please try again or choose a different session.");
          }
        */

        var userRegistration = await dbContext.Registration.Where(r => r.UserId == userId && r.SessionId == sessionId).FirstOrDefaultAsync();


        var sessionRegistrationDto = new SessionRegistrationDto
        {
            Id = userRegistration!.Id,
            SessionId = userRegistration.SessionId,
            RegistrationStatus = userRegistration.RegistrationStatus,
            CreatedAt = userRegistration.CreatedAt
        };


        var eventDto = new ViewEventDto();
        var sessionDto = new SessionDto();
        MapFromEntityToDto(session.Event!, eventDto);
        MapFromEntityToDto(session, sessionDto);
        var sessionWithEventDto = new SessionWithEventDto
        {
            EventDto = eventDto,
            SessionDto = sessionDto
        };

        return Ok(sessionWithEventDto);
    }
    [HttpDelete]
    [Route("api/session/{sessionId:int}/registrations")]
    public async Task<IActionResult> SessionSignOut(int sessionId)
    {
        var userId = UserService.GetUserId(User);
        try
        {
            dbContext.Database
                     .ExecuteSqlInterpolated($"BEGIN SESSION_PKG.UNREGISTER_FROM_SESSION({sessionId},{userId}); END;");

        }
        catch (OracleException ex)
        {
            return BadRequest("You are already signed out");
        }

        /*  var userRegistration = await dbContext.Registration.SingleOrDefaultAsync(u => u.UserId == userId &&  u.SessionId == sessionId);
          if (userRegistration == null) return NotFound("Registration not found.");

          var limit = await dbContext.SessionLimit.SingleOrDefaultAsync(sl => sl.SessionId == sessionId);
          limit.CurrentReserved--;
          dbContext.SessionLimit.Update(limit);

          dbContext.Remove(userRegistration);

          await dbContext.SaveChangesAsync();*/
        return NoContent();
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
    private static void MapFromEntityToDto(Session session, SessionDto sessionDto)
    {
        sessionDto.Id = session.Id;
        sessionDto.Name = session.Name;
        sessionDto.Description = session.Description;
        sessionDto.StartTime = session.StartTime;
        sessionDto.Duration = session.Duration;
        sessionDto.MaxParticipants = session.SessionLimit?.MaxParticipants ?? 0;
    }
}