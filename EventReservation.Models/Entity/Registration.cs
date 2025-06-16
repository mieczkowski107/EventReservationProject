using Utility;

namespace EventReservation.Models;

public class Registration
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SessionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public RegistrationStatus RegistrationStatus { get; set; }
    public virtual AppUser? AppUser { get; set; }
    public virtual Session? Session { get; set; }
}