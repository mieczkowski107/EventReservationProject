using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EventReservation.Models;

public class AppUser : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    
    [MaxLength(50)]
    public string? Country { get; set; }
}