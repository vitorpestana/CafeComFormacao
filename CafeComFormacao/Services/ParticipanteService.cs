using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Interfaces.Util;
using CafeComFormacao.Models;

namespace CafeComFormacao.Services
{
    public class ParticipanteService : IParticipanteService
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IViewsModelsRepository _viewsModelsRepository;
        private readonly IHashService _hashService;
        private readonly ISanitizarService _sanitizarService;
        private readonly IEmailService _emailService;

        public ParticipanteService(IParticipanteRepository participanteRepository, IEventoRepository eventoRepository, IViewsModelsRepository viewsModelsRepository, IHashService hashService, ISanitizarService sanitizar, IEmailService emailService)
        {
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
            _viewsModelsRepository = viewsModelsRepository;
            _hashService = hashService;
            _sanitizarService = sanitizar;
            _emailService = emailService;
        }

        public async Task CriarParticipanteService(Cadastro participante)
        {
            Participante participanteCadastro = new()
            {
                Nome = _sanitizarService.RetirarCaracteresPossivelmenteMaliciosos(participante.Nome),
                Email = _sanitizarService.RetirarCaracteresPossivelmenteMaliciosos(participante.Email),
                Celular = _sanitizarService.RetirarCaracteresPossivelmenteMaliciosos(participante.Celular),
                CursoLidere = participante.CursoLidere,
            };

            await _participanteRepository.InserirParticipante(participanteCadastro);

            string senhaSegura = _hashService.GerarCredenciaisSeguras(participante.Senha);

            CredenciaisParticipante credenciaisParticipante = new()
            {
                LoginEmail = _sanitizarService.RetirarCaracteresPossivelmenteMaliciosos(participante.Email),
                Senha = senhaSegura,
                EmailVerificado  = false
            };

            await _participanteRepository.InserirCredenciais(credenciaisParticipante);

            string idParticipante = await _participanteRepository.ObterIdParticipante(participanteCadastro.Email);

            string corpoDoEmailDeConfirmacaoDeCadastro = _emailService.GerarCorpoDoEmail(idParticipante);

            await _emailService.EnviarEmailAsync("vitorpestanatr@gmail.com", "Confirmação de cadastro no Café com Formação", corpoDoEmailDeConfirmacaoDeCadastro);
        }

        public void InscreverEventoService(List<int> eventosSelecionados, int idUsuario)
        {
            _eventoRepository.AdicionarEventoAoUsuario(eventosSelecionados, idUsuario);
        }

        public async Task<List<Evento>> SelecaoEventoService()
        {
            return await _eventoRepository.ListarEventos();
        }

        public async Task<List<ViewsModels>> UsuarioPorEventoService()
        {
            return await _viewsModelsRepository.PrepararParticipantesPorEventoViewsModels();
        }

        public async Task<List<Participante>> ListarParcipantesService()
        {
            return await _participanteRepository.ListarParticipantes();
        }

        public async Task<bool> VerificarExistenciaEmail(string email)
        {
            return await _participanteRepository.VerificarSeOEmailExiste(email);
        }

        public async Task<bool> VerificarExistenciaCelular(string celular)
        {
            return await _participanteRepository.VerificarSeOCelularExiste(celular);
        }

        public async Task<(string, string)> VerificarCodigoEnviadoPeloEmail(string codigoDeVerificacao)
        {
            bool codigoehValido = await _participanteRepository.VerificarCodigoDeValidacao(codigoDeVerificacao);

            if (codigoehValido)
            {
                await _participanteRepository.DeletarCodigoDeVerificacao(codigoDeVerificacao);

            }

            string alertaDeRetorno = codigoehValido ? "O código fornecido confere com o que foi enviado! Por favor, realize o seu login." : 
                                                          "O código de verificação fornecido não corresponde ao verdadeiro.";

            string viewDeRetorno = codigoehValido ? "./Views/Login/Index.cshtml" : "VerificarEmail";

            return (alertaDeRetorno, viewDeRetorno);
        }
    }
}
