using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.DAL;
using Stilosoft.Model.Entities;
using Stilosoft.ViewModels.Insumo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    [Authorize]
    public class InsumoController:Controller
    { 
    private readonly IInsumoService _insumoService;



    public  InsumoController (IInsumoService insumoService)
    {
        _insumoService = insumoService;

    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _insumoService.ObtenerListaInsumos());
    }

    // Crear
    public IActionResult CrearInsumo()
    {
        return View();
    }

    // Crear
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearInsumo(InsumoViewModel insumoViewModel)
    {
          
            if (ModelState.IsValid)
            {
                    Insumo insumo = new()
                    {
                        Nombre = insumoViewModel.Nombre,
                        Cantidad = insumoViewModel.Cantidad,
                        Medida = insumoViewModel.Medida,
                        Estado = true
                    };
                    
                try
                {
                    var insumoExiste = await _insumoService.NombreInsumoExiste(insumo.Nombre);

                    if (insumoExiste != null)
                    {
                        
                            TempData["Accion"] = "Error";
                            TempData["Mensaje"] = "El insumo ya existe";
                        return RedirectToAction("Index");
                    }

                    if (insumoViewModel.Cantidad <= 0)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "La cantidad no puede ser menor o igual a 0";
                        return RedirectToAction("Index");
                    }
                    await _insumoService.RegistrarInsumo(insumo);
                    TempData["Accion"] = "Crear";
                    TempData["Mensaje"] = "Insumo creado exitosamente";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw;
                }


            }
      
            return RedirectToAction(nameof(Index));
        
        

    }

        // Editar
        [HttpGet]
        public async Task<IActionResult> EditarInsumo(int Id)
    {

            Insumo insumo = await _insumoService.ObtenerInsumoPorId(Id);
            InsumoViewModel insumoViewModel = new()
            {
                InsumoId = insumo.InsumoId,
                Nombre = insumo.Nombre,
                Cantidad = insumo.Cantidad,
                Medida = insumo.Medida,
                Estado = insumo.Estado
            };
        return View(insumoViewModel);
    }

    // Editar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarInsumo(InsumoViewModel insumoViewModel)
    {
            if (ModelState.IsValid)
            {
                Insumo insumo = new()
                {
                    InsumoId = insumoViewModel.InsumoId,
                    Nombre = insumoViewModel.Nombre,
                    Cantidad = insumoViewModel.Cantidad,
                    Medida = insumoViewModel.Medida,
                    Estado = insumoViewModel.Estado
                };

                try
                {
                    if (insumoViewModel.Cantidad <= 0)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "La cantidad no puede ser menor o igual a 0";
                        return RedirectToAction("Index");
                    }
                    /*var insumoExiste = await _insumoService.NombreInsumoExiste(insumo.Nombre);

                    if (insumoExiste != null)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "Nombre del insumo ya existe";
                        return RedirectToAction("Index");
                    }*/
                    await _insumoService.EditarInsumo(insumo);
                    TempData["Accion"] = "Editar";
                    TempData["Mensaje"] = "Modificacion exitosa";
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return NotFound();
            }
           
        }

    // Eliminar
    [HttpPost]
    public async Task<IActionResult> EliminarInsumo(int? Id)
    {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Id != null)
                    {
                        await _insumoService.EliminarInsumo(Id.Value);
                        TempData["Accion"] = "Eliminar";
                        TempData["Mensaje"] = "Insumo eliminado correctamente";
                        return RedirectToAction("index");
                    }
               
                    return NotFound();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return NotFound();
            }


        }
    
    public async Task<IActionResult> EditarEstado(int Id)
        {
            Insumo insumo = await _insumoService.ObtenerInsumoPorId(Id);
            if (insumo.Estado == true)
               insumo.Estado = false;
            else if (insumo.Estado == false)
                insumo.Estado = true;

            try
            {
                await _insumoService.EditarInsumo(insumo);
                TempData["Accion"] = "EditarEstado";
                TempData["Mensaje"] = "Estado editado correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ingresaste un valor inválido";
                return RedirectToAction("Nndex");
            }
        }
    }
}

