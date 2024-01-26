using CafeComFormacao.Data;
using CafeComFormacao.Models;
using CafeComFormacao.Interfaces;

namespace CafeComFormacao.Repositories
{
    public class ViewsModelsRepository : IViewsModelsRepository
    {
        private readonly CafeComFormacaoContext _context;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IEventoRepository _eventoRepository;

        public ViewsModelsRepository(IParticipanteRepository participanteRepository, IEventoRepository eventoRepository, CafeComFormacaoContext context)
        {
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
            _context = context;
        }

        public async Task<ViewsModels> PrepararTudoViewsModels()
        {
            return new ViewsModels()
            {
                ParticipantesPorEvento = await _participanteRepository.ListarCadaParticipantePorEvento(await ObterIdsDosEventos()),
                Participantes = await _participanteRepository.ListarParticipantes(),
                Eventos = await _eventoRepository.ListarEventos(),
                UsuarioEventos = await _participanteRepository.ListarRelacionamentoParticipanteEvento()
            };
        }

        public async Task<ViewsModels> PrepararParticipantesPorEventoViewsModels()
        {
            return new ViewsModels()
            {
                ParticipantesPorEvento = await _participanteRepository.ListarCadaParticipantePorEvento(await ObterIdsDosEventos())
            };
        }

        public async Task<List<int>> ObterIdsDosEventos()
        {
            return await (from Evento evento in _context.Evento
                          select evento.Id).ToListAsync();
        }
    }
}
