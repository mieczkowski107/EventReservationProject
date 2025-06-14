using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace EventReservation.Models.DTO.SessionRegistration;

public class SessionRegistrationDto
{
    public int Id { get; set; }
    [Required] public int SessionId { get; set; }
    [Required] public RegistrationStatus RegistrationStatus { get; set; }
    [Required] public DateTime CreatedAt { get; set; }
}
