using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bwasm.Cookies.Providers;

public class CustomAuthProvider : AuthenticationStateProvider
{
    public ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(claimsPrincipal);
    }
}