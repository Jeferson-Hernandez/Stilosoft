using Stilosoft.Business.Dtos.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.Entities;
using Stilosoft.ViewModels.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;



namespace Stilosoft.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        const string SesionNombre = "_Nombre";

        public UsuariosController(IClienteService clienteService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _clienteService = clienteService;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _configuration = configuration;
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
        public IActionResult OlvidePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OlvidePassword(OlvidePasswordDto olvidePasswordDto)
        {
            if (ModelState.IsValid)
            {
                // buscamos el email a ver si existe
                var usuario = await _userManager.FindByEmailAsync(olvidePasswordDto.Email);

                if (usuario != null)
                {
                    //generamos un token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

                    //creamos un link para resetear el password
                    var passwordresetLink = Url.Action("ResetearPassword", "Usuarios",
                        new { email = olvidePasswordDto.Email, token = token }, Request.Scheme);

                    //Opción 1

                    MailMessage mensaje = new();
                    mensaje.To.Add(olvidePasswordDto.Email); //destinatario
                    mensaje.Subject = "Recuperar contraseña";
                    
                    mensaje.Body = passwordresetLink;
                    mensaje.IsBodyHtml = false;
                    mensaje.From = new MailAddress(_configuration["Mail"], "Maria C Stilos");
                    SmtpClient smtpClient = new("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(_configuration["Mail"], _configuration["Password"]);
                    smtpClient.Send(mensaje);
                    return View("OlvidePasswordConfirmacion");

                }else
                {
                    return View(olvidePasswordDto);
                }
            }


            return View(olvidePasswordDto);
        }

        //Cuando hacemos clic en el link que llegó al correo
        [HttpGet]
        public IActionResult ResetearPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Error token");
            }
            return View();
        }
        //Cuando hacemos clic en el link que llegó al correo
        [HttpPost]
        public async Task<IActionResult> ResetearPassword(ResetearPasswordDto resetearPasswordDto)
        {
            if (ModelState.IsValid)
            {
                //buscamos el usuario
                var usuario = await _userManager.FindByEmailAsync(resetearPasswordDto.Email);

                if (usuario != null)
                {
                    //se resetea el password
                    var result = await _userManager.ResetPasswordAsync(usuario, resetearPasswordDto.Token, resetearPasswordDto.Password);
                    if (result.Succeeded)
                        return View("ResetearPasswordConfirmacion");
                    else
                    {
                        foreach (var errores in result.Errors)
                        {
                            if (errores.Description.ToString().Equals("Invalid token."))
                                ModelState.AddModelError("", "El token es invalido");
                        }
                        return View(resetearPasswordDto);
                    }
                }
                return View(resetearPasswordDto);
            }
            return View(resetearPasswordDto);
        }

        [HttpGet]
        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
