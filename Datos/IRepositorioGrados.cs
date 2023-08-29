using System.Collections;

namespace CrudNet6.Datos
{
    public interface IRepositorioGrados
    {
        public Task<IEnumerable> ListadoGrados();
    }
}
