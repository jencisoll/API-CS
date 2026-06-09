using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Aplication.Productos
{
    public class AgregarProductoQuery
    {

        private readonly IProductoRepositorio _repository;
        public AgregarProductoQuery(IProductoRepositorio repository)
        {
            _repository = repository;
        }
        Task AgregarProductoAsync(Producto producto)
        {
            return _repository.AgregarProductoAsync(producto);
        }

    }
}
