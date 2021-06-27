using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.Entities;
using Stilosoft.ViewModels.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;
        const string SesionNombre = "_Nombre";

        public UsuariosController(IClienteService clienteService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager)
        {
            _clienteService = clienteService;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var listaUsuariosClientes = await _clienteService.ObtenerListaClientes();
            //var listaUsuarios = await _userManager.Users.Include(c=>c.Cliente).ToListAsync();            
            return View(listaUsuariosClientes);
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = new()
                {
                    UserName = usuarioViewModel.Email,
                    Email = usuarioViewModel.Email
                    
                };

                try
                {
                    var resultado = await _userManager.CreateAsync(identityUser, usuarioViewModel.Password);
                    if (resultado.Succeeded)
                    {
                        var usuario = await _userManager.FindByEmailAsync(usuarioViewModel.Email);
                        await _userManager.AddToRoleAsync(usuario, "cliente");
                        Cliente cliente = new()
                        {
                            ClienteId = usuario.Id,
                            Nombre = usuarioViewModel.Nombre,
                            Apellido = usuarioViewModel.Apellido,
                            Celular = usuarioViewModel.Celular,
                            Cedula = usuarioViewModel.Cedula,
                            Estado = true                            
                        };
                        await _clienteService.GuardarCliente(cliente);
                        return RedirectToAction("login", "Usuarios");
                    }
                    else
                        return View(usuarioViewModel);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RecordarMe, false);
                if (resultado.Succeeded)
                {
                    var usuario = await _userManager.FindByEmailAsync(loginViewModel.Email);

                    var cliente = await _clienteService.ObtenerClientePorId(usuario.Id);

                    _httpContextAccessor.HttpContext.Session.SetString(SesionNombre, cliente.Nombre);
                    return RedirectToAction("index","Servicios");
                }
                return View();
            }
            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CrearUsuario()
        {
            //var listaRoles = await _roleManager.Roles.ToListAsync();
            var listaRoles = await _roleManager.Roles.Where(r => r.Name != "Administrador").ToListAsync();
            ViewBag.Roles = new SelectList(listaRoles, "Name", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(CrearUsuarioViewModel crearUsuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = new()
                {
                    UserName = crearUsuarioViewModel.Email,
                    Email = crearUsuarioViewModel.Email
                };
                try
                {
                    var resultado = await _userManager.CreateAsync(identityUser, crearUsuarioViewModel.Password);
                    if (resultado.Succeeded)
                    {
                        var usuario = await _userManager.FindByEmailAsync(crearUsuarioViewModel.Email);
                        await _userManager.AddToRoleAsync(usuario, crearUsuarioViewModel.Rol);

                        return RedirectToAction("index");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(crearUsuarioViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Usuarios");
        }
    }
}
