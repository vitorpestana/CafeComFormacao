using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeComFormacao.Controllers
{
    public class PalestranteController : Controller
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IPalestranteService _palestranteService;

        public PalestranteController(IPalestranteRepository palestranteRepository, IEventoRepository eventoRepository, IPalestranteService palestranteService)
        {
            _palestranteRepository = palestranteRepository;
            _eventoRepository = eventoRepository;
            _palestranteService = palestranteService;
        }

        public IActionResult AdicionarPalestrante()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarPalestrante(Palestrante palestrante)
        {
            await _palestranteRepository.InserirPalestrante(palestrante);

            ViewBag.CarregarDeNovo = "AdicionarPalestrante";

            ViewBag.Aviso = "Palestrante adicionado com sucesso!";

            return View("./Views/Login/Admin.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarPalestranteNoEvento(int eventoId, int palestranteId)
        {
            await _palestranteService.InserirPalestranteNoEvento(eventoId, palestranteId);

            ViewsModels listaDePalestrantesEObjetoPalestrante = await _palestranteService.ListarPalestrantesEventos();

            ViewBag.CarregarDeNovo = "InserirPalestranteNoEvento";

            ViewBag.Aviso = "O palestrante foi adicionado com sucesso ao evento selecionado!";

            return View("./Views/Login/Admin.cshtml", listaDePalestrantesEObjetoPalestrante);
        }

        [HttpGet]
        public async Task<IActionResult> ListarPalestrantes()
        {
            List<Palestrante> palestrantes = await _palestranteRepository.ListarPalestrantes();

            return View("./Views/Palestrante/ListarPalestrantes.cshtml", palestrantes);
        }

        [HttpGet]
        public async Task<IActionResult> InserirPalestranteNoEvento()
        {
            ViewsModels listaDePalestrantesEObjetoPalestrante = await _palestranteService.ListarPalestrantesEventos();

            return View("./Views/Palestrante/InserirPalestranteNoEvento.cshtml", listaDePalestrantesEObjetoPalestrante);
        }
    }
}
