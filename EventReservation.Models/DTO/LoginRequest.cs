using System.ComponentModel.DataAnnotations;

namespace EventReservation.Models;

public class LoginRequest
{
    [Required]
    public string? Login { get; set; }
    [Required]
    public string? Password { get; set; }

    public bool RememberMe { get; set; }
}