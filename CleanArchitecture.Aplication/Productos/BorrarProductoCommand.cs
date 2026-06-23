using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Aplication.Productos
{
    /// <summary>
    /// Command para eliminar un producto por su ID.
    /// </summary>
    public class BorrarProductoCommand
    {
        private readonly IProductoRepositorio _repository;

        public BorrarProductoCommand(IProductoRepositorio repository)
        {
            _repository = repository;
        }

        public Task ExecuteAsync(Guid id)
        {
            return _repository.BorrarProductoAsync(id);
        }
    }
}
