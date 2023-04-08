using System.Data.SqlTypes;

namespace ToDo.WebAPI.ViewModels
{
    public class DificuldadeViewModel : EntityViewModel
    {
        public string Nome { get; set; }
        public string  Pontos { get; set; }
    }
}