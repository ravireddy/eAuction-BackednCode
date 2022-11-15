using APIGateway.Models;
using APIGateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IApiTokenService _tokenService = null;

        public AuthController(IApiTokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public ActionResult<AuthToken>AuthenticateUser([FromBody] AuthUser user)
        {
            var userCheck = _tokenService.GetUserDetails().Result.Where(c => c.UserName == user.UserName && c.Password==user.Password).SingleOrDefault();
            if (userCheck ==null || userCheck.UserId == 0)
            {
                return BadRequest(new { message = "Invalid credentials" });
            }
            return _tokenService.GetAuthToken(userCheck);
        }
        //[HttpPost]
        //[Route("refreshtoken")]
        //[HttpPost]
        //public IActionResult Refresh([FromBody] AuthToken token )
        //{
        //    var principal = _tokenService.GetPrincipalFromExpiredToken(token.accessToken);
        //    var username = principal.Identity.Name;
        //    var savedRefreshToken = _tokenService.GetRefreshTokenDetails().Result.Where(c => c.UserName == username.ToString() && c.ProtectedToken == token.refreshToken).SingleOrDefault(); //retrieve the refresh token from a data store

        //    if (savedRefreshToken.ProtectedToken != token.refreshToken)
        //    return BadRequest(new { message = "Invalid refresh token" });

        //    var newJwtToken = _tokenService.GetAuthToken(new UserDetails() {UserName= username.ToString() });

        //    return new ObjectResult(new
        //    {
        //        accessToken = newJwtToken.accessToken,
        //        refreshToken = newJwtToken.refreshToken
        //    });
        //}

       
    }
}
