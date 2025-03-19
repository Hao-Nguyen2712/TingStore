using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TingStore.Client.Areas.Admin.Models.Users;
using TingStore.Client.Areas.Admin.Services.Users;

namespace TingStore.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Users/[action]/{id?}")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            ViewBag.TotalUsers = await _userService.GetAllUsersCount();
            ViewBag.ActiveUsers = await _userService.GetAllActiveUsersCount();
            ViewBag.InactiveUsers = await _userService.GetAllInactiveUsersCount();
            return View(users);
        }

        // GET: Admin/Users/Active
        public async Task<IActionResult> Active()
        {
            var usersActive = await _userService.GetAllActiveUsers();
            ViewBag.TotalUsers = await _userService.GetAllUsersCount();
            ViewBag.ActiveUsers = await _userService.GetAllActiveUsersCount();
            ViewBag.InactiveUsers = await _userService.GetAllInactiveUsersCount();
            return View("Index", usersActive);
        }

        // GET: Admin/Users/Inactive
        public async Task<IActionResult> Inactive()
        {
            var usersInactive = await _userService.GetAllInactiveUsers();
            ViewBag.TotalUsers = await _userService.GetAllUsersCount();
            ViewBag.ActiveUsers = await _userService.GetAllActiveUsersCount();
            ViewBag.InactiveUsers = await _userService.GetAllInactiveUsersCount();
            return View("Index", usersInactive);
        }


        // GET: Admin/Users/Details/
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserRequest user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            try
            {
                var userResponse = await _userService.CreateUser(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error fetching users: {ex.Message}";
                return View(user);
            }
        }

        // GET: Admin/Users/Edit/
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                var updateRequest = new UpdateUserRequest
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
            catch (Exception ex){
                ViewBag.Error = $"Error fetching users: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateUserRequest user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            try
            {
                await _userService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error fetching users: {ex.Message}";
                return View(user);
            }
        }

        // GET: Admin/Users/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserById(id);
            return View(user);
        }

        // POST: Admin/Users/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                //return RedirectToAction("Index");
                return Json(new { success = true, message = "User banned successfully" });
            }
            catch (Exception ex)
            {
                //ViewBag.Error = $"Error fetching users: {ex.Message}";
                //return View();
                return Json(new { success = false, message = $"Error banning user: {ex.Message}" });
            }

        }

        // GET: Admin/Users/Restore
        public async Task<IActionResult> Restore(int id)
        {
            var user = await _userService.GetUserById(id);
            return View(user);
        }

        // POST: Admin/Users/Restore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(int id)
        {
            try
            {
                await _userService.RestoreUser(id);
                //return RedirectToAction("Index");
                return Json(new { success = true, message = "User unbanned successfully" });
            }
            catch (Exception ex)
            {
                //ViewBag.Error = $"Error fetching users: {ex.Message}";
                //return View();
                return Json(new { success = false, message = $"Error unbanning user: {ex.Message}" });
            }
        }

    }
}
