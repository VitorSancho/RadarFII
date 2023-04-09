using DevIO.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadarFII.Business.Models;

namespace RadarFII.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome)
                        .IsRequired()
                        .HasColumnType("varchar(200)");

            builder.Property(p => p.Email)
                        .IsRequired()
                        .HasColumnType("varchar(100)");

            builder.Property(p => p.Celular)
                        .IsRequired()
                        .HasColumnType("varchar(100)");

            builder.Property(p => p.CEP)
                    .IsRequired()
                        .HasColumnType("varchar(20)");

            // 1 : N => Usuario : Tarefas
            builder.HasMany(f => f.Tarefas)
                .WithOne(p => p.Usuario);

            builder.ToTable("Usuarios");
        }
    }
}
