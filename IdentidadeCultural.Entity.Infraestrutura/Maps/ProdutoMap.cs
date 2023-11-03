using IdentidadeCultural.Entity.Dominio.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeCultural.Infra.Servicos.Maps
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
                builder.HasKey(x => x.Id)
                    .HasName("ProdutoId");
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(d => d.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasOne(y => y.Usuario)
                .WithMany(z => z.Produtos)
                .HasForeignKey(p => p.UsuarioId);
                


        }
    }
}
