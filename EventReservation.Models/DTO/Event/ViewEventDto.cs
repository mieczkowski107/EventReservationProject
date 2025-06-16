using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Models.DTO.Event;

public class ViewEventDto
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public string EventEmail { get; set; } = string.Empty;
    public string CoordinatorName { get; set; } = string.Empty;
    public string CoordinatorSurname { get; set; } = string.Empty;
    public string CoordinatorPhone { get; set; } = string.Empty;
}
