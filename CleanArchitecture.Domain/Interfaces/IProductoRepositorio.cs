using CleanArchitecture.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{

    public interface IProductoRepositorio

    {
        Task<IEnumerable<Producto>> DameProductosAsync();
        Task<Producto?> DameProductoPorIdAsync(Guid id);
        Task AgregarProductoAsync(Producto producto);
        Task ActualizarProductoAsync(Producto producto);

        Task BorrarProductoAsync(Guid id);
    }
}
