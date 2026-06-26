using CleanArchitecture.Application.Productos;
using CleanArchitecture.Domain.Entidades;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Tests.Application.Productos;

public class ProductoCommandsTests
{
    private readonly EnMemoriaProductoRepositorio _repository = new();

    [Fact]
    public async Task AgregarProductoCommand_AsignaIdYGuardaProducto()
    {
        var command = new AgregarProductoCommand(_repository);
        var producto = new Producto { Nombre = "Auriculares", Precio = 80m };

        var resultado = await command.ExecuteAsync(producto);

        Assert.NotEqual(Guid.Empty, resultado.id);
        var guardado = await _repository.DameProductoPorIdAsync(resultado.id);
        Assert.NotNull(guardado);
    }

    [Fact]
    public async Task ActualizarProductoCommand_ProductoExistente_Actualiza()
    {
        var agregar = new AgregarProductoCommand(_repository);
        var original = await agregar.ExecuteAsync(new Producto { Nombre = "Tablet", Precio = 200m });

        var actualizar = new ActualizarProductoCommand(_repository);
        var actualizado = new Producto { id = original.id, Nombre = "Tablet Pro", Precio = 350m };
        await actualizar.ExecuteAsync(original.id, actualizado);

        var guardado = await _repository.DameProductoPorIdAsync(original.id);
        Assert.Equal("Tablet Pro", guardado!.Nombre);
    }

    [Fact]
    public async Task ActualizarProductoCommand_ProductoInexistente_LanzaExcepcion()
    {
        var command = new ActualizarProductoCommand(_repository);
        var producto = new Producto { id = Guid.NewGuid(), Nombre = "X", Precio = 1m };

        await Assert.ThrowsAsync<ProductoNoEncontradoException>(() => command.ExecuteAsync(producto.id, producto));
    }

    [Fact]
    public async Task BorrarProductoCommand_ProductoExistente_Elimina()
    {
        var agregar = new AgregarProductoCommand(_repository);
        var producto = await agregar.ExecuteAsync(new Producto { Nombre = "Cable", Precio = 10m });

        var borrar = new BorrarProductoCommand(_repository);
        await borrar.ExecuteAsync(producto.id);

        Assert.Null(await _repository.DameProductoPorIdAsync(producto.id));
    }

    [Fact]
    public async Task BorrarProductoCommand_ProductoInexistente_LanzaExcepcion()
    {
        var command = new BorrarProductoCommand(_repository);
        await Assert.ThrowsAsync<ProductoNoEncontradoException>(() => command.ExecuteAsync(Guid.NewGuid()));
    }
}
