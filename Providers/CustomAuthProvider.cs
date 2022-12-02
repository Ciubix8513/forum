using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bwasm.Cookies.Provider
{

    public class CustomAuthProvider : AuthenticationStateProvider
    {
        public ClaimsPrincipal claimsPrincipal = new(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(claimsPrincipal);
        }
        public void ClearAuthInfo()
        {
            ClaimsPrincipal claimsPrincipal = new(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public void SetAuthInfo(UserProfileModel userPofile)
        {
            var identity = new ClaimsIdentity(new[]{
                new Claim("Username",userPofile.Username),
                new Claim("UserId",userPofile.UserID.ToString()),
                new Claim("BIO",userPofile.BIO),
                new Claim("Privilege",userPofile.Privilege.ToString()),
                new Claim("PFP",userPofile.PFP)
            },"AuthCookie");
            claimsPrincipal = new(identity);
            // identity.`
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}