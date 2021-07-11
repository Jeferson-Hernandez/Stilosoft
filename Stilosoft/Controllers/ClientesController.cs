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
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        

        public ClientesController(IClienteService clienteService, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _clienteService = clienteService;
            _roleManager = roleManager;
            _userManager = userManager;            
        }
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ObtenerListaClientes();
            return View(clientes);
        }
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioViewModel usuarioViewModel)
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
                        TempData["Accion"] = "Registrar";
                        TempData["Mensaje"] = "Cliente registrado correctamente";
                        return RedirectToAction("index");
                    }
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ingresaste un valor inválido";
                    return View(usuarioViewModel);
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ingresaste un valor inválido";
                    return RedirectToAction("index");
                }
            }
            TempData["Accion"] = "Error";
            TempData["Mensaje"] = "Ingresaste un valor inválido";
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> EditarEstado(string id)
        {
            if (id == null)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Error";
                return RedirectToAction("index");
            }
            Cliente cliente = await _clienteService.ObtenerClientePorId(id);
            try
            {
                if (cliente.Estado == true)
                    cliente.Estado = false;
                else if (cliente.Estado == false)
                    cliente.Estado = true;

                await _clienteService.EditarCliente(cliente);
                TempData["Accion"] = "EditarEstado";
                TempData["Mensaje"] = "Estado editado correctamente";
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ingresaste un valor inválido";
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Eliminar(string id)
        {
            try
            {
                if (id == null)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Error";
                    return RedirectToAction("index");
                }
                await _clienteService.EliminarCliente(id);
                TempData["Accion"] = "Eliminar";
                TempData["Mensaje"] = "Cliente eliminado correctamente";
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
