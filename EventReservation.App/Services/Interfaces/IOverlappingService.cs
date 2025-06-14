using EventReservation.Models;
using EventReservation.Models.DTO.Session;

namespace EventReservation.App.Services.Interfaces;

public interface IOverlappingService
{
    public bool AreSessionsOverlapping(IEnumerable<Session> existingSessions);
    public bool AreSessionsOverlapping(IEnumerable<Session> existingSessions, SessionDto newSession);
    public bool AreSessionsOverlapping(IEnumerable<Session> existingSessions, Session newSession);
}
