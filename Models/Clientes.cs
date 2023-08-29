using System.ComponentModel.DataAnnotations;

namespace CrudNet6.Models
{
    public class Clientes
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Telefono { get; set; }

        //Agregamos clave foranea y propiedad de navegación 
        public int CategoriaClienteId { get; set; }
        public CategoriaCliente categoriaCliente { get; set; }

    }
}
