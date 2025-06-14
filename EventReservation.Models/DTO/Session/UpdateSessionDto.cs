using System.ComponentModel.DataAnnotations;

namespace EventReservation.Models.DTO.Session;

public class UpdateSessionDto
{
     [MaxLength(50)] public string? Name { get; set; }
     [MaxLength(500)] public string? Description { get; set; }
     public DateTime? StartTime { get; set; }
     public int? Duration { get; set; } // Duration in minutes
}
