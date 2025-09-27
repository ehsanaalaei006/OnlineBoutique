using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineBoutiqueCoreLayer.Services;
using OnlineBoutiqueDataLayer.Context;
using OnlineBoutiqueDataLayer.Entities;

namespace OnlineBoutiqueAdmin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }



        // GET: Users
        public async Task<IActionResult> Index()
        {
              return await _service.GetAllUsersAsync() != null ? 
                          View(await _service.GetAllUsersAsync()) :
                          Problem("Entity set 'AppDbContext.Users'  is null.");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_service.GetAllUsersAsync() == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            await _service.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
