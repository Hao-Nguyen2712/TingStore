using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TingStore.Client.Areas.Admin.Models.Users;
using TingStore.Client.Areas.Admin.Services.Users;

namespace TingStore.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Users/[action]")]
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
            return View(users);
        }

        // GET: Admin/Users/Active
        public async Task<IActionResult> Active()
        {
            var usersActive = await _userService.GetAllActiveUsers();
            return View(usersActive);
        }

        // GET: Admin/Users/Inactive
        public async Task<IActionResult> Inactive()
        {
            var usersInactive = await _userService.GetAllInactiveUsers();
            return View(usersInactive);
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
            catch (Exception e)
            {
                return View(user);
            }
        }

        // GET: Admin/Users/Edit/
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                var updateRequest = new UpdateUserRequest
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    IsActive = user.IsActive
                };
                return View(updateRequest);
            }
            catch (Exception ex)
            {
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
