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
    public class LogPontuacaoMapping : IEntityTypeConfiguration<LogPontuacao>
    {
        public void Configure(EntityTypeBuilder<LogPontuacao> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Hr_execucao)
                        .IsRequired()
                        .HasColumnType("time");

            // 1 : N => Usuario : Tarefas
            builder.HasOne(f => f.Tarefa)
                .WithOne(p => p.LogPontuacao);

            builder.ToTable("LogsPontucao");
        }
    }
}
