using CafeComFormacao.Interfaces;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeComFormacao.Controllers
{
    public class EventoController : Controller
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public ActionResult NovoEvento()
        {
            return View();
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

            _eventoRepository.CriarNovoEvento(evento);

            return View("./Views/Login/Admin.cshtml");
        }

        public async Task<IActionResult> ListarTodosOsEventosDoUsuario()
        {
            List<Evento> eventosDoUsuario = await _eventoRepository.ListarEventosDoUsuario(int.Parse(User.FindFirstValue(ClaimTypes.Sid)));

            return View("./Views/Evento/EventosDoUsuario.cshtml", eventosDoUsuario);
        }
    }
}
