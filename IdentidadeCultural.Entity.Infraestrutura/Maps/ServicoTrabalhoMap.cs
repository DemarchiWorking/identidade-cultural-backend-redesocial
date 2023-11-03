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
    public class ServicoTrabalhoMap : IEntityTypeConfiguration<ServicoTrabalho>
    {
        public void Configure(EntityTypeBuilder<ServicoTrabalho> builder)
        {
            builder.ToTable("Servicos");
                builder.HasKey(x => x.Id)
                    .HasName("ServicoId");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Titulo)
                .HasColumnType("varchar(100)")
                .IsRequired();


            builder.HasOne(y => y.Usuario)
                .WithMany(z => z.Servicos)
                .HasForeignKey(p => p.UsuarioId);

        }
    }
}
