using CafeComFormacao.Data;
using CafeComFormacao.Models;
using CafeComFormacao.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeComFormacao.Controllers
{
    public class LoginController : Controller
    {
        private readonly CafeComFormacaoContext _context;
        private readonly ILoginService _loginService;
        private readonly IParticipanteRepository _participanteRepository;

        public LoginController(CafeComFormacaoContext context, ILoginService login, IParticipanteRepository participanteRepository)
        {
            _context = context;
            _loginService = login;
            _participanteRepository = participanteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logar(string usuario, string senha)
        {
            CredenciaisAdm loginAdm = await _loginService.VerificarAdm(usuario, senha);

            if (loginAdm != null)
            {
                ClaimsPrincipal principalAdm = _loginService.ConfigurarCookiesAdm(loginAdm);

                await HttpContext.SignInAsync("CookieAuthentication", principalAdm, new AuthenticationProperties());

                return View("Admin");
            }

            CredenciaisParticipante loginParticipante = await _loginService.VerificarParticipante(usuario.Trim(), senha.Trim());

            if (loginParticipante == null) 
            {
                ViewBag.Aviso = "Usuário ou senha incorretos!";
                
                return View("Index");
            }

            ClaimsPrincipal principal = _loginService.ConfigurarCookiesParticipante(loginParticipante);

            await HttpContext.SignInAsync("CookieAuthentication", principal, new AuthenticationProperties());

            return View("Participante");
        }
    }
}
