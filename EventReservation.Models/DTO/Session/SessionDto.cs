using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Models.DTO.Session;

public class SessionDto
{
    public Guid Id { get; set; }
    [Required][MaxLength(50)] public string? Name { get; set; }
    [Required][MaxLength(500)] public string? Description { get; set; }
    [Required] public DateTime StartTime { get; set; }
    [Required][Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes (24 hours).")] 
    public int Duration { get; set; } // Duration in minutes
}
