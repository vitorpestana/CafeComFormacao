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
            (bool, Participante) login = await _loginService.VerificarUsuario(usuario.Trim(), senha.Trim());

            if (login.Item2 == null) 
            {
                ViewBag.Aviso = "Usuário ou senha incorretos!";
                
                return View("Index");
            }else 
            if(login.Item2 != null && login.Item1)
            {
                ClaimsPrincipal principalAdm = _loginService.ConfigurarCookies(login);

                await HttpContext.SignInAsync("CookieAuthentication", principalAdm, new AuthenticationProperties());

                var participantes = await _participanteRepository.ListarParticipantes();

                return View("Admin", participantes);
            }

            ClaimsPrincipal principal = _loginService.ConfigurarCookies(login);

            await HttpContext.SignInAsync("CookieAuthentication", principal, new AuthenticationProperties());

            return View("Usuario");
        }
    }
}
