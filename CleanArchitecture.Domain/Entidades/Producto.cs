using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entidades
{
    /// <summary>
    /// Representa la entidad de dominio 'Producto'.
    /// En una arquitectura limpia, las entidades de dominio son el corazón de la aplicación
    /// y contienen la lógica de negocio más importante y las reglas empresariales.
    /// No deben tener dependencias de otras capas (Aplicación, Infraestructura, Presentación).
    /// </summary>
    public class Producto
    {
        /// <summary>
        /// Obtiene o establece el identificador único del producto. 
        /// Se utiliza Guid (Globally Unique Identifier) para asegurar que cada producto tenga un ID único a nivel global.
        /// </summary>
        public Guid id { get;set; }

        /// <summary>
        /// Obtiene o establece el nombre del producto.
        /// Es un string, lo que permite flexibilidad en los nombres de los productos.
        /// </summary>
        public string Nombre{ get; set; }

        /// <summary>
        /// Obtiene o establece el precio del producto.
        /// Se utiliza decimal para asegurar una alta precisión en los cálculos monetarios, evitando problemas de coma flotante.
        /// </summary>
        public decimal Precio { get; set; }
    }
}
