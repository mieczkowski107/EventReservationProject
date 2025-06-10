using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Models;

public class Session
{
    public Guid Id { get; set; }
    [Required] [ForeignKey("Event")] public Guid EventId { get; set; }
    [Required] [MaxLength(50)] public string? Name { get; set; }
    [Required][MaxLength(500)] public string? Description { get; set; }
    [Required] public DateTime StartTime { get; set; }
    [Required]
    [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes (24 hours).")]
    public int Duration { get; set; } // Duration in minutes
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public virtual Event? Event { get; set; }
}