namespace ToDo.WebAPI.ViewModels
{
    public class LogPontuacaoViewModel : EntityViewModel
    {
        public Guid TarefaId { get; set; }
        public TimeOnly? Hr_execucao { get; set; }

        //public TarefaViewModel Tarefa { get; set; }  
    }

}