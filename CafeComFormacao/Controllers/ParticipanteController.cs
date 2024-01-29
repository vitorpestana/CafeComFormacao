using CafeComFormacao.Interfaces;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeComFormacao.Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IViewsModelsRepository _viewsModelsRepository;

        public ParticipanteController(IParticipanteRepository participanteRepository, IEventoRepository eventoRepository, IViewsModelsRepository viewsModelsRepository)
        {
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
            _viewsModelsRepository = viewsModelsRepository;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarParticipante(Cadastro participante)
        {
            await _participanteRepository.Inserir(participante);

            return View("./Views/Login/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InscreverEvento(List<int> eventosSelecionados)
        {
            _eventoRepository.AdicionarEventoAoUsuario(eventosSelecionados, int.Parse(User.FindFirstValue(ClaimTypes.Sid)));

            return View("./Views/Home/Index.cshtml");
        }

        public async Task<IActionResult> SelecaoEvento()
        {
            List<Evento> eventos = await _eventoRepository.ListarEventos();

            return View("./Views/Evento/SelecaoEvento.cshtml", eventos);
        }

        public async Task<IActionResult> ListarParticipantes()
        {
            var participantes = await _participanteRepository.ListarParticipantes();

            return View("ListarParticipantes", participantes);
        }

        public async Task<IActionResult> UsuarioPorEvento()
        {
            ViewsModels viewsModels = await _viewsModelsRepository.PrepararParticipantesPorEventoViewsModels();

            ViewData["ParticipantesPorEvento"] = viewsModels.ParticipantesPorEvento;

            return View("UsuarioPorEvento");
        }

    }
}
