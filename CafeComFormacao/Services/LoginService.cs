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

        public async Task<object> VerificarCredenciais(string usuario, string senha)
        {
            dynamic login = await _participanteRepository.VerificarSeEhAdm(usuario, senha);

            login ??= await _participanteRepository.VerificarCredenciais(usuario, senha);

            bool senhaVerificada = VerificarSenha(login, senha);

            if (senhaVerificada)
            {
                if (login is CredenciaisAdm)
                {
                    login = new CredenciaisAdm { Id = login.Id, LoginEmail = login.LoginEmail };
                }
                else
                {
                    login = new CredenciaisParticipante { Id = login.Id, LoginEmail = login.LoginEmail };
                }
            }

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
