using CafeComFormacao.Interfaces;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<(bool, Participante)> VerificarUsuario(string usuario, string senha)
        {
            Participante participante = await _participanteRepository.VerificarCredenciais(usuario, senha);

            return (participante.Admin, participante);
        }

        public ClaimsPrincipal ConfigurarCookies((bool, Participante) login)
        {
            List<Claim> claims = new();

            claims.Add(new(ClaimTypes.Name, login.Item2.Nome));
            claims.Add(new(ClaimTypes.Sid, login.Item2.Id.ToString()));

            ClaimsIdentity identidadeDoUsuarioAdmn = new(claims, "Acesso");

            return new ClaimsPrincipal(identidadeDoUsuarioAdmn);
        }
    }
}
