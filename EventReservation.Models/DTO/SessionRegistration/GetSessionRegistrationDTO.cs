using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventReservation.Models.DTO.Session;
using Utility;

namespace EventReservation.Models.DTO.SessionRegistration;

public class GetSessionRegistrationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SessionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public RegistrationStatus RegistrationStatus { get; set; }
    public SessionWithLimitDto? Session { get; set; }
}
