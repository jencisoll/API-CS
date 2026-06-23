using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Entidades;

namespace CleanArchitecture.Aplication.Productos
{
    /// <summary>
    /// Command para actualizar un producto existente.
    /// </summary>
    public class ActualizarProductoCommand
    {
        private readonly IProductoRepositorio _repository;

        public ActualizarProductoCommand(IProductoRepositorio repository)
        {
            _repository = repository;
        }

        public Task ExecuteAsync(Guid id, Producto producto)
        {
            producto.id = id;
            return _repository.ActualizarProductoAsync(producto);
        }
    }
}
