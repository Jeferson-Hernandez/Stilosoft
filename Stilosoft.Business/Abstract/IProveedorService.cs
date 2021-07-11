﻿using Stilosoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft.Business.Abstract
{
    interface IProveedorService
    {        
        Task<IEnumerable<Proveedor>> ObtenerListaProveedor();
        Task<Proveedor> ObtenerProveedorPorId(int id);
        Task GuardarProveedor(Proveedor proveedor);
        Task EditarProveedor(Proveedor proveedor);
        Task EliminarProveedor(int id);
        Task<Proveedor> NombreProveedorExiste(string nombre);
    }
}
