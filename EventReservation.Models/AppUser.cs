using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EventReservation.Models;

public class AppUser : IdentityUser
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    public string? Country { get; set; }
}