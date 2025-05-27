using System.ComponentModel.DataAnnotations;

namespace EventReservation.Models.DTO.UserDTO;

public class LoginRequest
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }

    public bool RememberMe { get; set; }
}