using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Aplication.Productos
{
    /// <summary>
    /// Esta clase representa una 'Query' (consulta) para obtener productos.
    /// En el patrón CQRS, las Queries son responsables de la lectura de datos
    /// y no modifican el estado de la aplicación. Su objetivo es proporcionar datos
    /// al cliente de la manera más eficiente posible.
    /// </summary>
    public class DameProductoQuery

    {
        /// <summary>
        /// Campo de sólo lectura para la interfaz del repositorio de productos.
        /// Se inyecta la dependencia del repositorio para obtener los datos.
        /// </summary>
        private readonly IProductoRepositorio _repository;

        /// <summary>
        /// Constructor que recibe una instancia de IProductoRepositorio.
        /// </summary>
        /// <param name="repository">La implementación del repositorio de productos.</param>
        public DameProductoQuery(IProductoRepositorio repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Ejecuta la consulta para obtener todos los productos de forma asíncrona.
        /// Delega la llamada al método correspondiente del repositorio.
        /// </summary>
        /// <returns>Una tarea que devuelve una colección de productos.</returns>
        public Task<IEnumerable<Producto>> ExecuteAsync()
        {
            // La lógica de la query es simple: solo reenvía la llamada al repositorio.
            // En casos más complejos, aquí se podría realizar mapeo de DTOs, filtrado,
            // paginación, etc., antes de devolver los datos al cliente.
            return _repository.DameProductosAsync();
        }

    }
}
