using EventReservation.Models.DTO.Event;
using EventReservation.Models.DTO.Session;
using EventReservation.Models.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Models.DTO.Export;

public class EventJsonDto
{
    public ViewEventDto Event { get; set; } = new ViewEventDto();
    public List<SessionUsersPair> SessionUser { get; set; } = new ();
}
