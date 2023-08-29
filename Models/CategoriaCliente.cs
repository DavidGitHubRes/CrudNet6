namespace CrudNet6.Models
{
    public class CategoriaCliente
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public List<Clientes> Clientes { get; set; }

    }
}
