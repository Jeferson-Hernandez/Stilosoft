using Microsoft.EntityFrameworkCore;
using Stilosoft.Business.Abstract;
using Stilosoft.Model.DAL;
using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Business.Business
{
    public class ProductoService:IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Producto>> ObtenerListaProductos()
        {
            return await _context.Producto.ToListAsync();
        }
        public async Task<Producto> ObtenerProductoPorId(int Id)
        {
            return await _context.Producto.FirstOrDefaultAsync(s => s.ProductoId == Id);
        }
        public async Task RegistrarProducto(Producto producto)
        {
            _context.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task EditarProducto(Producto producto)
        {
            _context.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarProducto(int id)
        {
            var producto = await ObtenerProductoPorId(id);
            _context.Remove(producto);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarCantidad(int productoId, int cantidad)
        {
            var producto = await ObtenerProductoPorId(productoId);
            producto.Cantidad += cantidad;
            _context.Update(producto);
            await _context.SaveChangesAsync();
        }
        public async Task<Producto> NombreInsumoExiste(string Nombre)
        {
            return await _context.Producto.FirstOrDefaultAsync(n => n.Nombre == Nombre);
        }
    }
}
