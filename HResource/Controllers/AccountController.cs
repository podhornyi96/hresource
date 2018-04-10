using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using HResource.Common;
using HResource.EntityFramework.Identity;
using HResource.EntityFramework.Services.Email;
using HResource.Models;
using HResource.Models.AccountViewModels;
using HResource.Services.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace HResource.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IFacebookAuthService _facebookAuthService;
        private readonly IOptions<AuthOptions> _options;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            IFacebookAuthService facebookAuthService,
            IOptions<AuthOptions> options)
        {
            _userManager = userManager;
            _facebookAuthService = facebookAuthService;
            _options = options;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser { Email = model.Email, UserName = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //TODO: send email confiramtion

                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userToVerify = await _userManager.FindByNameAsync(viewModel.UserName);

            if (userToVerify == null || !await _userManager.CheckPasswordAsync(userToVerify, viewModel.Password))
            {
                ModelState.AddModelError("login_failure", "Login or password are incorrect");

                return BadRequest(ModelState);
            }

            var encodedJwt = GenerateToken(userToVerify);

            return new OkObjectResult(new
            {
                access_token = encodedJwt,
                username = viewModel.UserName
            });
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {


            return Ok();
        }

        [HttpGet("signin-facebook")]
        public async Task<IActionResult> FacebookLogin(string code, string state)
        {
            var tokenResponse = await _facebookAuthService.GetAccessToken(code);

            if (string.IsNullOrEmpty(tokenResponse.AccessToken))
                return InvalidLoginAttempt();

            var loginData = await _facebookAuthService.ValidateAccessToken(tokenResponse.AccessToken);

            if (!loginData.Data.IsValid)
                return InvalidLoginAttempt();

            var userData = await _facebookAuthService.GetUserData(tokenResponse.AccessToken);

            if (string.IsNullOrEmpty(userData?.Email))
                return InvalidLoginAttempt();

            var user = await _userManager.FindByEmailAsync(userData.Email);

            if (user == null)
            {
                var identityResult = await _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = userData.Email,
                    Email = userData.Email
                });

                if (!identityResult.Succeeded)
                {
                    ModelState.AddModelError("login_failure", "Failed to create local user account.");

                    return BadRequest(ModelState);
                }

                user = await _userManager.FindByEmailAsync(userData.Email);
            }

            if (user == null)
            {
                ModelState.AddModelError("login_failure", "Failed to retrieve user.");

                return BadRequest(ModelState);
            }

            var encodedJwt = GenerateToken(user);

            return new OkObjectResult(new
            {
                access_token = encodedJwt,
                username = user.UserName
            });
        }

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_options.Value.LifeTime),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Value.Key)), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private BadRequestResult InvalidLoginAttempt()
        {
            ModelState.AddModelError("login_failure", "Invalid login attempt.");

            return BadRequest();
        }

    }
}