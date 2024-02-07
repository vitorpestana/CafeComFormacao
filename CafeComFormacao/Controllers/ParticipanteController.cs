﻿using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeComFormacao.Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly IParticipanteService _participanteService;

        public ParticipanteController(IParticipanteService participanteService)
        {
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
            int idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.Sid));

            _participanteService.InscreverEventoService(eventosSelecionados, idUsuario);

            return View("./Views/Home/Index.cshtml");
        }

        public async Task<IActionResult> SelecaoEvento()
        {
            List<Evento> eventos = await _participanteService.SelecaoEventoService();

            return View("./Views/Evento/SelecaoEvento.cshtml", eventos);
        }

        public async Task<IActionResult> ListarParticipantes()
        {
            List<Participante> participantes = await _participanteService.ListarParcipantesService();

            return View("ListarParticipantes", participantes);
        }

        public async Task<IActionResult> UsuarioPorEvento()
        {
            List<ViewsModels> viewsModels = await _participanteService.UsuarioPorEventoService();

            return View("UsuarioPorEvento", viewsModels);
        }

    }
}
