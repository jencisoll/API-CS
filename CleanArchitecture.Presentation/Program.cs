using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Entidades;
using CleanArchitecture.Infraestructure.Data;
using CleanArchitecture.Aplication.Productos;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchitecture API", Version = "v1" });
});

// MODIFICADO: Registro del repositorio en memoria como singleton para mantener los datos durante la sesión
builder.Services.AddSingleton<IProductoRepositorio, EnMemoriaProductoRepositorio>();

// MODIFICADO: Registro de los casos de uso (queries/commands) como transient
builder.Services.AddTransient<DameProductoQuery>();
builder.Services.AddTransient<DameProductoPorIdQuery>();
builder.Services.AddTransient<AgregarProductoQuery>();
builder.Services.AddTransient<ActualizarProductoCommand>();
builder.Services.AddTransient<BorrarProductoCommand>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// MODIFICADO: Endpoints de la API de productos
app.MapGet("/api/productos", async (DameProductoQuery query) =>
{
    var productos = await query.ExecuteAsync();
    return Results.Ok(productos);
})
.WithTags("Productos");

app.MapGet("/api/productos/{id:guid}", async (Guid id, DameProductoPorIdQuery query) =>
{
    var producto = await query.ExecuteAsync(id);
    return producto is not null ? Results.Ok(producto) : Results.NotFound();
})
.WithTags("Productos");

app.MapPost("/api/productos", async (Producto producto, AgregarProductoQuery command) =>
{
    producto.id = Guid.NewGuid();
    await command.AgregarProductoAsync(producto);
    return Results.Created($"/api/productos/{producto.id}", producto);
})
.WithTags("Productos");

app.MapPut("/api/productos/{id:guid}", async (Guid id, Producto producto, ActualizarProductoCommand command) =>
{
    await command.ExecuteAsync(id, producto);
    return Results.NoContent();
})
.WithTags("Productos");

app.MapDelete("/api/productos/{id:guid}", async (Guid id, BorrarProductoCommand command) =>
{
    await command.ExecuteAsync(id);
    return Results.NoContent();
})
.WithTags("Productos");

app.Run();
