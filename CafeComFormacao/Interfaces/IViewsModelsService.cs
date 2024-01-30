using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces
{
    public interface IViewsModelsService
    {
        Task<List<ViewsModels>> ListarCadaParticipantePorEvento(List<int> eventosId);
    }
}
