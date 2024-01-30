using CafeComFormacao.Models;
using System.Threading.Tasks;

namespace CafeComFormacao.Interfaces
{
    public interface IViewsModelsRepository
    {
        Task<List<ViewsModels>> PrepararParticipantesPorEventoViewsModels();
        Task<List<int>> ObterIdsDosEventos();
    }
}
