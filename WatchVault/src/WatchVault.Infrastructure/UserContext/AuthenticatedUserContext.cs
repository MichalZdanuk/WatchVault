using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WatchVault.Application.Common;
public class AuthenticatedUserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid UserId
    {
        get
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new ArgumentException("Could not resolve NameIdentifier from token");
            }
            return userId;
        }
    }
}