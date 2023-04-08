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
    public class DificuldadeMapping : IEntityTypeConfiguration<Dificuldade>
    {
        public void Configure(EntityTypeBuilder<Dificuldade> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome)
                        .IsRequired()
                        .HasColumnType("varchar(200)");

            builder.Property(p => p.Pontos)
                        .IsRequired()
                        .HasColumnType("integer");

            // 1 : N => Fornecedor : Produtos
            builder.HasMany(f => f.Tarefa)
                .WithOne(p => p.Dificuldade);

            builder.ToTable("Dificuldades");
        }
    }
}
