using CafeComFormacao.Data;
using CafeComFormacao.Models;
using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;

namespace CafeComFormacao.Repositories
{
    public class ViewsModelsRepository : IViewsModelsRepository
    {
        private readonly CafeComFormacaoContext _context;
        private readonly IViewsModelsService _viewsModelsService;

        public ViewsModelsRepository(CafeComFormacaoContext context, IViewsModelsService viewsModelsService)
        {
            _context = context;
            _viewsModelsService = viewsModelsService;
        }

        public async Task<List<ViewsModels>> PrepararParticipantesPorEventoViewsModels()
        {
            return await _viewsModelsService.ListarCadaParticipantePorEvento(await ObterIdsDosEventos());
        }

        public async Task<List<int>> ObterIdsDosEventos()
        {
            return await (from Evento evento in _context.Evento
                          select evento.Id).ToListAsync();
        }
    }
}
