﻿using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Business.Abstract
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> ObtenerListaProductos();
        Task RegistrarProducto(Producto producto);
        Task<Producto> ObtenerProductoPorId(int id);
        Task EditarProducto(Producto producto);
        Task EliminarProducto(int id);
    }
}
