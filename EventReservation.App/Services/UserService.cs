using System.Security.Claims;

namespace EventReservation.App.Services;

public class UserService
{
    public static Guid? GetUserId(ClaimsPrincipal user)
    {
        Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
        return userId;
    }
}