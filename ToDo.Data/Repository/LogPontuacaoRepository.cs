using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RadarFII.Business.Intefaces;
using RadarFII.Business.Models;
using RadarFII.Data.Context;

namespace RadarFII.Data.Repository
{
    public class LogPontuacaoRepository : Repository<LogPontuacao>, ILogPontuacaoRepository
    {
        public LogPontuacaoRepository(MeuDbContext context) : base(context) { }

        public async Task<IEnumerable<LogPontuacao>> ObterListaLogPontuacaoPorUsuario(Guid usuarioId)
        {
            var logPontuacao = await Db.LogsPontuacao.AsNoTracking()
                .Include(x => x.Tarefa.Usuario)
                .Where(x => x.Tarefa.UsuarioId == usuarioId).ToListAsync();

            return logPontuacao;
        }

        public async Task<LogPontuacao> ObterLogPontuacaoPorTarefa(Guid tarefaId)
        {
            var logPontuacao = await Db.LogsPontuacao.AsNoTracking()
                .Include(x => x.Tarefa)
                .FirstOrDefaultAsync(x => x.Tarefa.Id == tarefaId);

            return logPontuacao;
        }
    }
}