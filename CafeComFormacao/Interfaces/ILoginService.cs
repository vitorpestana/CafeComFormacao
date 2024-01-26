using CafeComFormacao.Models;
using System.Security.Claims;

namespace CafeComFormacao.Interfaces
{
    public interface ILoginService
    {
        Task<(bool, Participante)> VerificarUsuario(string usuario, string senha);
        ClaimsPrincipal ConfigurarCookies((bool, Participante) login);
    }
}
