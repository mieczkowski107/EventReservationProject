using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Models;

public class SessionLimit
{
    public int Id { get; set; }
    [ForeignKey("Session")]public int SessionId { get; set; }
    [Required] public int MaxParticipants { get; set; }
    [Required] public int CurrentReserved { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public virtual Session? Session { get; set; }
    [Timestamp]
    public byte[] Version { get; set; }
}