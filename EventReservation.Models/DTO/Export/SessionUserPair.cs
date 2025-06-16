using EventReservation.Models.DTO.Session;
using EventReservation.Models.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Models.DTO.Export;

public class SessionUsersPair
{
    public SessionWithLimitDto Session { get; set; } = new();
    public List<ViewUsersDto> Users { get; set; } = new();
}
