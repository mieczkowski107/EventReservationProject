using EventReservation.Models.DTO.Event;
using EventReservation.Models.DTO.Session;
using EventReservation.Models.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Models.DTO.Export;

public class SessionJsonDto
{
    public ViewEventDto Event { get; set; } = new ViewEventDto();
    public SessionWithLimitDto Session { get; set; } = new SessionWithLimitDto();
    public List<ViewUsersDto> Participants { get; set; }  = new List<ViewUsersDto>();
}
