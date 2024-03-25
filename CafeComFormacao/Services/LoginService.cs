using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Interfaces.Util;
using CafeComFormacao.Models;
using System.Security.Claims;

namespace CafeComFormacao.Services
{
    public class LoginService : ILoginService
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IHashService _hashService;

        public LoginService(IParticipanteRepository participanteRepository, IHashService hashService)
        {
            _participanteRepository = participanteRepository;
            _hashService = hashService;
        }

        public async Task<CredenciaisParticipante> VerificarCredenciais(string usuario, string senha)
        {
            CredenciaisParticipante login = await _participanteRepository.VerificarCredenciais(usuario);

            bool senhaVerificada = login == null ? false : VerificarSenha(login, senha);

            return senhaVerificada ? login : null;
        }

        public async Task<CredenciaisAdm> VerificarCredenciaisAdm(string usuario, string senha)
        {
            CredenciaisAdm login = await _participanteRepository.VerificarSeEhAdm(usuario);

            bool senhaVerificada = login == null ? false: VerificarSenha(login, senha);

            return senhaVerificada ? login : null;
        }

        private bool VerificarSenha(dynamic login, string senha)
        {
            string sal = login.Senha.Substring(0, 24);

            string hashSenhaFornecida = _hashService.GerarHashSHA256(senha, sal);

            return login.Senha == hashSenhaFornecida;
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
            }
            else
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
