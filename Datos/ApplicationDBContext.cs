using CrudNet6.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrudNet6.Datos
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options) : base(options)
        {
        }
        public DbSet<Clientes> clientes { get; set; }
        public DbSet<CategoriaCliente> CategoriasCliente { get; set; }

    }
}
