using backend.Entities;
using Microsoft.EntityFrameworkCore;


namespace backend.Context
{
    public class FinalDBContext : DbContext
    {

        public FinalDBContext(DbContextOptions<FinalDBContext> options)
     : base(options)
        {
        }
        public DbSet<Cadastro> Cadastros { get; set; }
        public DbSet<RegistroDePartida> RegistroDePartidas { get; set; }

    }
}