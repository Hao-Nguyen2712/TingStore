using System.Net.Http;
using System.Threading.Tasks;
using Identity.Provider.DbContext;
using Identity.Provider.Models;
using Identity.Provider.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Provider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenServices _tokenService;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(UserManager<ApplicationUser> userManager, AppDbContext context, IConfiguration configuration, ITokenServices tokenServices, RoleManager<IdentityRole> roleManager, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _tokenService = tokenServices;
            _roleManager = roleManager;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Unauthorized($"User with {request.Email} not find");

            }
            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
            {
                return Unauthorized("Invalid credentials");
            }
            var token = await _tokenService.GenerateJwtToken(user);
            if (token == null)
            {
                return Unauthorized("Token is not generated");
            }

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:RefreshTokenLifetime"]));
            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new AuthenticationResponse
            {
                Email = request.Email,
                Token = token
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {

            if (request == null)
            {
                return BadRequest("Request is null");
            }

            var emailExist = await _userManager.FindByEmailAsync(request.Email);
            if (emailExist != null)
            {
                return BadRequest("Email already exist");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var userAddNeww = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                if (userAddNeww != null)
                {
                    userAddNeww.UserId = Math.Abs(Guid.NewGuid().GetHashCode());
                    _context.Users.Update(userAddNeww);
                    await _context.SaveChangesAsync();
                }

                await _userManager.AddToRoleAsync(user, "User");

                var client = _httpClientFactory.CreateClient();
                var requestContent = new CreateUserRequest
                {
                    Id = userAddNeww.UserId ?? Math.Abs(Guid.NewGuid().GetHashCode()),
                    Email = user.Email,
                    FullName = "User",
                    Password = "Please confirm your password",
                    PhoneNumber = "123456789",
                    Address = "Please confirm your address",
                    IsActive = true
                }; 
                var response = await client.PostAsJsonAsync("http://localhost:5001/apigateway/users/CreateUser", requestContent);
              
                return Ok(new
                {
                    Email = user.Email
                });
            }
            return BadRequest("User is not created");
        }


        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync([FromHeader(Name = "Authorization")] string token)
        {
            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            {
                return BadRequest("Invalid token");
            }
            //await _signInManager.SignOutAsync();
            var jwt = token.Replace("Bearer ", "");

            var blacklistToken = new BlackListToken
            {
                Token = jwt,
                BlacklistedAt = DateTime.Now,
                ExpiryTime = DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:AccessTokenLifetime"]))
            };

            await _context.BlackListToken.AddAsync(blacklistToken);
            await _context.SaveChangesAsync();


            return Ok(new
            {
                Message = "Logout successfully"
            });
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refreshtoken([FromBody] TokenModel model)
        {
            var pricipal = await _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
            // Your refresh token logic here

            var email = pricipal.Identity.Name;

            if (email == null)
            {
                return BadRequest("Invalid token");
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid refresh token");
            }

            var token = await _tokenService.GenerateJwtToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:RefreshTokenLifetime"]));
            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new AuthenticationResponse
            {
                Email = user.Email,
                Token = token
            });

        }

        [HttpGet("check-token")]
        public async Task<IActionResult> CheckToken([FromQuery] string token)
        {
            var isRevoked = await _context.BlackListToken.AnyAsync(t => t.Token == token);
            return Ok(new
            { IsRevoked = isRevoked }
            );
        }



    }
}
