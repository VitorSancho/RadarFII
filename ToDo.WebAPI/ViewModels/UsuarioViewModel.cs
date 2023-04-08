using System.ComponentModel.DataAnnotations;

namespace ToDo.WebAPI.ViewModels
{
    public class UsuarioViewModel : EntityViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Hr_cadastroTarefas { get; set; }
        public string CEP { get; set; }

        //public IEnumerable<TarefaViewModel> Tarefas { get; set; }

    }
}