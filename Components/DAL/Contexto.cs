using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Components.Models;

namespace RegistroTecnicos.Components.DAL
{
    public class Contexto:DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Tecnicos> Tecnicos { get; set; }
    }
}
