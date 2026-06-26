namespace CleanArchitecture.Application.Productos;

public static class ProductoRequestValidator
{
    public static (bool IsValid, List<string> Errors) Validate(ProductoRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.Nombre))
        {
            errors.Add("El nombre es obligatorio.");
        }
        else if (request.Nombre.Length > 100)
        {
            errors.Add("El nombre no puede exceder los 100 caracteres.");
        }

        if (request.Precio < 0)
        {
            errors.Add("El precio no puede ser negativo.");
        }

        return (errors.Count == 0, errors);
    }
}
