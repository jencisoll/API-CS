using CleanArchitecture.Application.Productos;
using CleanArchitecture.Domain.Entidades;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Presentation.Middleware;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchitecture API", Version = "v1" });
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<IProductoRepositorio, EnMemoriaProductoRepositorio>();

builder.Services.AddTransient<DameProductoQuery>();
builder.Services.AddTransient<DameProductoPorIdQuery>();
builder.Services.AddTransient<AgregarProductoCommand>();
builder.Services.AddTransient<ActualizarProductoCommand>();
builder.Services.AddTransient<BorrarProductoCommand>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapGet("/api/productos", async (DameProductoQuery query) =>
{
    var productos = await query.ExecuteAsync();
    return Results.Ok(productos.Select(MapToResponse));
})
.WithName("ObtenerProductos")
.WithTags("Productos")
.Produces<IEnumerable<ProductoResponse>>(StatusCodes.Status200OK);

app.MapGet("/api/productos/{id:guid}", async (Guid id, DameProductoPorIdQuery query) =>
{
    var producto = await query.ExecuteAsync(id);
    return producto is not null ? Results.Ok(MapToResponse(producto)) : Results.NotFound();
})
.WithName("ObtenerProductoPorId")
.WithTags("Productos")
.Produces<ProductoResponse>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/api/productos", async (ProductoRequest request, AgregarProductoCommand command) =>
{
    var (isValid, errors) = ProductoRequestValidator.Validate(request);
    if (!isValid)
    {
        return Results.BadRequest(new { Errors = errors });
    }

    var producto = new Producto
    {
        Nombre = request.Nombre,
        Precio = request.Precio
    };

    var creado = await command.ExecuteAsync(producto);
    return Results.Created($"/api/productos/{creado.id}", MapToResponse(creado));
})
.WithName("CrearProducto")
.WithTags("Productos")
.Produces<ProductoResponse>(StatusCodes.Status201Created)
.ProducesValidationProblem();

app.MapPut("/api/productos/{id:guid}", async (Guid id, ProductoRequest request, ActualizarProductoCommand command) =>
{
    var (isValid, errors) = ProductoRequestValidator.Validate(request);
    if (!isValid)
    {
        return Results.BadRequest(new { Errors = errors });
    }

    var producto = new Producto
    {
        id = id,
        Nombre = request.Nombre,
        Precio = request.Precio
    };

    await command.ExecuteAsync(id, producto);
    return Results.NoContent();
})
.WithName("ActualizarProducto")
.WithTags("Productos")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.ProducesValidationProblem();

app.MapDelete("/api/productos/{id:guid}", async (Guid id, BorrarProductoCommand command) =>
{
    await command.ExecuteAsync(id);
    return Results.NoContent();
})
.WithName("EliminarProducto")
.WithTags("Productos")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);

app.Run();

static ProductoResponse MapToResponse(Producto producto) =>
    new(producto.id, producto.Nombre, producto.Precio);

public partial class Program { }
