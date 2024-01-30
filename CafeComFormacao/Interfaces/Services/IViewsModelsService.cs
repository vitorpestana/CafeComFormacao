using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces.Services
{
    public interface IViewsModelsService
    {
        Task<List<ViewsModels>> ListarCadaParticipantePorEvento(List<int> eventosId);
    }
}
