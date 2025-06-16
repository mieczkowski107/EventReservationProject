using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Models.DTO.Session;

public class SessionWithLimitDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public int Duration { get; set; } // Duration in minutes
    public int MaxParticipants { get; set; }
    public int CurrentReserved { get; set; } = 0;
}
