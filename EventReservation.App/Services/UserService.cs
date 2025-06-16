using System.Security.Claims;

namespace EventReservation.App.Services;

public class UserService
{
    public static int GetUserId(ClaimsPrincipal user)
    {
        //int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
        int.TryParse(user.FindFirstValue("IntId"), out int userId);
        return userId;
    }
}