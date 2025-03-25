using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TingStore.Client.Areas.User.Models.UserProfile;
using TingStore.Client.Areas.User.Services.UserProfile;

namespace TingStore.Client.Areas.User.Controllers
{

    [Area("User")]
    [Route("[area]/[controller]/[action]/{id?}")]

    public class ProfileController : Controller
    {
        private readonly IUserProfileService _userService;

        public ProfileController(IUserProfileService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
        try
        {
        int userId = 1; // ID của user cần lấy
        var user = await _userService.GetUserById(userId);
        if (user == null)
        {
            return RedirectToAction("Index");
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
