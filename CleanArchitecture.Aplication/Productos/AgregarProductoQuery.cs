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
    /// Esta clase, a pesar de su nombre 'Query', actúa como un manejador de comandos
    /// para agregar un nuevo producto. En una arquitectura CQRS pura, las operaciones de escritura
    /// se manejan con 'Commands' y las de lectura con 'Queries'. Aquí, parece que se usa 'Query'
    /// para ambos, lo cual es una ligera desviación pero funcional.
    /// Su responsabilidad es orquestar la operación de agregar un producto utilizando el repositorio de dominio.
    /// </summary>
    public class AgregarProductoQuery
    {

        /// <summary>
        /// Campo de sólo lectura para la interfaz del repositorio de productos.
        /// Esto permite la inyección de dependencias, lo que hace que la clase sea testable y desacoplada
        /// de la implementación concreta del repositorio (que residirá en la capa de Infraestructura).
        /// </summary>
        private readonly IProductoRepositorio _repository;

        /// <summary>
        /// Constructor que recibe una instancia de IProductoRepositorio.
        /// Esta es la forma en que se inyecta la dependencia del repositorio.
        /// </summary>
        /// <param name="repository">La implementación del repositorio de productos.</param>
        public AgregarProductoQuery(IProductoRepositorio repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Método asíncrono para agregar un producto.
        /// Delega la operación directamente al repositorio de productos. No debería contener lógica de negocio
        /// compleja; su rol es invocar la operación correcta en el dominio.
        /// </summary>
        /// <param name="producto">El objeto Producto a agregar.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        public Task AgregarProductoAsync(Producto producto)
        {
            // Aquí simplemente se delega la llamada al repositorio. 
            // La lógica de negocio real (validaciones, etc.) debería estar en la entidad Producto o en servicios de dominio si fuera más compleja.
            return _repository.AgregarProductoAsync(producto);
        }

    }
}
