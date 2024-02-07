using CafeComFormacao.Models;
using System.Security.Claims;

namespace CafeComFormacao.Interfaces.Services
{
    public interface ILoginService
    {
        ClaimsPrincipal ConfigurarCookies<T>(T login) where T : class;
        Task<object> VerificarCredenciais(string usuario, string senha);
    }
}
