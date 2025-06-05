using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Models;

public class Session
{
    public Guid Id { get; set; }
    [Required] [ForeignKey("Event")] public Guid EventId { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public string? Description { get; set; }
    [Required] public DateTime StartTime { get; set; }
    [Required] public int Duration { get; set; } // Duration in minutes
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public virtual Event? Event { get; set; }
}