using CafeComFormacao.Data;
using CafeComFormacao.Models;
using CafeComFormacao.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Security.Claims;

namespace CafeComFormacao.Controllers
{
    public class LoginController : Controller
    {
        private readonly CafeComFormacaoContext _context;
        private readonly LoginService _loginService;
        private readonly BancoDeDadosService _bancoDeDadosService;

        public LoginController(CafeComFormacaoContext context, LoginService login, BancoDeDadosService bancoService)
        {
            _context = context;
            _loginService = login;
            _bancoDeDadosService = bancoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logar(string usuario, string senha)
        {
            (bool, Participante) login = await _loginService.VerificarUsuario(usuario.Trim(), senha.Trim());

            List<Claim> claims = new();

            if (login.Item2 == null) 
            {
                ViewBag.Aviso = "Usuário ou senha incorretos!";
                
                return View("Index");
            }else 
            if(login.Item2 != null && login.Item1)
            {
                claims.Add(new(ClaimTypes.Name, login.Item2.Nome));
                claims.Add(new(ClaimTypes.Sid, login.Item2.Id.ToString()));

                ClaimsIdentity identidadeDoUsuarioAdmn = new(claims, "Acesso");

                ClaimsPrincipal principalAdm = new(identidadeDoUsuarioAdmn);

                await HttpContext.SignInAsync("CookieAuthentication", principalAdm, new AuthenticationProperties());

                var participantes = await _bancoDeDadosService.ListarParticipantes();

                return View("Admin", participantes);
            }

            claims.Add(new(ClaimTypes.Name, login.Item2.Nome));
            claims.Add(new(ClaimTypes.Sid, login.Item2.Id.ToString()));

            ClaimsIdentity identidadeDoUsuario = new(claims, "Acesso");

            ClaimsPrincipal principal = new(identidadeDoUsuario);

            await HttpContext.SignInAsync("CookieAuthentication", principal, new AuthenticationProperties());

            return View("Usuario");
        }
    }
}
