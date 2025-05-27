using System.ComponentModel.DataAnnotations;

namespace EventReservation.Models.DTO.UserDTO;

public class RegisterRequest
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
    public string? Password { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Confirm Password must be at least 6 characters long.", MinimumLength = 6)]
    public string? ConfirmPassword { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
    public string? LastName { get; set; }
    [Required]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    [RegularExpression(@"^\+?[1-9]\d{9,14}$", ErrorMessage = "Invalid Phone Number Format")]
    public string? Phone { get; set; }

    [StringLength(50, ErrorMessage = "Country cannot be longer than 50 characters.")]
    public string? Country { get; set; }

    public bool ArePasswordsEqual()
    {
        return Password == ConfirmPassword;
    }
    
}