using CleanArchitecture.Domain.Entidades;
using CleanArchitecture.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.Data
{
    public class EnMemoriaProductoRepositorio : IProductoRepositorio
    {
        private readonly List<Producto> _productos = new();
        public Task ActualizarProductoAsync(Producto producto)
        {
            var index = _productos.FindIndex(p => p.id == producto.id);
            if (index >= 0)
            
                _productos[index] = producto;
                return Task.CompletedTask;
            
        }
        public Task AgregarProductoAsync(Producto producto)
        {
            _productos.Add(producto);
            return Task.CompletedTask;
        }
        public Task BorrarProductoAsync(Guid id)
        {
            var producto = _productos.FirstOrDefault(p => p.id == id);
            if (producto != null)
            
                _productos.Remove(producto);
                return Task.CompletedTask;

        }
        public Task<Producto?> DameProductoPorIdAsync(Guid id)
        {
            
            return Task.FromResult(_productos.FirstOrDefault(p => p.id == id));
        }
        public Task<IEnumerable<Producto>> DameProductosAsync()
        {
            return Task.FromResult(_productos.AsEnumerable());
        }

    }
}
