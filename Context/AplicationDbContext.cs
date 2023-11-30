using Contactos_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace Contactos_BE.Context
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }
        public DbSet<Contacto> Contactos { get; set; }

    }
}
