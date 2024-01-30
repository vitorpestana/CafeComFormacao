using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Models;
using System.Security.Claims;

namespace CafeComFormacao.Services
{
    public class LoginService : ILoginService
    {
        private readonly IParticipanteRepository _participanteRepository;

        public LoginService(IParticipanteRepository participanteRepository)
        {
            _participanteRepository = participanteRepository;
        }

        public async Task<CredenciaisParticipante> VerificarParticipante(string usuario, string senha)
        {
            CredenciaisParticipante participante = await _participanteRepository.VerificarCredenciais(usuario, senha);

            return participante;
        }

        public async Task<CredenciaisAdm> VerificarAdm(string usuario, string senha)
        {
            return await _participanteRepository.VerificarSeEhAdm(usuario, senha);
        }

        public ClaimsPrincipal ConfigurarCookiesParticipante(CredenciaisParticipante login)
        {
            List<Claim> claims = new();

            claims.Add(new(ClaimTypes.Name, login.LoginEmail));
            claims.Add(new(ClaimTypes.Sid, login.Id.ToString()));
            claims.Add(new(ClaimTypes.Actor, "Usuário"));

            ClaimsIdentity identidadeDoUsuarioAdmn = new(claims, "Acesso");

            return new ClaimsPrincipal(identidadeDoUsuarioAdmn);
        }
        public ClaimsPrincipal ConfigurarCookiesAdm(CredenciaisAdm login)
        {
            List<Claim> claims = new();

            claims.Add(new(ClaimTypes.Name, login.LoginEmail));
            claims.Add(new(ClaimTypes.Sid, login.Id.ToString()));
            claims.Add(new(ClaimTypes.Actor, "Admin"));

            ClaimsIdentity identidadeDoUsuarioAdmn = new(claims, "Acesso");

            return new ClaimsPrincipal(identidadeDoUsuarioAdmn);
        }
    }
}
