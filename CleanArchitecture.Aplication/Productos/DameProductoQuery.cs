using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Aplication.Productos
{
    public class DameProductoQuery

    {
        private readonly IProductoRepositorio _repository;
        public DameProductoQuery(IProductoRepositorio repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<Producto>> ExecuteAsync()
        {
            return _repository.DameProductosAsync();
        }

    }
}
