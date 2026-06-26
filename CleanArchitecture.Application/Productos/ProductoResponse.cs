namespace CleanArchitecture.Application.Productos;

public sealed record ProductoResponse(
    Guid Id,
    string Nombre,
    decimal Precio);
