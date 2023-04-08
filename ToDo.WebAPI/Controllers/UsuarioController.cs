using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.App.Extensions;
using ToDo.Api.Controllers;
using ToDo.App.Extensions;
using ToDo.Business.Intefaces;
using ToDo.Business.Models;
using ToDo.WebAPI.ViewModels;

namespace ToDo.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly INotificador _notificador;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper, INotificador notificador)
            : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _notificador = notificador;
        }

        /// <summary> Obtém todos os usuarios </summary>
        [ClaimsAuthorize("Usuario", "Leitura")]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> ObterTodos()
        {
            try
            {
                var usuarios = _mapper.Map<IEnumerable<UsuarioViewModel>>(await _usuarioRepository.ObterTodos());


                return Ok(usuarios);
            }
            catch { return NotFound(); }
        }

        [HttpGet]
        [Route("[action]/{id:guid}")]
        public async Task<ActionResult> ObterUsuario(Guid id)
        {
            try
            {
                var usuarios = _mapper.Map<UsuarioViewModel>(await _usuarioRepository.ObterPorId(id));

                return Ok(usuarios);
            }
            catch { return NotFound(); }
        }

        [ClaimsAuthorize("Usuario","Inserir")]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InserirUsuario([FromBody] UsuarioViewModel usuario)
        {

            if (! ModelState.IsValid) return CustomResponse(usuario);

            var usuarioMapped = _mapper.Map<Usuario>(usuario);
            await _usuarioRepository.Adicionar(usuarioMapped);

            return CustomResponse(usuario);

        }

        [ClaimsAuthorize("Usuario", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> AtualizarUsuario(Guid id,UsuarioViewModel usuario)
        {
            if (id != usuario.Id) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(usuario);

            var usuarioMapped = _mapper.Map<Usuario>(usuario);
            await _usuarioRepository.Atualizar(usuarioMapped);

            return CustomResponse(usuario);

        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult> ApagarUsuario(Guid id)
        {
            var consulta = ObterUsuario(id);
            
            
            await _usuarioRepository.Remover(id);

            return Ok();
        }
    }
}
