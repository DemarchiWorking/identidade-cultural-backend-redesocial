using IdentidadeCultural.Entity.Dominio.Model;
using IdentidadeCultural.Infra.Servicos.Maps;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Infraestrutura
{
    public class IdentityContext : IdentityDbContext
    {

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }
        public DbSet<ServicoTrabalho> Servicos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Amizade> Amizades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new AmizadeMap());
            builder.ApplyConfiguration(new ServicoTrabalhoMap());
            builder.ApplyConfiguration(new ProdutoMap());
            /*
            builder.Entity<Usuario>(new UsuarioMap().Configure);
            builder.Entity<ServicoTrabalho>(new ServicoTrabalhoMap().Configure);            
            builder.Entity<Produto>(new ProdutoMap().Configure);
            */
 

            base.OnModelCreating(builder);
        }
    }
}
