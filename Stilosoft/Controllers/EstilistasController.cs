using Microsoft.AspNetCore.Mvc;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.Entities;
using Stilosoft.ViewModels.Estilistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class EstilistasController : Controller
    {
        private readonly IEstilistaService _estilistaService;

        public EstilistasController(IEstilistaService estilistaService)
        {
            _estilistaService = estilistaService;
        }
        public async Task<IActionResult> Index()
        {            
            return View(await _estilistaService.ObtenerListaEstilistas());
        }
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(EstilistaViewModel estilistaViewModel)
        {
            if (ModelState.IsValid)
            {
                Estilista estilista = new()
                {
                    Nombre = estilistaViewModel.Nombre,
                    FechaNacimiento = estilistaViewModel.FechaNacimiento,
                    Cedula = estilistaViewModel.Cedula,
                    Celular = estilistaViewModel.Celular,
                    Estado = true
                };
                try
                {
                    var CedulaExiste = await _estilistaService.CedulaEstilistaExiste(estilista.Cedula);
                    if (CedulaExiste != null)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "La cédula ya se encuentra registrada";
                        return View(estilistaViewModel);
                    }
                    else if (estilista.FechaNacimiento.Year >= (DateTime.Now.Year - 15))
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "La edad debe ser mayor de 15 años";
                        return View(estilistaViewModel);
                    }
                    await _estilistaService.GuardarEstilista(estilista);
                    TempData["Accion"] = "Crear";
                    TempData["Mensaje"] = "Estilista guardado correctamente";
                    return RedirectToAction("index");
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
            return View(estilistaViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Error";
                return RedirectToAction("index");
            }
            Estilista estilista = await _estilistaService.ObtenerEstilistaPorId(id.Value);
            EstilistaViewModel estilistaViewModel = new()
            {
                EstilistaId = estilista.EstilistaId,
                Nombre = estilista.Nombre,
                FechaNacimiento = estilista.FechaNacimiento,
                Cedula = estilista.Cedula,
                Celular = estilista.Celular,
                Estado = estilista.Estado
            };
            return View(estilistaViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(EstilistaViewModel estilistaViewModel)
        {
            if (ModelState.IsValid)
            {
                Estilista estilista = new()
                {
                    Nombre = estilistaViewModel.Nombre,
                    FechaNacimiento = estilistaViewModel.FechaNacimiento,
                    Cedula = estilistaViewModel.Cedula,
                    Celular = estilistaViewModel.Celular,
                    Estado = estilistaViewModel.Estado,
                    EstilistaId = estilistaViewModel.EstilistaId
                };

                try
                {
                    /*var cedulaExiste = await _estilistaService.CedulaEstilistaExisteEditar(estilistaViewModel.Cedula);
                    if (cedulaExiste.Count().Equals(2))
                    {
                        return View(estilistaViewModel);
                    }*/
                    if (estilista.FechaNacimiento.Year >= (DateTime.Now.Year - 15))
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "La edad debe ser mayor de 15 años";
                        return View(estilistaViewModel);
                    }
                    await _estilistaService.EditarEstilista(estilista);
                    TempData["Accion"] = "Editar";
                    TempData["Mensaje"] = "Estilista editado correctamente";
                    return RedirectToAction("index");
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
            return View(estilistaViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Eliminar(int? id)
        {            
            try
            {
                if (id == null || id == 0)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Error";
                    return RedirectToAction("index");
                }
                await _estilistaService.EliminarEstilista(id.Value);
                TempData["Accion"] = "Eliminar";
                TempData["Mensaje"] = "Estilista eliminado correctamente";
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
        public async Task<IActionResult> EditarEstado(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Error";
                return RedirectToAction("index");
            }
            Estilista estilista = await _estilistaService.ObtenerEstilistaPorId(id.Value);
            try
            {
                if (estilista.Estado == true)
                    estilista.Estado = false;
                else if (estilista.Estado == false)
                    estilista.Estado = true;

                await _estilistaService.EditarEstilista(estilista);
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
    }
}
