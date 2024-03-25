using CafeComFormacao.Models;
using System.Security.Claims;

namespace CafeComFormacao.Interfaces.Services
{
    public interface ILoginService
    {
        ClaimsPrincipal ConfigurarCookies<T>(T login) where T : class;
        Task<CredenciaisParticipante> VerificarCredenciais(string usuario, string senha);
        Task<CredenciaisAdm> VerificarCredenciaisAdm(string usuario, string senha);

    }
}
