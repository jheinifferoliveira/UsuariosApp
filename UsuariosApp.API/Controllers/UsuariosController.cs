using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using UsuariosApp.API.DTOs;
using UsuariosApp.API.DTOs.RequestDTO;
using UsuariosApp.API.DTOs.ResponseDTO;
using UsuariosApp.API.DTOs.ResponsesDTO;
using UsuariosApp.API.Security;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Services;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioDomainService _usuarioDomainService;

        public UsuariosController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        [HttpPost]
        [Route("autenticar")]
        [ProducesResponseType(typeof(AutenticarUsuarioResponseDTO), 200)]
        public IActionResult Autenticar(AutenticarUsuarioRequestDTO dto)
        {
            try
            {
                var usuario = _usuarioDomainService.Autenticar(dto.Email, dto.Senha);

                var response = new AutenticarUsuarioResponseDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    DataHoraAcesso = DateTime.Now,
                    AccessToken = JwtTokenSecurity.GenerateToken(usuario.Id.Value),
                    DataHoraExpiracao = DateTime.Now.AddHours(JwtTokenSecurity.ExpirationInHours)
                };

                return StatusCode(200, new { message = "Usuário autenticado com sucesso", response });
            }

            catch (ApplicationException e)
            {
                return StatusCode(401, new { e.Message });
            }
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPost]
        [Route("criar")]
        [ProducesResponseType(typeof(CriarUsuarioResponseDTO), 201)]
        public IActionResult Criar(CriarUsuarioRequestDTO dto) {
            try
            {
                var usuario=new Usuario{
                    Nome= dto.Nome,
                    Email=dto.Email,
                    Senha=dto.Senha
                };

                _usuarioDomainService.CriarConta(usuario);

                var response = new CriarUsuarioResponseDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    DataHoraCadastro = usuario.DataHoraCadastro.Value

                };

                return StatusCode(201, new { message = "Conta de usuário criado com sucesso.", response });
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("obter-dados")]
        [ProducesResponseType(typeof(ObterDadosUsuarioResponseDTO),200)]
        public IActionResult Get()
        {
            try
            {
                var id = Guid.Parse(User.Identity.Name);

                var usuario = _usuarioDomainService.ObterDados(id);

                var response = new ObterDadosUsuarioResponseDTO
                {
                    Id = usuario.Id,
                    NomeUsuario = usuario.Nome,
                    Email = usuario.Email,
                    PerfilId = usuario.Perfil.Id,
                    NomePerfil = usuario.Perfil.Nome,
                    DataHoraCadastro = usuario.DataHoraCadastro


                };

                return StatusCode(200, response);

            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}
