using CrudNet6.Models;
using System.Collections;

namespace CrudNet6.Datos
{
    public interface IRepositorioClientes
    {
        public Task<IEnumerable> Index();
        public Task Crear(Clientes clientes);
        public Task Actualizar(Clientes clientes);
        public Task<Clientes> Buscar(int? id);
        public Task Borrar(Clientes clientes);




    }
}
