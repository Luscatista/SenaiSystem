﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.DTOs;
using SenaiSystem.Interface;
using SenaiSystem.Models;
using SenaiSystem.Services;
using SenaiSystem.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace SenaiSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [Authorize]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todos os usuários",
            Description = "Este endpoint lista todos usuários."

        )]
        public IActionResult ListarTodos()
        {
            var usuario = _usuarioRepository.ListarTodos();

            return Ok(usuario);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria um usuário.(EXEMPLO)",
            Description = "Este endpoint cria usuários.(EXEMPLO)"

        )]
        public IActionResult Cadastrar(CadastroEditarUsuarioDto usuario)
        {
            _usuarioRepository.Cadastrar(usuario);
            return Created("Usuario cadastrado com sucesso", usuario);
        }



        [Authorize]
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualizar Usuários",
            Description = "Este endpoint atualiza usuários."

        )]
        public IActionResult Atualizar(int id, CadastroEditarUsuarioDto usuario)
        {
            try
            {
                _usuarioRepository.Atualizar(id, usuario);
                return Ok(usuario);
            }
            catch (Exception)
            {
                return NotFound("Nota não encontrado.");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletar Usuários",
            Description = "Este endpoint deleta usuários."

        )]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Usuario não encontrado.");
            }
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Login - Recebe Token",
            Description = "Este endpoint gera o token de acesso para usuários."

        )]
        public IActionResult Login(LoginDto loginDto)
        {

            var usuario = _usuarioRepository.BuscarPorEmailSenha(loginDto.Email, loginDto.Senha);
            if (usuario == null)
            {
                return Unauthorized("Dados inválidos.");
            }
            var usuarioViewModel = new ListarUsuarioViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            var tokenService = new TokenService();

            var token = tokenService.GenerateToken(usuario.Email);

            return Ok(new
            {
                token,
                usuarioViewModel
            });
        }
        [Authorize]
        [HttpPut("TrocarSenha/{id}")]
        [SwaggerOperation(
            Summary = "Trocar a senha do usuário",
            Description = "Este endpoint troca a senha do usuário."

        )]
        public IActionResult TrocarSenhaDto(TrocarSenhaDto senhaDto)
        {

            var usuario = _usuarioRepository.TrocarSenhaDto(senhaDto.IdUsuario, senhaDto.SenhaAtual, senhaDto.NovaSenha);

            if (usuario == null) return null;

            return Ok(usuario);
        }

        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Busca usuario por Id",
            Description = "Este endpoint busca o usuário pelo id informado."

        )]
        public IActionResult BuscarPorId(int id)
        {
            var usuario = _usuarioRepository.BuscarPorId(id);
            if (usuario == null)
            {
                return NotFound("Usuario não encontrado.");
            }
            return Ok(usuario);
        }
    }
}
