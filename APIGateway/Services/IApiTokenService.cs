using APIGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APIGateway.Services
{
   public interface IApiTokenService
    {
        Task<IEnumerable<Models.UserDetails>> GetUserDetails();
        AuthToken GetAuthToken(UserDetails user);
        //Task<IEnumerable<Models.RefreshTokendetails>> GetRefreshTokenDetails();
        //string GenerateRefreshToken();
        //ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        //void SaveRefreshToken(string username, string refreshToken);
        //bool DeleteRefreshToken(string username, string refreshToken);

    }
}
