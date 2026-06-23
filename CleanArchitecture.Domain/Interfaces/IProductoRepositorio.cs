using CleanArchitecture.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    /// <summary>
    /// Define el contrato para el repositorio de productos. 
    /// En una Arquitectura Limpia, estas interfaces residen en la capa de Dominio
    /// para asegurar que la lógica de negocio no dependa de los detalles de implementación
    /// de la base de datos o de cualquier otra infraestructura.
    /// </summary>
    public interface IProductoRepositorio
    {
        /// <summary>
        /// Obtiene de forma asíncrona todos los productos.
        /// Utiliza Task<IEnumerable<Producto>> para devolver una colección de productos de forma asíncrona,
        /// lo que mejora la escalabilidad al no bloquear el hilo de ejecución.
        /// </summary>
        Task<IEnumerable<Producto>> DameProductosAsync();

        /// <summary>
        /// Obtiene de forma asíncrona un producto por su identificador.
        /// Task<Producto?> indica que la operación es asíncrona y puede devolver null si el producto no se encuentra,
        /// utilizando el operador ? para indicar que es un tipo de referencia que permite valores nulos (nullable).
        /// </summary>
        /// <param name="id">El Guid del producto a buscar.</param>
        Task<Producto?> DameProductoPorIdAsync(Guid id);

        /// <summary>
        /// Agrega un nuevo producto de forma asíncrona.
        /// </summary>
        /// <param name="producto">El objeto Producto a agregar.</param>
        Task AgregarProductoAsync(Producto producto);

        /// <summary>
        /// Actualiza un producto existente de forma asíncrona.
        /// </summary>
        /// <param name="producto">El objeto Producto con los datos actualizados.</param>
        Task ActualizarProductoAsync(Producto producto);

        /// <summary>
        /// Elimina un producto por su identificador de forma asíncrona.
        /// </summary>
        /// <param name="id">El Guid del producto a eliminar.</param>
        Task BorrarProductoAsync(Guid id);
    }
}
