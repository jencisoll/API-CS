using CleanArchitecture.Domain.Entidades;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Tests.Infrastructure.Data;

public class EnMemoriaProductoRepositorioTests
{
    private readonly EnMemoriaProductoRepositorio _sut = new();

    [Fact]
    public async Task DameProductosAsync_Inicialmente_DevuelveColeccionVacia()
    {
        var productos = await _sut.DameProductosAsync();
        Assert.Empty(productos);
    }

    [Fact]
    public async Task AgregarProductoAsync_AgregaProductoYSeRecupera()
    {
        var producto = new Producto { Nombre = "Teclado", Precio = 50m };

        await _sut.AgregarProductoAsync(producto);
        var recuperado = await _sut.DameProductoPorIdAsync(producto.id);

        Assert.NotNull(recuperado);
        Assert.Equal(producto.Nombre, recuperado.Nombre);
        Assert.Equal(producto.Precio, recuperado.Precio);
    }

    [Fact]
    public async Task DameProductoPorIdAsync_ProductoInexistente_DevuelveNull()
    {
        var producto = await _sut.DameProductoPorIdAsync(Guid.NewGuid());
        Assert.Null(producto);
    }

    [Fact]
    public async Task ActualizarProductoAsync_ProductoExistente_ActualizaValores()
    {
        var producto = new Producto { Nombre = "Mouse", Precio = 25m };
        await _sut.AgregarProductoAsync(producto);

        var actualizado = new Producto { id = producto.id, Nombre = "Mouse Gamer", Precio = 45m };
        await _sut.ActualizarProductoAsync(actualizado);

        var recuperado = await _sut.DameProductoPorIdAsync(producto.id);
        Assert.NotNull(recuperado);
        Assert.Equal("Mouse Gamer", recuperado.Nombre);
        Assert.Equal(45m, recuperado.Precio);
    }

    [Fact]
    public async Task ActualizarProductoAsync_ProductoInexistente_LanzaExcepcion()
    {
        var producto = new Producto { id = Guid.NewGuid(), Nombre = "X", Precio = 1m };
        await Assert.ThrowsAsync<ProductoNoEncontradoException>(() => _sut.ActualizarProductoAsync(producto));
    }

    [Fact]
    public async Task BorrarProductoAsync_ProductoExistente_EliminaProducto()
    {
        var producto = new Producto { Nombre = "Monitor", Precio = 300m };
        await _sut.AgregarProductoAsync(producto);

        await _sut.BorrarProductoAsync(producto.id);

        var recuperado = await _sut.DameProductoPorIdAsync(producto.id);
        Assert.Null(recuperado);
    }

    [Fact]
    public async Task BorrarProductoAsync_ProductoInexistente_LanzaExcepcion()
    {
        await Assert.ThrowsAsync<ProductoNoEncontradoException>(() => _sut.BorrarProductoAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task AgregarProductoAsync_ProductoNull_LanzaArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.AgregarProductoAsync(null!));
    }
}
