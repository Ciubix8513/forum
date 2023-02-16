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