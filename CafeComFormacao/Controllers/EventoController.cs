﻿using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeComFormacao.Controllers
{
    public class EventoController : Controller
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        public ActionResult NovoEvento()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CriarEvento(string nomeDoEvento, DateTime dataDoEvento, string horaDoEvento, double valorDoEvento)
        {
            _eventoService.CriarNovoEventoService(nomeDoEvento, dataDoEvento, horaDoEvento, valorDoEvento);

            ViewBag.CarregarDeNovo = "NovoEvento";

            ViewBag.Aviso = "Um novo evento foi criado com sucesso!";

            return View("./Views/Login/Admin.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodosOsEventosDoUsuario()
        {
            List<Evento> eventosDoUsuario = await _eventoService.ListarTodosOsEventosDoUsuarioService(int.Parse(User.FindFirstValue(ClaimTypes.Sid)));

            return View("./Views/Evento/EventosDoUsuario.cshtml", eventosDoUsuario);
        }
    }
}
