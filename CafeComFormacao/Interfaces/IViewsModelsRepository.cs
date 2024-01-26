using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces
{
    public interface IViewsModelsRepository
    {
        Task<ViewsModels> PrepararTudoViewsModels();
        Task<ViewsModels> PrepararParticipantesPorEventoViewsModels();
        Task<List<int>> ObterIdsDosEventos();
    }
}
