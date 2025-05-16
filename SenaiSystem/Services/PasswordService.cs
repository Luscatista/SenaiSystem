using Microsoft.AspNetCore.Identity;
using SenaiSystem.Models;

namespace SenaiSystem.Services;

public class PasswordService
{
    private readonly PasswordHasher<Usuario> _hasher = new();

    public string HashPassword(Usuario usuario)
    {
        return _hasher.HashPassword(usuario, usuario.Senha);
    }
    public bool VerificarSenha(Usuario usuario, string senhaInformada)
    {
        var resultado = _hasher.VerifyHashedPassword(usuario, usuario.Senha, senhaInformada);

        return resultado == PasswordVerificationResult.Success;
    }
}
