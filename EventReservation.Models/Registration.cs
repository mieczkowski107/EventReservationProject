using Utility;

namespace EventReservation.Models;

public class Registration
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public RegistrationStatus RegistrationStatus { get; set; }
    public virtual AppUser? AppUser { get; set; }
    public virtual Session? Session { get; set; }
}