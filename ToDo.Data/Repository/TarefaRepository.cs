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
    public class TarefaRepository : Repository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(MeuDbContext context) : base(context) { }

        public async Task<IEnumerable<Tarefa>> ObterListaTarefasPorDificuldade(Guid dificuldadeId)
        {
            var tarefas = await Db.Tarefas.AsNoTracking()
                            .Include(x => x.Dificuldade)
                            .Where(x => x.Dificuldade.Id == dificuldadeId).ToListAsync();

            return tarefas;
        }

        public async Task<IEnumerable<Tarefa>> ObterListaTarefasPorUsuario(Guid usuarioId)
        {
            var tarefas = await Db.Tarefas.AsNoTracking()
                .Include(x => x.Usuario)
                .Where(x => x.Usuario.Id == usuarioId).ToListAsync();

            return tarefas;
        }

        public async Task<Tarefa> ObterTarefaPorUsuario(Guid usuarioId)
        {
            var tarefa = await Db.Tarefas.AsNoTracking()
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Usuario.Id == usuarioId);

            return tarefa;
        }
    }    
}