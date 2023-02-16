//The GPLv3 License (GPLv3)
//
//Copyright (c) 2023 Ciubix8513
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
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