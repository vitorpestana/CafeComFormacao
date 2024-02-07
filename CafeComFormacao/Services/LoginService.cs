using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Models;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CafeComFormacao.Services
{
    public class LoginService : ILoginService
    {
        private readonly IParticipanteRepository _participanteRepository;

        public LoginService(IParticipanteRepository participanteRepository)
        {
            _participanteRepository = participanteRepository;
        }

        public async Task<object> VerificarCredenciais(string usuario, string senha)
        {
            object login = await _participanteRepository.VerificarSeEhAdm(usuario, senha);

            login ??= await _participanteRepository.VerificarCredenciais(usuario, senha);

            return login;
        }

        public ClaimsPrincipal ConfigurarCookies<T>(T login) where T : class
        {
            if (login == null)
            {
                throw new ArgumentException("Erro!");
            }

            List<Claim> claims = new();

            if (login is CredenciaisAdm)
            {
                CredenciaisAdm loginAdm = login as CredenciaisAdm;

                claims.Add(new(ClaimTypes.Name, loginAdm.LoginEmail));
                claims.Add(new(ClaimTypes.Sid, loginAdm.Id.ToString()));
                claims.Add(new(ClaimTypes.Actor, "Admin"));
            }else
            if (login is CredenciaisParticipante)
            {
                CredenciaisParticipante loginParticipante = login as CredenciaisParticipante;

                claims.Add(new(ClaimTypes.Name, loginParticipante.LoginEmail));
                claims.Add(new(ClaimTypes.Sid, loginParticipante.Id.ToString()));
                claims.Add(new(ClaimTypes.Actor, "Usuário"));
            }
            else
            {
                throw new ArgumentException("Erro!");
            }

            ClaimsIdentity identidadeDoUsuarioAdmn = new(claims, "Acesso");

            return new ClaimsPrincipal(identidadeDoUsuarioAdmn);
        }
    }
}
