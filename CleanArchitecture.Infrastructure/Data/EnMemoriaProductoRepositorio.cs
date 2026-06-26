using System.Collections.Concurrent;
using CleanArchitecture.Domain.Entidades;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Infrastructure.Data;

public class EnMemoriaProductoRepositorio : IProductoRepositorio
{
    private readonly ConcurrentDictionary<Guid, Producto> _productos = new();

    public Task<IEnumerable<Producto>> DameProductosAsync()
    {
        return Task.FromResult(_productos.Values.AsEnumerable());
    }

    public Task<Producto?> DameProductoPorIdAsync(Guid id)
    {
        _productos.TryGetValue(id, out var producto);
        return Task.FromResult(producto);
    }

    public Task AgregarProductoAsync(Producto producto)
    {
        ArgumentNullException.ThrowIfNull(producto);

        producto.id = producto.id == Guid.Empty ? Guid.NewGuid() : producto.id;
        _productos.TryAdd(producto.id, producto);
        return Task.CompletedTask;
    }

    public Task ActualizarProductoAsync(Producto producto)
    {
        ArgumentNullException.ThrowIfNull(producto);

        if (!_productos.ContainsKey(producto.id))
        {
            throw new ProductoNoEncontradoException(producto.id);
        }

        _productos[producto.id] = producto;
        return Task.CompletedTask;
    }

    public Task BorrarProductoAsync(Guid id)
    {
        if (!_productos.TryRemove(id, out _))
        {
            throw new ProductoNoEncontradoException(id);
        }

        return Task.CompletedTask;
    }
}
