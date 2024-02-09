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

        public ParticipanteService(IParticipanteRepository participanteRepository, IEventoRepository eventoRepository, IViewsModelsRepository viewsModelsRepository, IHashService hashService)
        {
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
            _viewsModelsRepository = viewsModelsRepository;
            _hashService = hashService;
        }

        public async Task CriarParticipanteService(Cadastro participante)
        {
            await _participanteRepository.InserirParticipante(participante);

            string senhaSegura = _hashService.GerarCredenciaisSeguras(participante.Senha);

            participante.Senha = senhaSegura;

            await _participanteRepository.InserirCredenciais(participante);
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
    }
}
