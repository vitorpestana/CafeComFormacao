﻿using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Interfaces.Util;
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

            ViewBag.CodigoVerificacao = "Um código de verificação foi enviado para o seu email. Para completar seu cadastro e realizar seu login, copie e cole o código no link indicado no email.";

            return View("./Views/Home/Index.cshtml");
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

        [HttpGet]
        public async Task<IActionResult> ListarParticipantes()
        {
            List<Participante> participantes = await _participanteService.ListarParcipantesService();

            return View("ListarParticipantes", participantes);
        }

        [HttpGet]
        public async Task<IActionResult> UsuarioPorEvento()
        {
            List<ViewsModels> viewsModels = await _participanteService.UsuarioPorEventoService();

            return View("UsuarioPorEvento", viewsModels);
        }

        [HttpGet]
        public async Task<bool> VerificarSeOEmailJaExiste(string email)
        {
            return await _participanteService.VerificarExistenciaEmail(email);
        }

        [HttpGet]
        public async Task<bool> VerificarSeOCelularJaExiste(string celular)
        {
            return await _participanteService.VerificarExistenciaCelular(celular);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerificarCodigoDeVerificacao(string codigoDeVerificacao)
        {
            (string alertaDeRetorno, string viewDeRetorno) stringsDeRetorno= await _participanteService.VerificarCodigoEnviadoPeloEmail(codigoDeVerificacao);

            ViewBag.CodigoVerificacao = stringsDeRetorno.alertaDeRetorno;

            return View(stringsDeRetorno.viewDeRetorno);
        }

        [HttpGet]
        public IActionResult VerificarEmail()
        {
            return View();
        }
    }
}
