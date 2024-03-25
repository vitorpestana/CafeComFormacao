using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces.Services
{
    public interface IPalestranteService
    {
        Task<ViewsModels> ListarPalestrantesEventos();
        Task InserirPalestranteNoEvento(int eventoId, int palestranteId);
    }
}
