using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
  
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
            TempData["Accion"] = "Crear";
            TempData["Mensaje"] = "Rol guardado correctamente";
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(string rol)
        {
            var Rol = await _roleManager.FindByNameAsync(rol);
            var resultado = await _roleManager.DeleteAsync(Rol);
            if (resultado.Succeeded)
            {
                TempData["Accion"] = "Eliminar";
                TempData["Mensaje"] = "Rol eliminado correctamente";
                return RedirectToAction("index");
            }
            return NotFound();            
        }

        [HttpGet]
        public async Task<IActionResult> Editar(string id)
        {
            if (id == null || id == "")
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Error";
                return RedirectToAction("index");
            }
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                return View(role);
            }
            catch (Exception)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ingresaste un valor inválido";
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(IdentityRole identityRole)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(identityRole.Id);
                role.Name = identityRole.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (!result.Succeeded)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Error";
                    return RedirectToAction("index");
                }
                TempData["Accion"] = "Editar";
                TempData["Mensaje"] = "Rol editado correctamente";
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ingresaste un valor inválido";
                return RedirectToAction("index");
            }
        }



    }
}
