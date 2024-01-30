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
        private readonly IParticipanteService _participanteService;
        private readonly IViewsModelsService _viewModelsService;

        public ParticipanteController(IParticipanteRepository participanteRepository, IEventoRepository eventoRepository, IViewsModelsRepository viewsModelsRepository, IParticipanteService participanteService)
        {
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
            _viewsModelsRepository = viewsModelsRepository;
            _participanteService = participanteService;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarParticipante(Cadastro participante)
        {
            await _participanteService.CriarParticipanteService(participante);

            return View("./Views/Login/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InscreverEvento(List<int> eventosSelecionados)
        {
            _participanteService.InscreverEventoService(eventosSelecionados, int.Parse(User.FindFirstValue(ClaimTypes.Sid)));

            return View("./Views/Home/Index.cshtml");
        }

        public async Task<IActionResult> SelecaoEvento()
        {
            List<Evento> eventos = await _eventoRepository.ListarEventos();

            return View("./Views/Evento/SelecaoEvento.cshtml", eventos);
        }

        public async Task<IActionResult> ListarParticipantes()
        {
            List<Evento> participantes = await _participanteService.SelecaoEventoService();

            return View("ListarParticipantes", participantes);
        }

        public async Task<IActionResult> UsuarioPorEvento()
        {
            List<ViewsModels> viewsModels = await _participanteService.UsuarioPorEventoService();

            ViewData["ParticipantesPorEvento"] = viewsModels;

            return View("UsuarioPorEvento");
        }

    }
}
