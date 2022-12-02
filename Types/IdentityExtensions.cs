using System.Security.Claims;
using System.Security.Principal;

public static class IdentityExtensions
{
    public static string GetClaimByName(this IIdentity identity,string name)
    {
        var claimsIdentity = identity as ClaimsIdentity;
        var claim = claimsIdentity?.FindFirst(name);
        if(claim == null)
            return "";
        return claim.Value;
    }
}