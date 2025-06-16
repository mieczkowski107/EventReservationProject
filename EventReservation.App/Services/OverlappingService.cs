using EventReservation.App.Services.Interfaces;
using EventReservation.Models;
using EventReservation.Models.DTO.Session;

namespace EventReservation.App.Services;

public class OverlappingService : IOverlappingService
{
    public bool AreSessionsOverlapping(IEnumerable<Session> existingSessions, SessionDto newSession)
    {
        foreach (var session in existingSessions)
        {
            if (IsSessionOverlapping(session, newSession))
            {   
                return true;
            }
        }
        return false;
    }
    public bool AreSessionsOverlapping(IEnumerable<Session> existingSessions, Session newSession)
    {
        foreach (var session in existingSessions)
        {
            if (IsSessionOverlapping(session, newSession))
            {
                return true;
            }
        }
        return false;
    }
    public bool AreSessionsOverlapping(IEnumerable<Session> existingSessions)
    {
        var iterator = existingSessions.GetEnumerator();
        if (!iterator.MoveNext()) return false;
        while (iterator.MoveNext())
        {
            var currentSession = iterator.Current;
            foreach (var session in existingSessions)
            {
                if (currentSession.Id != session.Id && IsSessionOverlapping(currentSession, session))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool IsSessionOverlapping(Session session1, Session session2)
    {
        var sessionStartTime = session1.StartTime;
        var sessionEndTime = session1.StartTime.AddMinutes(session1.Duration);
        var newSessionStartTime = session2.StartTime;
        var newSessionEndTime = session2.StartTime.AddMinutes(session2.Duration);
        return newSessionStartTime < sessionEndTime && newSessionEndTime > sessionStartTime;
    }
    private bool IsSessionOverlapping(Session session, SessionDto newSession)
    {
        var sessionStartTime = session.StartTime;
        var sessionEndTime = session.StartTime.AddMinutes(session.Duration);
        var newSessionStartTime = newSession.StartTime;
        var newSessionEndTime = newSession.StartTime.AddMinutes(newSession.Duration);
        return newSessionStartTime < sessionEndTime && newSessionEndTime > sessionStartTime;
    }
}
