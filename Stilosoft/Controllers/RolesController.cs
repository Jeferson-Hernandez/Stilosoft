using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
   //[Authorize]
    public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(string rol)
        {
            await _roleManager.CreateAsync(new IdentityRole(rol));
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(string rol)
        {
            var Rol = await _roleManager.FindByNameAsync(rol);
            var resultado = await _roleManager.DeleteAsync(Rol);
            if (resultado.Succeeded)
            {
                return RedirectToAction("index");
            }
            return NotFound();
            
        }



    }
}
