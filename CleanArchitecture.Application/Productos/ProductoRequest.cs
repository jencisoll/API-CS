namespace CleanArchitecture.Application.Productos;

public sealed record ProductoRequest(
    string Nombre,
    decimal Precio);
