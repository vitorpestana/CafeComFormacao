using CafeComFormacao.Models;
using CafeComFormacao.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CafeComFormacao.Controllers
{
    public class InscricaosController : Controller
    {
        private readonly BancoDeDadosService _bancoService;

        public InscricaosController(BancoDeDadosService bancoService)
        {
            _bancoService = bancoService;
        }


        // GET: InscricaoController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InscreverEvento(List<int> eventosSelecionados, int idUsuario)
        {
            _bancoService.AdicionarEventoAoUsuario(eventosSelecionados, idUsuario);

            return View("./Views/Home/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CriarEvento(string nomeDoEvento, DateTime dataDoEvento, string horaDoEvento, double valorDoEvento)
        {
            Evento evento = new()
            {
                NomeDoEvento = nomeDoEvento,
                DataDoEvento = dataDoEvento.Date,
                HoraDoEvento = horaDoEvento,
                ValorDoEvento = valorDoEvento
            };

            _bancoService.CriarNovoEvento(evento);

            return View("./Views/Login/Admin.cshtml");
        }

        public ActionResult NovoEvento()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Participante participante)
        {
            int? idUsuario = await _bancoService.Inserir(participante);

            if(idUsuario == null)
            {
                throw new ArgumentException("Houve um erro na hora de cadastrar o usuário.");
            }

            var eventos = await _bancoService.ListarEventos();

            ViewBag.idUsuario = idUsuario; //arrume uma maneira mais segura de passar esse id!

            return View("Evento", eventos);
        }

        public async Task<IActionResult> ListarParticipantes()
        {
            var participantes = await _bancoService.ListarParticipantes();

            return View("ListarParticipantes", participantes);
        }

        public async Task<IActionResult> UsuarioPorEvento()
        {
            ViewsModels viewsModels = await _bancoService.PrepararParticipantesPorEventoViewsModels();

            ViewData["ParticipantesPorEvento"] = viewsModels.ParticipantesPorEvento;

            return View("UsuarioPorEvento");
        }

        public async  Task<IActionResult> ListarTodosOsEventosDoUsuario(int usuarioId)
        {
            List<Evento> eventosDoUsuario = await _bancoService.ListarEventosDoUsuario(usuarioId);

            return View("EventosDoUsuario", eventosDoUsuario);
        }
    }
}
