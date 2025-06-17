using EventReservation.Models.DTO.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Models.DTO.Session;

public class SessionWithEventDto
{
    public ViewEventDto EventDto { get; set; } = new();
    public SessionDto SessionDto { get; set; } = new();

}
