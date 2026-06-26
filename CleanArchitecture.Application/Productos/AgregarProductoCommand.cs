using CleanArchitecture.Domain.Entidades;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Application.Productos;

public class AgregarProductoCommand
{
    private readonly IProductoRepositorio _repository;

    public AgregarProductoCommand(IProductoRepositorio repository)
    {
        _repository = repository;
    }

    public async Task<Producto> ExecuteAsync(Producto producto)
    {
        producto.id = Guid.NewGuid();
        await _repository.AgregarProductoAsync(producto);
        return producto;
    }
}
