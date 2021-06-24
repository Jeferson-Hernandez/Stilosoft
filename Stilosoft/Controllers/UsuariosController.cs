using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UsuariosController(IClienteService clienteService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _clienteService = clienteService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
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
                        return RedirectToAction("index", "Servicios");
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
    }
}
