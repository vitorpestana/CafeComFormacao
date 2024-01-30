using CafeComFormacao.Models;
using System.Security.Claims;

namespace CafeComFormacao.Interfaces.Services
{
    public interface ILoginService
    {
        Task<CredenciaisParticipante> VerificarParticipante(string usuario, string senha);
        ClaimsPrincipal ConfigurarCookiesAdm(CredenciaisAdm login);
        ClaimsPrincipal ConfigurarCookiesParticipante(CredenciaisParticipante login);
        Task<CredenciaisAdm> VerificarAdm(string usuario, string senha);
    }
}
