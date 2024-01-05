using CafeComFormacao.Data;
using CafeComFormacao.Models;
using CafeComFormacao.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

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
            bool login = await _loginService.VerificarUsuario(usuario.Trim(), senha.Trim());
            bool adm = await _loginService.VerificarSeEhAdmin(usuario.Trim(), senha.Trim());

            if (!login) 
            {
                ViewBag.Aviso = "Usuário ou senha incorretos!";
                
                return View("Index");
            }else 
            if(login && adm)
            {
                var participantes = await _bancoDeDadosService.ListarParticipantes();

                return View("Admin", participantes);
            }

            return View("Usuario");
        }
    }
}
