using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APIGateway.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace APIGateway.Services
{
    public class ApiTokenService : IApiTokenService
    {
        private readonly ApiGatewayDbContext _appDBContext;
        private readonly IConfiguration _config;
        public ApiTokenService(ApiGatewayDbContext context, IConfiguration config)
        {
            _appDBContext = context ??
                throw new ArgumentNullException(nameof(context));


            _config = config ??
                throw new ArgumentNullException(nameof(config));
        }
        public async Task<IEnumerable<Models.UserDetails>> GetUserDetails()
        {
            return await _appDBContext.userDetails.ToListAsync();
        }
        public AuthToken GetAuthToken(UserDetails user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtToken:SecretKey"]));// eauction_auth_scheme
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expirationDate = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtToken:TokenExpiry"]));
            //var ClientId = _config["JwtToken:ClientID"];
            var Claims = new[] { new Claim(ClaimTypes.Name, user.UserName.ToString()), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
            var token = new JwtSecurityToken(audience: _config["JwtToken:Audience"], issuer: _config["JwtToken:Issuer"], claims: Claims, expires: expirationDate, signingCredentials: credentials);
            var authToken = new AuthToken();
            authToken.ExpirationTime = expirationDate;
            authToken.accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            authToken.email = user.Email;
            authToken.userName = user.UserName.ToString();
            List<string> userRoleList = new List<string>();
            if (Convert.ToBoolean(user.IsAdmin) == true)
            {
                userRoleList.Add("ROLE_ADMIN");
            }
            userRoleList.Add(user.Role);
            authToken.roles = userRoleList;

            //var usercheck = GetRefreshTokenDetails().Result.Where(c => c.ClientId == ClientId && c.UserName == user.UserName.ToString()).SingleOrDefault();
            //if (usercheck != null)
            //{
            //    if (usercheck.Id > 0)
            //    {
            //        DeleteRefreshToken(user.UserName.ToString(), usercheck.ProtectedToken);
            //        authToken.refreshToken = GenerateRefreshToken();
            //        SaveRefreshToken(user.UserName.ToString(), authToken.refreshToken);
            //    }
            //    else
            //    {
            //        authToken.refreshToken = GenerateRefreshToken();
            //        SaveRefreshToken(user.UserName.ToString(), authToken.refreshToken);
            //    }
            //}
            //else
            //{
            //    authToken.refreshToken = GenerateRefreshToken();
            //    SaveRefreshToken(user.UserName.ToString(), authToken.refreshToken);
            //}
            return authToken;

        }

        //public async Task<IEnumerable<Models.RefreshTokendetails>> GetRefreshTokenDetails()
        //{
        //    return await _appDBContext.refreshTokenDetails.ToListAsync();
        //}
        //public string GenerateRefreshToken()
        //{
        //    var randomNumber = new byte[32];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(randomNumber);
        //        return Convert.ToBase64String(randomNumber);
        //    }
        //}
        //public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        //{
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
        //        ValidateIssuer = false,
        //        ValidateIssuerSigningKey = true,
        //        //ValidIssuer = _config["JwtToken:Issuer"],
        //        //ValidAudience = _config["JwtToken:Audience"],
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtToken:SecretKey"])),
        //        ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    SecurityToken securityToken;
        //    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        //    var jwtSecurityToken = securityToken as JwtSecurityToken;
        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
        //        throw new SecurityTokenException("Invalid token");

        //    return principal;
        //}

        //public void SaveRefreshToken(string username, string refreshToken)
        //{
        //    _appDBContext.refreshTokenDetails.Add(new RefreshTokendetails() { ClientId = _config["JwtToken:ClientID"], UserName = username, ExpireTimeInMinutes = Convert.ToInt32(_config["JwtToken:RefreshTokenExpiry"]), startTime = DateTime.UtcNow, endTime = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtToken:RefreshTokenExpiry"])), ProtectedToken = refreshToken });
        //    _appDBContext.SaveChanges();
        //}
        //public bool DeleteRefreshToken(string username, string refreshToken)
        //{
        //    bool result = false;
        //    var token = _appDBContext.refreshTokenDetails.Where(c => c.UserName == username && c.ProtectedToken == refreshToken).SingleOrDefault();
        //    if (token != null)
        //    {
        //        _appDBContext.Entry(token).State = EntityState.Deleted;
        //        _appDBContext.SaveChanges();
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }
        //    return result;
        //}

    }
}
