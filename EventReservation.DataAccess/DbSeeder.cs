using Bogus;
using EventReservation.Models;
using EventReservation.Models.DTO.UserDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace EventReservation.DataAccess;
public class DbSeeder
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _db;

    public DbSeeder(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
    }
    public void Seed()
    {
        if (!_roleManager.RoleExistsAsync(nameof(Roles.Admin)).Result)
        {
            _roleManager.CreateAsync(new IdentityRole(nameof(Roles.Admin))).Wait();
        }
        if (!_roleManager.RoleExistsAsync(nameof(Roles.Client)).Result)
        {
            _roleManager.CreateAsync(new IdentityRole(nameof(Roles.Client))).Wait();
        }

        if (_userManager.Users.Count() == 0)
        {
            CreateUsers();
        }

        if (_db.Event.Count() == 0)
        {
            var events = GetEvents();
            _db.Event.AddRange(events);
            _db.SaveChanges();
        }
        if (_db.Session.Count() == 0)
        {
            var sessions = GetSessions();
            _db.Session.AddRange(sessions);
            _db.SaveChanges();
        }
        if (_db.SessionLimit.Count() == 0)
        {
            var sessions = _db.Session.ToList();
            var sessionLimits = GetSessionsLimit(sessions);
            _db.SessionLimit.AddRange(sessionLimits);
            _db.SaveChanges();
        }

        if (_db.Registration.Count() == 0)
        {
            var sessions = _db.Session.ToList();
            var sessionLimits = _db.SessionLimit.ToList();
            var users = _userManager.Users.ToList();
            var registrations = GetRegistrations(sessions, users, sessionLimits);
            _db.Registration.AddRange(registrations);
            _db.SaveChanges();
        }

    }
    private List<Event> GetEvents()
    {
        var faker = new Faker<Event>();
        faker.RuleFor(e => e.Id, f => f.Random.Guid())
             .RuleFor(e => e.Name, f => f.Company.CompanyName())
             .RuleFor(e => e.Description, f => f.Lorem.Sentence(20))
             .RuleFor(e => e.StartTime, f => f.Date.Future())
             .RuleFor(e => e.EndTime, (f, e) =>
             {
                 var durations = new[] { 1, 3, 5 };
                 var durationDays = f.PickRandom(durations);
                 return e.StartTime.AddDays(durationDays);
             })
             .RuleFor(e => e.Location, f => f.Address.FullAddress())
             .RuleFor(e => e.EventEmail, (f, e) => f.Internet.Email(e.Name))
             .RuleFor(e => e.IsOverLappingAllowed, f => f.Random.Bool())
             .RuleFor(e => e.CoordinatorName, f => f.Name.FirstName())
             .RuleFor(e => e.CoordinatorSurname, f => f.Name.LastName())
             .RuleFor(e => e.CoordinatorPhone, f => f.Phone.PhoneNumber());

        var events = faker.Generate(10);
        return events;
    }


    private List<Session> GetSessions()
    {
        int[] allowedDurations = { 15, 30, 45, 60 };
        var events = _db.Event.ToList();
        var sessions = new List<Session>();
        var faker = new Faker();

        foreach (var ev in events)
        {
            int sessionCount = faker.Random.Int(2, 5);
            DateTime currentStart = ev.StartTime;

            var sessionFaker = new Faker<Session>()
                .RuleFor(s => s.Id, f => f.Random.Guid())
                .RuleFor(s => s.EventId, f => ev.Id)
                .RuleFor(s => s.Name, f => f.Lorem.Word())
                .RuleFor(s => s.Description, f => f.Lorem.Sentence(20));

            for (int i = 0; i < sessionCount; i++)
            {
                int duration = faker.PickRandom(allowedDurations);

                DateTime startTime;
                if (ev.IsOverLappingAllowed)
                {
                    // Losowy czas startu w zakresie eventu
                    var latestStart = ev.EndTime.AddMinutes(-duration);
                    startTime = faker.Date.Between(ev.StartTime, latestStart);
                }
                else
                {
                    // Ustalany sekwencyjnie, jeśli się mieści w czasie eventu
                    if (currentStart.AddMinutes(duration) > ev.EndTime)
                        break;

                    startTime = currentStart;
                    currentStart = currentStart.AddMinutes(duration); // przesuwamy start do końca tej sesji
                }

                var session = sessionFaker.Clone()
                    .RuleFor(s => s.StartTime, _ => startTime)
                    .RuleFor(s => s.Duration, _ => duration)
                    .Generate();

                sessions.Add(session);
            }
        }

        return sessions;
    }


    private List<SessionLimit> GetSessionsLimit(List<Session> sessions)
    {
        int[] allowedMaxParticipants = { 5, 10, 20, 30 };
        var faker = new Faker();

        var sessionLimits = sessions.Select(session => new SessionLimit
        {
            Id = Guid.NewGuid(),
            SessionId = session.Id,
            MaxParticipants = faker.PickRandom(allowedMaxParticipants),
            CurrentReserved = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }).ToList();

        return sessionLimits;
    }


    private void CreateUsers()
    {
        var faker = new Faker<RegisterRequest>();

        faker.RuleFor(u => u.Email, f => f.Internet.Email())
             .RuleFor(u => u.Password, f => f.Internet.Password())
             .RuleFor(u => u.ConfirmPassword, (f, u) => u.Password)
             .RuleFor(u => u.FirstName, f => f.Name.FirstName())
             .RuleFor(u => u.LastName, f => f.Name.LastName())
             .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
             .RuleFor(u => u.Country, f => f.Address.Country());

        var users = faker.Generate(10);

        users.ForEach(users =>
        {
            var appUser = new AppUser
            {
                UserName = users.Email,
                Email = users.Email,
                FirstName = users.FirstName,
                LastName = users.LastName,
                PhoneNumber = users.Phone,
                Country = users.Country,
            };
            _userManager.CreateAsync(appUser, users.Password!).Wait();
            _userManager.AddToRoleAsync(appUser, nameof(Roles.Client)).Wait();
        });
        _db.SaveChanges();
    }

    private List<Registration> GetRegistrations(List<Session> sessions, List<AppUser> users, List<SessionLimit> sessionLimits)
    {
        var faker = new Faker();
        var registrations = new List<Registration>();
        var userSessions = new Dictionary<string, List<(DateTime Start, DateTime End)>>();

        foreach (var user in users)
        {
            userSessions[user.Id] = new List<(DateTime, DateTime)>();
            var assignedCount = 0;

            foreach (var session in faker.Random.Shuffle(sessions))
            {
                var start = session.StartTime;
                var end = session.StartTime.AddMinutes(session.Duration);
                var currentReserved = sessionLimits.Find(sl => sl.SessionId == session.Id)?.CurrentReserved ?? 0;
                var maxParticipants = sessionLimits.Find(sl => sl.SessionId == session.Id)?.MaxParticipants ?? 0;
                // jeśli brak kolizji z już przypisanymi sesjami tego użytkownika
                if (!userSessions[user.Id].Any(r => start < r.End && end > r.Start) && currentReserved < maxParticipants)
                {
                    registrations.Add(new Registration
                    {
                        Id = Guid.NewGuid(),
                        UserId = Guid.Parse(user.Id),
                        SessionId = session.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        RegistrationStatus = faker.PickRandom<RegistrationStatus>()
                    });

                    sessionLimits.Find(sessionLimits => sessionLimits.SessionId == session.Id)!.CurrentReserved++;

                    userSessions[user.Id].Add((start, end));
                    assignedCount++;

                    if (assignedCount >= 20) break;
                }
            }
        }

        return registrations;
    }

}
