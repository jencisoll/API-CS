using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Entidades;

namespace CleanArchitecture.Aplication.Productos
{
    /// <summary>
    /// Query para obtener un producto por su ID.
    /// </summary>
    public class DameProductoPorIdQuery
    {
        private readonly IProductoRepositorio _repository;

        public DameProductoPorIdQuery(IProductoRepositorio repository)
        {
            _repository = repository;
        }

        public Task<Producto?> ExecuteAsync(Guid id)
        {
            return _repository.DameProductoPorIdAsync(id);
        }
    }
}
