using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces.Services
{
    public interface IEventoService
    {
        void CriarNovoEventoService(string nomeDoEvento, DateTime dataDoEvento, string horaDoEvento, double valorDoEvento);
        Task<List<Evento>> ListarTodosOsEventosDoUsuarioService(int idUsuario);
    }
}
