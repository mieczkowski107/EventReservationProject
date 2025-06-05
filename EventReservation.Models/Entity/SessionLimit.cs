using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Models;

public class SessionLimit
{
    public Guid Id { get; set; }
    [ForeignKey("Session")]public Guid SessionId { get; set; }
    [Required] public int MaxParticipants { get; set; }
    [Required] public int CurrentReserved { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public virtual Session? Session { get; set; }
}