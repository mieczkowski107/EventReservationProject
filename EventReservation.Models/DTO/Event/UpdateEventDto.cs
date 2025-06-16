using System.ComponentModel.DataAnnotations;

namespace EventReservation.Models.DTO.Event;

public class UpdateEventDto
{
    [MaxLength(50)] public string? Name { get; set; }
    [MaxLength(500)] public string? Description { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    [MaxLength(50)] public string? Location { get; set; }
    [MaxLength(50)] public string? EventEmail { get; set; }
    public bool? IsOverLappingAllowed { get; set; }
    public string? CoordinatorName { get; set; }
    public string? CoordinatorSurname { get; set; }
    public string? CoordinatorPhone { get; set; }
}