using AutoMapper;
using System.Net;
using ToDo.Business.Models;
using ToDo.WebAPI.ViewModels;

namespace ToDo.WebAPI.Configuration
{

    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Tarefa, TarefaViewModel>().ReverseMap();
            CreateMap<LogPontuacao, LogPontuacaoViewModel>().ReverseMap();
            CreateMap<Dificuldade, DificuldadeViewModel>().ReverseMap();
        }
    }
}