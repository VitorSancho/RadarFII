using System.Text.Json.Serialization;

namespace ToDo.WebAPI.ViewModels
{
    public class TarefaViewModel : EntityViewModel
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public Guid DificuldadeId { get; set; }
        public TimeOnly Hr_planejado { get; set; }
        [JsonIgnore]
        public bool FoiExecutada { get; set; } = false;
        //public UsuarioViewModel Usuario { get; set; }
        //public DificuldadeViewModel Dificuldade { get; set; }
    }
}