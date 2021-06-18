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
    public class InsumoController : Controller
    {
        private readonly AppDbContext _context;

        public InsumoController(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            return View(await _context.Insumo.ToListAsync());
        }

        // Crear
        public IActionResult CrearInsumo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearInsumo([Bind("InsumoId, Nombre, Cantidad, Medida, Estado")] Insumo insumo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(insumo);
                    await _context.SaveChangesAsync();
                    TempData["Accion"] = "CrearInsumo";
                    TempData["Mensaje"] = "Insumo creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                TempData["Accion"] = "CrearInsumoF";
                TempData["Mensaje"] = "Fallo al crear el insumo";

            }
            return View(insumo);

         // Editar
        }
        public async Task<IActionResult> EditarInsumo(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumo.FindAsync(Id);

            if (insumo == null)
            {
                return NotFound();
            }
            return View(insumo);
        }

        // Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarInsumo(int Id, [Bind("InsumoId, Nombre, Cantidad, Medida, Estado")] Insumo insumo)
        {

            if (Id != insumo.InsumoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insumo);
                    await _context.SaveChangesAsync();
                    TempData["Accion"] = "EditarInsumo";
                    TempData["Mensaje"] = "Insumo editado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["Accion"] = "EditarInsumo";
                    TempData["Mensaje"] = "Fallo al editar el insumo";
                    if (!InsumoExists(insumo.InsumoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(insumo);
        }

        //Eliminar
        public async Task<IActionResult> EliminarInsumo(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumo
                .FirstOrDefaultAsync(m => m.InsumoId == Id);
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

        // Eliminar
        [HttpPost, ActionName("EliminarInsumo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int Id)
        {
            try
            {
                var insumo = await _context.Insumo.FindAsync(Id);
                _context.Insumo.Remove(insumo);
                await _context.SaveChangesAsync();
                TempData["Accion"] = "EliminarInsumo";
                TempData["Mensaje"] = "Insumo eliminado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Accion"] = "EliminarInsumoF";
                TempData["Mensaje"] = "Fallo al eliminar el insumo";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool InsumoExists(int Id)
        {
            return _context.Insumo.Any(e => e.InsumoId == Id);
        }
    }
}
