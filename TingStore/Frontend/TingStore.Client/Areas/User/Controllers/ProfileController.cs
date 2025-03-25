using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TingStore.Client.Areas.User.Models.UserProfile;
using TingStore.Client.Areas.User.Services.JWT;
using TingStore.Client.Areas.User.Services.UserProfile;

namespace TingStore.Client.Areas.User.Controllers
{

    [Area("User")]
    [Route("[area]/[controller]/[action]/{id?}")]

    public class ProfileController : Controller
    {
        private readonly IUserProfileService _userService;
        private readonly JwtService _jwtService;

        public ProfileController(IUserProfileService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task<IActionResult> Index()
        {
                
            try
            {
                string token = Request.Cookies["acessToken"];
                if (string.IsNullOrEmpty(token))
                {
                    ViewBag.Error = "No JWT token found.";
                    return View();
                }

                // Decode token để lấy userId từ claim
                var claimsPrincipal = _jwtService.DecodeJwt(token);
                if (claimsPrincipal == null || !claimsPrincipal.Identity.IsAuthenticated)
                {
                    ViewBag.Error = "Invalid JWT token.";
                    return View();
                }

                var userIdClaim = claimsPrincipal.FindFirst("UserID")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    ViewBag.Error = "Invalid or missing user ID in token.";
                    return View();
                }

                var user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                return View(user);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error fetching user: {ex.Message}";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] UpdateUserProfileRequest request)
        {
            if (request == null) return BadRequest("Invalid data.");

            try
            {
                var updatedUser = await _userService.UpdateUserProfile(request);
                return Ok(new { message = "Profile updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                var updateRequest = new UpdateUserProfileRequest
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    IsActive = user.IsActive
                };
                return View(updateRequest);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error fetching users: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
