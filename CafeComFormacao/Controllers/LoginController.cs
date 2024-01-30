using CafeComFormacao.Data;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Logar(string usuario, string senha, bool lembrar)
        {
            CredenciaisAdm loginAdm = await _loginService.VerificarAdm(usuario, senha);

            if (loginAdm != null)
            {
                ClaimsPrincipal principalAdm = _loginService.ConfigurarCookiesAdm(loginAdm);

                await HttpContext.SignInAsync("CookieAuthentication", principalAdm, new AuthenticationProperties()
                {
                    IsPersistent = lembrar
                });

                return RedirectToAction("Admin", "Login");
            }

            CredenciaisParticipante loginParticipante = await _loginService.VerificarParticipante(usuario.Trim(), senha.Trim());

            if (loginParticipante == null) 
            {
                ViewBag.Aviso = "Usuário ou senha incorretos!";
                
                return View("Index");
            }

            ClaimsPrincipal principal = _loginService.ConfigurarCookiesParticipante(loginParticipante);

            await HttpContext.SignInAsync("CookieAuthentication", principal, new AuthenticationProperties()
            {
                IsPersistent = lembrar
            });

            return RedirectToAction("Participante", "Login");
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
