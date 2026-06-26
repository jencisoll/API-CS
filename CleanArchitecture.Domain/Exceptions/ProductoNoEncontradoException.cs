namespace CleanArchitecture.Domain.Exceptions;

public class ProductoNoEncontradoException : Exception
{
    public ProductoNoEncontradoException(Guid id)
        : base($"El producto con id '{id}' no fue encontrado.")
    {
        Id = id;
    }

    public Guid Id { get; }
}
