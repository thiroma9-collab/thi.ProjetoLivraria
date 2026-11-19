using Microsoft.EntityFrameworkCore;
using Thi.ProjetoLivraria.Models;

namespace Thi.ProjetoLivraria.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Logins => Set<Login>();

        public DbSet<Livro> Livros => Set<Livro>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuracao da tabela de usuarios
            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("login");

                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.Email)
                      .IsUnique();
            });

            // Configuracao da tabela de livros
            modelBuilder.Entity<Livro>(entity =>
            {
                entity.ToTable("livro");

                entity.HasKey(l => l.Id);

                entity.HasIndex(l => l.Isbn)
                      .IsUnique();
            });
        }
    }
}