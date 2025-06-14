using System.ComponentModel.DataAnnotations;
using Utility;

namespace EventReservation.Models;

public class Event
{
    public int Id { get; set; }
    [Required] [MaxLength(100)] public string? Name { get; set; }
    [Required] [MaxLength(500)] public string? Description { get; set; }
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }
    [Required] [MaxLength(50)] public string? Location { get; set; }
    [Required] [MaxLength(50)] public string? EventEmail { get; set; }
    [Required] public EventSessionStatus EventSessionStatus { get; set; } = EventSessionStatus.Created;
    [Required] public bool IsOverLappingAllowed { get; set; }
    [Required] public string? CoordinatorName { get; set; }
    [Required] public string? CoordinatorSurname { get; set; }
    [Required] public string? CoordinatorPhone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<Session>? Sessions { get; set; }
    [Timestamp]
    public byte[] Version { get; set; }
}