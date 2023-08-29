using CrudNet6.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CrudNet6.Datos
{
    public class ClientesRepositorio : IRepositorioClientes
    {
        private readonly ApplicationDBContext ContextoDatos;

        private Clientes clientes;

        public ClientesRepositorio(ApplicationDBContext xContextoDatos)
        {
            ContextoDatos = xContextoDatos;
        }
        public async Task Actualizar(Clientes clientes)
        {
            ContextoDatos.clientes.Update(clientes);
            await ContextoDatos.SaveChangesAsync();
        }

        public async Task<Clientes> Buscar(int? id)
        {
            return clientes = await ContextoDatos.clientes.FindAsync(id);
        }

        public async Task Crear(Clientes clientes)
        {
            ContextoDatos.clientes.Add(clientes);
            await ContextoDatos.SaveChangesAsync();
        }

        public async Task Borrar(Clientes clientes)
        {
            ContextoDatos.clientes.Remove(clientes);
            await ContextoDatos.SaveChangesAsync();
        }

        public async Task<IEnumerable> Index()
        {
            return await ContextoDatos.clientes.ToListAsync();
        }
    }
}
