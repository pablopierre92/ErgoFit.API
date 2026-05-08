using Ergo.Fit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UsuarioMasterModel> UsuariosMaster { get; set; }
        public DbSet<EmpresaModel> Empresas { get; set; }
        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        public DbSet<DepartamentoModel> Departamentos { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<ExercicioModel> Exercicios { get; set; }
        public DbSet<SessaoModel> Sessoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
