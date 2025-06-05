using System.ComponentModel.DataAnnotations;

namespace EventReservation.Models.DTO.UserDTO;

public class LoginRequest
{
    [Required]
    [MaxLength(50)]
    public string? Email { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Password { get; set; }
    
    public bool RememberMe { get; set; }
}