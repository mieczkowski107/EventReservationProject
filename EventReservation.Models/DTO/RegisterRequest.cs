using System.ComponentModel.DataAnnotations;

namespace EventReservation.Models;

public class RegisterRequest
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
    [Required]
    public string? ConfirmPassword { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? Phone { get; set; }
    
    public string? Country { get; set; }

    public bool ArePasswordsEqual()
    {
        return Password == ConfirmPassword;
    }
    
}