using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.DAL;
using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Controllers
{
    public class InsumoController:Controller
    { 
    private readonly IInsumoService _insumoService;
        private Boolean validacion = true;



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
    public IActionResult CrearInsu()
    {
        return View();
    }

    // Crear
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearInsu(Insumo insumo)
    {
        try
        {
            if (ModelState.IsValid)
            {
                    insumo.Estado = validacion;
                    await _insumoService.RegistrarInsumo(insumo);
                    
               

            }

            TempData["Accion"] = "CrearInsu";
            TempData["Mensaje"] = "Insumo creado exitosamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            TempData["Accion"] = "CrearInsuF";
            TempData["Mensaje"] = "Fallo al crear el insumo";
            return RedirectToAction(nameof(Index));
        }

    }

    // Editar
    public async Task<IActionResult> EditarInsu(int Id)
    {

        Insumo insumo = await _insumoService.ObtenerInsumoPorId(Id);
        return View(insumo);
    }

    // Editar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarInsu(int Id, Insumo insumo)
    {
        if (Id != insumo.InsumoId)
        {
            return NotFound();
        }
            
            if (ModelState.IsValid)
        {
                try
                {
                    await _insumoService.EditarInsumo(insumo);
                    TempData["Accion"] = "EditarInsu";
                    TempData["Mensaje"] = "Modificacion exitosa";
                    return RedirectToAction(nameof(Index));
                   
            }
            catch (Exception)
            {
                TempData["Accion"] = "EditarInsuF";
                TempData["Mensaje"] = "Modificacion fallida";
                return RedirectToAction(nameof(Index));
            }
        }
        return View(insumo);
    }

    // Eliminar
    [HttpPost]
    public async Task<IActionResult> EliminarInsu(int Id)
    {
        if (ModelState.IsValid)
        {
            try
            {
                Insumo insumo = await _insumoService.ObtenerInsumoPorId(Id);

                await _insumoService.EliminarInsumo(Id);
                TempData["Accion"] = "EliminarInsu";
                TempData["Mensaje"] = "Insumo eliminado correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ocurrió un error";
                return RedirectToAction("Index");
            }
        }
        else
        {
            TempData["Accion"] = "Error";
            TempData["Mensaje"] = "Ocurrió un error";
            return RedirectToAction("Index");
        }

    }

}
}

