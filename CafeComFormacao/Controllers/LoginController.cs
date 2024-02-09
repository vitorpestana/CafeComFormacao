using CafeComFormacao.Data;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using CafeComFormacao.Interfaces.Util;

namespace CafeComFormacao.Controllers
{
    public class LoginController : Controller
    {
        private readonly CafeComFormacaoContext _context;
        private readonly ILoginService _loginService;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IHashService _hashService;

        public LoginController(CafeComFormacaoContext context, ILoginService login, IParticipanteRepository participanteRepository, IHashService hashService)
        {
            _context = context;
            _loginService = login;
            _participanteRepository = participanteRepository;
            _hashService = hashService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logar(string usuario, string senha, bool lembrar)
        {
            object login = await _loginService.VerificarCredenciais(usuario, senha);

            if (login == null) 
            {
                ViewBag.Aviso = "Credenciais incorretas!";
                
                return View("Index");
            }

            string view = login.GetType() == typeof(CredenciaisAdm) ? "Admin" : "Participante";

            ClaimsPrincipal principal = _loginService.ConfigurarCookies(login);

            await HttpContext.SignInAsync("CookieAuthentication", principal, new AuthenticationProperties()
            {
                IsPersistent = lembrar
            });

            return RedirectToAction(view, "Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuthentication", new AuthenticationProperties
            {
                IsPersistent = false
            });

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(AuthenticationSchemes = "CookieAuthentication")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "CookieAuthentication")]
        public IActionResult Participante()
        {
            return View();
        }
    }
}
