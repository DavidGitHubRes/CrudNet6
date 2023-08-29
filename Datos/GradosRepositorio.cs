using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CrudNet6.Datos
{
    public class GradosRepositorio : IRepositorioGrados
    {
        private readonly ApplicationDBContext ContextoDatos;
        public GradosRepositorio(ApplicationDBContext xContextoDatos)
        {
            ContextoDatos = xContextoDatos;
        }

        public async Task<IEnumerable> ListadoGrados()
        {
            return await ContextoDatos.CategoriasCliente.ToListAsync(); 
        }
    }
}
