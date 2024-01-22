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
        public void InscreverEvento(List<int> eventosSelecionados, int idUsuario)
        {
            _bancoService.AdicionarEventoAoUsuario(eventosSelecionados, idUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void CriarEvento(string nomeDoEvento, DateTime dataDoEvento, string horaDoEvento, double valorDoEvento)
        {
            Evento evento = new()
            {
                NomeDoEvento = nomeDoEvento,
                DataDoEvento = dataDoEvento.Date,
                HoraDoEvento = horaDoEvento,
                ValorDoEvento = valorDoEvento
            };

            _bancoService.CriarNovoEvento(evento);
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
            IEnumerable<Evento> eventos = await _bancoService.ListarEventos();

            List<int> idEventos = new();

            foreach (Evento evento in eventos)
            {
                idEventos.Add(evento.Id);
            }

            ViewsModels viewsModels = new ViewsModels()
            {
                ParticipantesPorEvento = _bancoService.TodosOsUsuariosPorEvento(idEventos),
                Participantes = await _bancoService.ListarParticipantes(),
                Eventos = eventos,
                UsuarioEventos = await _bancoService.ListarTodosOsUsuariosPorEvento()
            };

            ViewData["Evento"] = viewsModels.Eventos;
            ViewData["UsuarioEvento"] = viewsModels.UsuarioEventos;
            ViewData["ParticipantesPorEvento"] = viewsModels.ParticipantesPorEvento;

            return View("UsuarioPorEvento", viewsModels.Participantes);
        }
    }
}
