using CleanArchitecture.Application.Productos;

namespace CleanArchitecture.Tests.Application.Productos;

public class ProductoRequestValidatorTests
{
    [Theory]
    [InlineData("Laptop", 1000, true)]
    [InlineData("Laptop", 0, true)]
    [InlineData("", 1000, false)]
    [InlineData("   ", 1000, false)]
    [InlineData("Laptop", -1, false)]
    public void Validate_RetornaResultadoEsperado(string nombre, decimal precio, bool esperadoValido)
    {
        var request = new ProductoRequest(nombre, precio);
        var (isValid, _) = ProductoRequestValidator.Validate(request);
        Assert.Equal(esperadoValido, isValid);
    }

    [Fact]
    public void Validate_NombreMuyLargo_RetornaError()
    {
        var request = new ProductoRequest(new string('A', 101), 100);
        var (isValid, errors) = ProductoRequestValidator.Validate(request);

        Assert.False(isValid);
        Assert.Contains(errors, e => e.Contains("100 caracteres"));
    }
}
